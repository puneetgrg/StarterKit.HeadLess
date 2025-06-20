using EPiServer.Shell.ObjectEditing;
using static StarterKit.HeadLess.CMS.Implemenation.Models.Constant;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Editor.SelectionFactory
{
    public class AlignmentSelectionFactory : ISelectionFactory
    {
        public virtual IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {
                new SelectItem { Text = Alignment.LeftAlignment, Value = Alignment.LeftAlignment.ToLower() },
                new SelectItem { Text = Alignment.RightAlignment, Value = Alignment.RightAlignment.ToLower() }
            };
        }
    }
}
