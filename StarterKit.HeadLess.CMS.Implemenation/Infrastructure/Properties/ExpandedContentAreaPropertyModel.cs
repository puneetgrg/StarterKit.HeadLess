using EPiServer.ContentApi.Core.Serialization.Models.Internal;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Properties
{
    /// <summary>
    /// This class is needed for expanding Content Areas and nested content inside of them in Content Delivery API
    /// Code originally from Optimizely support
    /// </summary>
    public class ExpandedContentAreaPropertyModel : ContentAreaPropertyModel
    {
        private readonly IContentExpander _contentExpander;

        public ExpandedContentAreaPropertyModel(PropertyContentArea propertyContentArea,
            ConverterContext converterContext, IContentExpander contentExpander) : base(propertyContentArea,
            converterContext)
        {
            _contentExpander = contentExpander;
        }

        protected override IEnumerable<ContentApiModel> ExtractExpandedValue(CultureInfo language)
        {
            var expandedModels = new List<ContentApiModel>();

            foreach (var item in Value)
            {
                expandedModels.Add(InlineBlockPropertyModel.IsNullOrEmpty(item.InlineBlock) ? _contentExpander.Expand(item.ContentLink, CreateExpandableContext(item.ContentLink, ConverterContext, language)) : item.InlineBlock.Expand());
            }

            return expandedModels;
        }

        private static ConverterContext CreateExpandableContext(ContentModelReference contentLink,
            ConverterContext converterContext, CultureInfo language) => new(
            new ContentReference(contentLink.Id!.Value, contentLink.WorkId!.Value), language, converterContext.Options,
            converterContext.ContextMode, null, "*", false);
    }

    [ServiceConfiguration(typeof(IPropertyConverter), Lifecycle = ServiceInstanceScope.Singleton)]
    public class CustomContentAreaPropertyConverter : IPropertyConverter
    {
        public IPropertyModel Convert(PropertyData propertyData, ConverterContext contentMappingContext)
        {
            if (propertyData is not PropertyContentArea propertyContentArea)
            {
                throw new InvalidOperationException();
            }

            var model = new ExpandedContentAreaPropertyModel(propertyContentArea, contentMappingContext,
                ServiceLocator.Current.GetInstance<IContentExpander>());
            model.Expand(contentMappingContext.Language);

            return model;
        }
    }

    [ServiceConfiguration(typeof(IPropertyConverterProvider), Lifecycle = ServiceInstanceScope.Singleton)]
    public class CustomPropertyConverterProvider : IPropertyConverterProvider
    {
        public int SortOrder => 200;

        public IPropertyConverter Resolve(PropertyData propertyData)
        {
            if (propertyData is PropertyContentArea)
            {
                return new CustomContentAreaPropertyConverter();
            }

            return null!;
        }
    }
}
