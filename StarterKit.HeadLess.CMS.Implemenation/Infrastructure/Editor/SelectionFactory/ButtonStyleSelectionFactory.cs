using EPiServer.Shell.ObjectEditing;
using static StarterKit.HeadLess.CMS.Implemenation.Models.Constant;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Editor.SelectionFactory
{
    public class ButtonStyleSelectionFactory : ISelectionFactory
    {
        public virtual IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {
                new SelectItem { Text = ButtonStyle.PrimaryButton, Value = ButtonStyleClasses.PrimaryButton },
                new SelectItem { Text = ButtonStyle.SecondaryButton, Value = ButtonStyleClasses.SecondaryButton },
                new SelectItem { Text = ButtonStyle.TertiaryButton, Value = ButtonStyleClasses.TertiaryButton },
            };
        }
    }
}
