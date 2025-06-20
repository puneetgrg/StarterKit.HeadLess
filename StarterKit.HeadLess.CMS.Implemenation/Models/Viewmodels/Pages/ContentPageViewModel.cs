using EPiServer.Core;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels.Pages
{
    public class ContentPageViewModel : IContentPageViewModel
    {
        public string Title { get; set; }

        public XhtmlString Subheadline { get; set; }

        public IEnumerable<IJumpLinkItemViewModel> JumpLinks { get; set; }

        public string BackToTopLabel { get; set; }

        public bool ShowBackTopButton { get; set; }

        public ContentArea MainContentArea { get; set; }

        public ContentArea RightContentArea { get; set; }
    }
}
