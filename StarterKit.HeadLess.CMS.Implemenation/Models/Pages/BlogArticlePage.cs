using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Geta.Optimizely.Categories.DataAnnotations;
using Geta.Optimizely.Categories;
using StarterKit.HeadLess.CMS.Implemenation.Models.Blocks;
using StarterKit.HeadLess.CMS.Implemenation.Models.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Pages
{
    [ContentType(DisplayName = "Blog Article Page", GUID = "24635DF8-7F51-4CC8-8AA1-F9EC865452ED", Description = "Use this page to create an blog article page", GroupName = SystemTabNames.Content)]
    [AvailableContentTypes(Availability.Specific)]
    public class BlogArticlePage : HeadlessBasePageData, ICategorizableContent
    {
        // Hide tags
        [ScaffoldColumn(false)]
        public override string Tags { get; set; }

        //Override the base property to apply restrictions
        [Required]
        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(Name = "Title", GroupName = SystemTabNames.Content, Order = 10)]
        public override string Title { get; set; }

        [Required]
        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(Name = "Short Description", GroupName = SystemTabNames.Content, Description = "This description will be used when the user drag and drop the page inside the the featured blog component. Max. of 135 characters", Order = 20)]
        public virtual string ShortDescription { get; set; }

        //Override the base property to apply restrictions
        [UIHint(UIHint.Image)]
        [Display(Name = "Thumbnail",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        public override ContentReference Thumbnail { get; set; }

        //Override the base property to apply restrictions
        [Categories]
        [AllowedTypes(typeof(HeadlessBaseCategoryData))]
        [Display(GroupName = SystemTabNames.PageHeader, Order = 40)]
        public override IList<ContentReference> Categories { get; set; }

        [CultureSpecific]
        [Display(Name = "Author", GroupName = SystemTabNames.Content, Order = 60)]
        public virtual string Author { get; set; }

        [JsonIgnore]
        [Display(Name = "Read Time (minutes)",
             Description = "Enter a number between 1 and 120 to show the article's estimated reading time in minutes, or enter 0 to hide it.",
             Order = 40)]
        [Range(0, 120)]
        public virtual int ReadTime { get; set; }

        [Required]
        [CultureSpecific]
        [Display(Name = "Rich Text", GroupName = SystemTabNames.Content, Order = 70)]
        public virtual XhtmlString RichText { get; set; }

        [CultureSpecific]
        [Display(Name = "Right content area", GroupName = SystemTabNames.Content, Order = 150)]      
        public virtual ContentArea RightContentArea { get; set; }       

        [CultureSpecific]
        [Display(Name = "Show Global Blog Article Cards",
            Description = "When the box is checked the Global Blog article card is displayed in the FE.",
            GroupName = SystemTabNames.Content,
            Order = 220)]
        public virtual bool IncludeGlobalBlogsInBottomContent { get; set; }

        //Override the base propety to apply restrictions
        [Ignore]
        public override ContentArea TopContentArea { get; set; }

        //Override the base propety to apply restrictions
        [Ignore]
        public override ContentArea MainContentArea { get; set; }

        [Ignore]
        public override ContentReference BackgroundImage { get; set; }
    }
}
