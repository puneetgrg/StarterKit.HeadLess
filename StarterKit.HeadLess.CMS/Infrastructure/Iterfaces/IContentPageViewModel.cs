using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Infrastructure.Iterfaces
{
    public interface IContentPageViewModel
    {
        string Title { get; set; }
        XhtmlString Subheadline { get; set; }

        IEnumerable<IJumpLinkItemViewModel> JumpLinks { get; set; }

        string BackToTopLabel { get; set; }

        bool ShowBackTopButton { get; set; }

        ContentArea MainContentArea { get; set; }

        ContentArea RightContentArea { get; set; }
    }

}
