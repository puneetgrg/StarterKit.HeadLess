using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Newtonsoft.Json;
using StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Editor.SelectionFactory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Blocks
{
    [ContentType(DisplayName = "Call To Action Block",
        GUID = "87623E60-D41E-4237-A177-34DAA696DD98", Description = "Use the Call to Action (CTA) block to add a preformatted but editable CTA button",
        AvailableInEditMode = false)]
   
    public class CallToActionButtonBlock : BlockData
    {
        [JsonIgnore]
        [CultureSpecific]
        [Display(Name = "CTA Link", Description = "Drop content here or \"Create a new link\"", GroupName = SystemTabNames.Content, Order = 10)]
        [ListItems(1, ErrorMessage = "The property CTA Button Link is limited to 1 item(s)")]
        public virtual LinkItemCollection Link { get; set; }

        [ScaffoldColumn(false)]
        [SelectOne(SelectionFactoryType = typeof(ButtonStyleSelectionFactory))]
        [Display(Name = "Style", Description = "Choose a predefined style for the button based on the style guide", GroupName = SystemTabNames.Content, Order = 20)]
        public virtual string Style { get; set; }

        [CultureSpecific]
        [Display(Name = "CTA Icon Alignment", Description = "",
            GroupName = SystemTabNames.Content, Order = 30)]
        [SelectOne(SelectionFactoryType = typeof(AlignmentSelectionFactory))]
        public virtual string Alignment { get; set; }

        [JsonIgnore]
        [UIHint(UIHint.Image)]
        [Display(Name = "CTA Icon",
            Description = "Navigate to the button folder to choose your icon. The recommendation is to use only a \"thin\" icon. ",
            Order = 40)]
        public virtual ContentReference Icon { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            //Style = ButtonStyleClasses.PrimaryButton;
            //Alignment = Infrastructure.Constant.Alignment.RightAlignment.ToLower();
        }
    }
}
