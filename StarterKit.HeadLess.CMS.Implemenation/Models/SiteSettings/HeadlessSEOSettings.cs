using EPiServer.DataAnnotations;
using StarterKit.HeadLess.Core.Models.SiteSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StarterKit.HeadLess.CMS.Implemenation.Models.Constant;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.SiteSettings
{
    [SettingsContentType(DisplayName = "Headless - SEO Settings", GUID = "5043BD48-1A03-4110-846C-1CED35E4D0A6", SettingsName = "SEO Settings")]
    public class HeadlessSEOSettings : SettingsBase
    {
        [CultureSpecific]
        [UIHint("textarea")]
        [Display(Name = "Robots.txt", GroupName = PropertyGroupNames.SEO, Order = 1)]
        public virtual string RobotsTxt { get; set; }
    }
}
