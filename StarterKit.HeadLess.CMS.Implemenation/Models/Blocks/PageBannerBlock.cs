using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Blocks
{
    [ContentType(DisplayName = "Page Banner Block", GUID = "333C4B49-F92D-4DE5-81AA-45B591F4C260", Description = "")]
    public class PageBannerBlock : BlockData
    {
        [CultureSpecific]
        [Display(Name = "Eyebrow", Description = "",
           GroupName = SystemTabNames.Content, Order = 5)]
        public virtual string Eyebrow { get; set; }

        [CultureSpecific]
        [Required]
        [Display(Name = "Headline", Description = "Please limit your input to a maximum 62 characters in English, 78 characters in non-English to respect the propriety of the design.",
            GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string Headline { get; set; }

        [UIHint(UIHint.Textarea)]
        [CultureSpecific]
        [Display(Name = "Description", Description = "Please limit your input to a maximum 148 characters in English, 186 characters in non-English to respect the propriety of the design.",
            GroupName = SystemTabNames.Content, Order = 20)]
        public virtual XhtmlString Description { get; set; }

        [JsonIgnore]
        [Display(Name = "CTA 1", Description = "Please limit your input to a maximum 28 characters in English, 35 characters in non-English to respect the propriety of the design",
            GroupName = SystemTabNames.Content, Order = 40)]
        public virtual CallToActionButtonBlock Cta1 { get; set; }

        [JsonIgnore]
        [Display(Name = "CTA 2", Description = "Please limit your input to a maximum 28 characters in English, 35 characters in non-English to respect the propriety of the design",
           GroupName = SystemTabNames.Content, Order = 50)]
        public virtual CallToActionButtonBlock Cta2 { get; set; }
    }
}
