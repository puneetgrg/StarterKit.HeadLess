using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using StarterKit.HeadLess.CMS.Implemenation.Models.Properties;
using System.ComponentModel.DataAnnotations;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Pages
{
    [ContentType(DisplayName = "Content Page", GUID = "E278C8FD-F5AD-468E-9995-980B1C4C5AD6", Description = "", GroupName = SystemTabNames.Content)]
   
    [AvailableContentTypes(Availability.Specific, Include = new[] { typeof(HeadlessBasePageData) })]
    public class ContentPage : HeadlessBasePageData
    {
        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(Name = "Subheadline", Description = "Please limit your input to a maximum 168 characters in English, 211 characters in non-English to respect the propriety of the design.",
            GroupName = SystemTabNames.Content, Order = 40)]
        public virtual XhtmlString Subheadline { get; set; }

        [Display(Name = "Show back to top button",
            Description = "Check the box if you want to display a \"back to the top button” on the page.\"",
            Order = 50)]
        public virtual bool ShowBackTopButton { get; set; }

        [Display(Name = "Jump Links",
            Description ="",Order = 60)]
        [EditorDescriptor(EditorDescriptorType = typeof(CollectionEditorDescriptor<JumpLink>))]
        public virtual IList<JumpLink> JumpLinks { get; set; }

        [CultureSpecific]
        [Display(Name = "Right content area", GroupName = SystemTabNames.Content, Order = 150)]      
        public virtual ContentArea RightContentArea { get; set; }

        [Ignore]
        public override ContentArea TopContentArea { get; set; }

        [Ignore]
        public override ContentArea BottomContentArea { get; set; }

        [Ignore]
        public override ContentReference BackgroundImage { get; set; }
    }
}
