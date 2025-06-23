using AddOn.Episerver.Settings.Core;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using StarterKit.HeadLess.CMS.Implemenation.Models.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.SiteSettings
{
    [SettingsContentType(DisplayName = "Headless - Blog Settings", GUID = "4ED3523E-332A-4119-9BA5-86927EC5C5AA", SettingsName = "Blog Settings")]
    
    public class BlogSettings : SettingsBase
    {
        [CultureSpecific]
        [Display(Name = "Blog Listing Page", GroupName = SystemTabNames.Content, Order = 10)]
        //[AllowedTypes(typeof(BlogListingPage))]
        public virtual ContentReference BlogListingPage { get; set; }

        [CultureSpecific]
        [Display(Name = "Blog Category/Listing Page - Navigation",
            Description = "Drag the Blog Categories into the content area (Level 1 or 2). The Category Display Name is displayed on the navigation.",
            GroupName = SystemTabNames.Content, Order = 20)]
        [AllowedTypes(typeof(starterKitCategory))]
        public virtual ContentArea BlogLinksContentArea { get; set; }

        [CultureSpecific]
        [Display(Name = "Global Content - Bottom content area", GroupName = SystemTabNames.Content, Order = 30)]
        //[AllowedTypes(AllowedTypes = new[]
        //{
        //    typeof(BlogArticleCardsBlock)
        //})]
        public virtual ContentArea BottomContentArea { get; set; }
    }
}
