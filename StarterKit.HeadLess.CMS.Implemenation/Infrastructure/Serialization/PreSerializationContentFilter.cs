using EPiServer.ContentApi.Core.Serialization.Internal;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Serialization
{
    /// <summary>
    /// This class prevent the serialization of certain properties before the serialization
    /// </summary>
    [ServiceConfiguration(typeof(IContentFilter), Lifecycle = ServiceInstanceScope.Singleton)]
    internal class PreSerializationContentFilter : ContentFilter<IContent>
    {
        public override void Filter(IContent content, ConverterContext converterContext)
        {

            //Content areas
            content.Property.Remove("TopContentArea");
            content.Property.Remove("MainContentArea");
            content.Property.Remove("BottomContentArea");
            content.Property.Remove("RightContentArea");
            content.Property.Remove("LeftContentArea");
            content.Property.Remove("ProductTitlePageContentArea");
            content.Property.Remove("VariantContentArea");
            content.Property.Remove("TabContentArea_1");
            content.Property.Remove("TabContentArea_2");
            content.Property.Remove("TabContentArea_3");
            content.Property.Remove("TabContentArea_4");

            //Properties that have a certain logic
            content.Property.Remove("MetaTitle");
            content.Property.Remove("MetaKeywords");
            content.Property.Remove("MetaDescription");
            content.Property.Remove("DisableIndexing");
            content.Property.Remove("EnableNoFollow");
            content.Property.Remove("OgContentType");
            content.Property.Remove("Categories");
            content.Property.Remove("Tags");
            content.Property.Remove("LevelCategories");
            content.Property.Remove("JobRolesCategories");

            //Images (since the content reference object doesn't provide much information in case of an Image)
            content.Property.Remove("Thumbnail");
            content.Property.Remove("BackgroundImage");
        }
    }
}
