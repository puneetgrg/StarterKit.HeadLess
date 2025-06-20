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
            content.Property.Remove("TopContentArea");
            content.Property.Remove("MainContentArea");
            content.Property.Remove("BottomContentArea");
        }
    }
}
