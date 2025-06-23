using StarterKit.HeadLess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Infrastructure.Iterfaces
{
    public interface IBlogArticleCardSubViewModel
    {
        string Title { get; set; }
        string TitleUrl { get; set; }
        string ShortDescription { get; set; }
        bool ShowCategory { get; set; }
        string ReadTime { get; set; }
        ImageModel ImageData { get; set; }
        ICallToActionViewModel Cta { get; set; }
        IEnumerable<ICategoryTagViewModel> Categories { get; set; }
        //Only has value when generated for the search results
        string TrackingId { get; set; }
    }
}
