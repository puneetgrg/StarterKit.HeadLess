using Newtonsoft.Json;
using StarterKit.HeadLess.CMS.Implemenation.Models.Blocks;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using StarterKit.HeadLess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels
{
    public class CallToActionViewModel : BaseBlockViewModel, ICallToActionViewModel
    {
        public string CtaLabel { get; set; }

        public string CtaAriaLabel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

        public string CtaType { get; set; }

        public string CtaUrl { get; set; }

        public string CtaTarget { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Alignment { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ImageModel ImageData { get; set; }

        public CallToActionViewModel(CallToActionButtonBlock callToActionButtonBlock = null) : base(callToActionButtonBlock)
        {
            ContentType = new List<string> { "Block", nameof(CallToActionButtonBlock) };
        }
    }
}
