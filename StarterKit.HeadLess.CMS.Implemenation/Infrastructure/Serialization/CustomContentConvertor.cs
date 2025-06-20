using EPiServer.ContentApi.Core.Internal;
using EPiServer.ContentApi.Core.Serialization.Internal;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Serialization
{
    /// <summary>
    /// This class adds support to exclude personalization in Content Delivery API
    /// </summary>
    [ServiceConfiguration(typeof(CustomContentConvertor), Lifecycle = ServiceInstanceScope.Singleton)]
    public class CustomContentConvertor : DefaultContentConverter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomContentConvertor(
                    IContentTypeRepository contentTypeRepository,
                    ReflectionService reflectionService,
                    IContentModelReferenceConverter contentModelService,
                    IContentVersionRepository contentVersionRepository,
                    ContentLoaderService contentLoaderService,
                    UrlResolverService urlResolverService,
                    IPropertyConverterResolver propertyConverterResolver,
                    ContentTypeResolver contentTypeResolver,
                    IHttpContextAccessor httpContextAccessor)
                    : base(contentTypeRepository,
                          reflectionService,
                          contentModelService,
                          contentVersionRepository,
                          contentLoaderService,
                          urlResolverService,
                          propertyConverterResolver,
                          contentTypeResolver)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override ContentApiModel Convert(IContent content, ConverterContext converterContext)
        {
            var excludePersonalizationParam = _httpContextAccessor.HttpContext?.Request.Query["excludePersonalization"];
            //Use excludePersonalization in the request url to control whether the response should peronalized or not. True = Not Personalized. False/Not the in the url = Personalized
            bool.TryParse(excludePersonalizationParam, out var excludePersonalizedContent);

            var newContext = new ConverterContext(content.ContentLink, converterContext.Language, converterContext.Options, converterContext.ContextMode, "*", "*", excludePersonalizedContent);

            var model = base.Convert(content, newContext);

            return model;
        }
    }
}
