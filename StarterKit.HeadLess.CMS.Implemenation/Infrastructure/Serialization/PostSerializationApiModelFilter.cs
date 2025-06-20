using EPiServer.ContentApi.Core.Serialization.Internal;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Serialization
{
    [ServiceConfiguration(typeof(IContentApiModelFilter), Lifecycle = ServiceInstanceScope.Singleton)]
    public class PostSerializationApiModelFilter : ContentApiModelFilter<ContentApiModel>
    {
        public override void Filter(ContentApiModel contentApiModel, ConverterContext converterContext)
        {
            try
            {
                // Set those values below as null, and configure ContentApiOption.IncludeNullValues = false in Initialization
                // then, response data will not include those ones.

                contentApiModel.ContentLink = null;
                contentApiModel.ExistingLanguages = null;
                contentApiModel.MasterLanguage = null;
                contentApiModel.ParentLink = null;
                contentApiModel.Language = null;
                contentApiModel.StartPublish = null;
                contentApiModel.StopPublish = null;
                contentApiModel.RouteSegment = null;
                contentApiModel.Changed = null;
                contentApiModel.Created = null;
                contentApiModel.Saved = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
