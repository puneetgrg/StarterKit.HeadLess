using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StarterKit.HeadLess.CMS.Implemenation.Models.Constant;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Pages
{
    public abstract class HeadlessBasePageData : PageData
    {
        #region Content
        [Required]
        [CultureSpecific]
        [Display(Name = "Title", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string Title { get; set; }

        [CultureSpecific]
        [Display(Name = "Summary", GroupName = SystemTabNames.Content, Order = 20)]
        [UIHint(UIHint.Textarea)]
        public virtual string Summary { get; set; }

        [UIHint(UIHint.Image)]
        [Display(Name = "Thumbnail",
            Description = "Thumbnail image is used for Social platform Image.",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        public virtual ContentReference Thumbnail { get; set; }

        [UIHint(UIHint.Image)]
        [Display(Name = "Background Image",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 40)]
        public virtual ContentReference BackgroundImage { get; set; }

        //[UIHint("Tags")]
        //[CultureSpecific]
        //[GetaTags(AllowSpaces = true, AllowDuplicates = false, CaseSensitive = false)]
        //[Display(Name = "Tags", GroupName = SystemTabNames.Content, Order = 50)]
        //public virtual string Tags { get; set; }

        [CultureSpecific]
        [Display(Name = "Top content area", GroupName = SystemTabNames.Content, Order = 190)]
        public virtual ContentArea TopContentArea { get; set; }

        [CultureSpecific]
        [Display(Name = "Main content area", GroupName = SystemTabNames.Content, Order = 200)]
        public virtual ContentArea MainContentArea { get; set; }

        [CultureSpecific]
        [Display(Name = "Bottom content area", GroupName = SystemTabNames.Content, Order = 210)]
        public virtual ContentArea BottomContentArea { get; set; }

        //[Categories]
        //[AllowedTypes(typeof(HeadlessBaseCategoryData))]
        //[Display(GroupName = SystemTabNames.PageHeader, Order = 40)]
        //public virtual IList<ContentReference> Categories { get; set; }

        #endregion

        #region SEO

        [CultureSpecific]
        [Display(Name = "Meta Title",
            GroupName = PropertyGroupNames.SEO,
            Order = 10)]
        public virtual string MetaTitle { get; set; }

        [CultureSpecific]
        [Display(Name = "Meta Keywords",
            GroupName = PropertyGroupNames.SEO,
            Order = 20)]
        [UIHint("StringList")]
        public virtual string MetaKeywords { get; set; }

        [CultureSpecific]
        [Display(Name = "Meta Description",
            GroupName = PropertyGroupNames.SEO,
            Order = 30)]
        public virtual string MetaDescription { get; set; }

        [Display(Name = "Disable Indexing",
            GroupName = PropertyGroupNames.SEO,
            Order = 40)]
        public virtual bool DisableIndexing { get; set; }

        [Display(Name = "Enable No Follow",
            GroupName = PropertyGroupNames.SEO,
            Order = 50)]
        public virtual bool EnableNoFollow { get; set; }

        [Display(Name = "Og Content Type",
            GroupName = PropertyGroupNames.SEO,
            Order = 60)]
        [UIHint("OgContentType")]
        public virtual string OgContentType { get; set; }

        #endregion

        #region Search
        /// <summary>
        ///     use title field instead of page name in search index
        /// </summary>
        public string SearchTitle
        {
            get
            {
                return !string.IsNullOrEmpty(Title) ? Title : Name;
            }
        }

        /// <summary>
        ///     use Summary field in search index
        /// </summary>
        public string SearchSummary
        {
            get
            {
                return !string.IsNullOrEmpty(Summary) ? Summary : Title;
            }
        }
        #endregion

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            OgContentType = "website";
        }
    }
}
