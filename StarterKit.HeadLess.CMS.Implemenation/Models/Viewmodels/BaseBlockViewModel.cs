using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;
using Newtonsoft.Json;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels
{
    public class BaseBlockViewModel : IBaseBlockViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ContentType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ContentModelReference ContentLink { get; set; }

        public BaseBlockViewModel(BlockData blockData)
        {
            var content = blockData as IContent;

            if (content != null)
            {
                ContentLink = new ContentModelReference
                {
                    Id = content.ContentLink?.ID,
                    GuidValue = new Guid?(content.ContentGuid)
                };
            }
        }
    }
}
