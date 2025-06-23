using AddOn.Episerver.Settings.Core;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.Core.Features.Settings
{
    [SettingsContentType(DisplayName = "Headless - Layout Settings", GUID = "13742D06-F007-47C4-A949-9EE7428BEAC8", SettingsName = "Layout Settings")]
   // [ContentTypeIcon(FontAwesome5Solid.Th, backgroundColor: "#C8102E")]
    public class HeadlessLayoutSettings : SettingsBase
    {
        //#region Quick Links

        //[CultureSpecific]
        //[Display(Name = "Quick Links",
        //    Description = "Add links groups to your quick link menu.",
        //    GroupName = PropertyGroupNames.QuickLinks)]
        //[AllowedTypes(typeof(QuickLinksGroupBlock))]
        //public virtual ContentArea QuickLinks { get; set; }

        //#endregion

        //#region Header
        [CultureSpecific]
        [UIHint(UIHint.Image)]
        [Display(Name = "Logo", Description = "",
            GroupName = "Header",
            Order = 10)]
        public virtual ContentReference HeaderLogo { get; set; }

        //[CultureSpecific]
        //[Display(Name = "Logo Link", Description = "",
        //    GroupName = PropertyGroupNames.Header,
        //    Order = 20)]
        //public virtual LinkItem HeaderLogoLink { get; set; }

        //[JsonIgnore]
        //[CultureSpecific]
        //[AllowedTypes(typeof(HeadlessL1MenuItem))]
        //[Display(Name = "Level One Links",
        //    Description = "", GroupName = PropertyGroupNames.Header,
        //    Order = 30)]
        //public virtual ContentArea LevelOneLinks { get; set; }

        //[CultureSpecific]
        //[Display(Name = "Shop Link", Description = "",
        //    GroupName = PropertyGroupNames.Header,
        //    Order = 40)]
        //public virtual LinkItem ShopLink { get; set; }

        //[CultureSpecific]
        //[Display(Name = "Search Page Link", Description = "",
        //    GroupName = PropertyGroupNames.Header,
        //    Order = 50)]
        //[AllowedTypes(typeof(HeadlessSearchPage))]
        //public virtual ContentReference SearchPage { get; set; }

        //[CultureSpecific]
        //[Display(Name = "Quick Links Items (Search)", Description = "", GroupName = PropertyGroupNames.Header,
        //    Order = 60)]
        //public virtual IList<string> QuickLinksItems { get; set; }

        //[JsonIgnore]
        //[CultureSpecific]
        //[Display(Name = "Login Menu Items",
        //    Description = "", GroupName = PropertyGroupNames.Header,
        //    Order = 70)]
        //public virtual LinkItemCollection LoginMenuItems { get; set; }
        //#endregion

        //#region Footer

        //[CultureSpecific]
        //[UIHint(UIHint.Image)]
        //[Display(Name = "Logo", Description = "",
        //    GroupName = PropertyGroupNames.Footer,
        //    Order = 10)]
        //public virtual ContentReference FooterLogo { get; set; }

        //[CultureSpecific]
        //[Display(Name = "Logo Link", Description = "",
        //    GroupName = PropertyGroupNames.Footer,
        //    Order = 20)]
        //public virtual LinkItem FooterLogoLink { get; set; }

        //[Display(Name = "Social Media",
        //    GroupName = PropertyGroupNames.Footer,
        //    Order = 30)]
        //[MaxListElements(5)]
        //[ListItemHeaderProperty(nameof(SocialMediaItem.Title))]
        //public virtual IList<SocialMediaItem> SocialMediaItems { get; set; }

        //[CultureSpecific]
        //[Display(Name = "Column one",
        //    Description = "",
        //    GroupName = PropertyGroupNames.Footer, Order = 30)]
        //public virtual LinkItemCollection ColumnOne { get; set; }

        //[CultureSpecific]
        //[Display(Name = "Column two",
        //    Description = "",
        //    GroupName = PropertyGroupNames.Footer, Order = 40)]
        //public virtual LinkItemCollection ColumnTwo { get; set; }

        //[CultureSpecific]
        //[Display(Name = "Copyright",
        //    Description = "",
        //    GroupName = PropertyGroupNames.Footer, Order = 50)]
        //public virtual string Copyright { get; set; }
        //#endregion


        //#region Forms

        //[CultureSpecific]
        //[Display(Name = "B2C Form Source URL",
        //    GroupName = PropertyGroupNames.Forms, Order = 10)]
        //public virtual string FormSourceUrl { get; set; }

        //[CultureSpecific]
        //[Display(Name = "B2B Form Source URL",
        //    GroupName = PropertyGroupNames.Forms, Order = 20)]
        //public virtual string B2BFormSourceUrl { get; set; }
        //#endregion

        //[Display(Name = "Disable CloudFlare Cache Purge", Description = "Disable CloudFlare Cache ", GroupName = "CloudflareCache", Order = 10)]
        //public virtual bool DisableCloudFlareCachePurge { get; set; }
    }
}
