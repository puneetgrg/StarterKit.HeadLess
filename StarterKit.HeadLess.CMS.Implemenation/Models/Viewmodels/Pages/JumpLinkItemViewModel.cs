using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels.Pages
{
    public class JumpLinkItemViewModel : IJumpLinkItemViewModel
    {
        public string Title { get; set; }

        public string AnchorId { get; set; }
    }
}
