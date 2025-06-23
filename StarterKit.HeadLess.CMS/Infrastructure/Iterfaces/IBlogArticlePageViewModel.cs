using EPiServer.Core;
using StarterKit.HeadLess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Infrastructure.Iterfaces
{
    public interface IBlogArticlePageViewModel
    {
        IEnumerable<ICategoryTagViewModel> Categories { get; set; } //Display names for the associated Categories

        IEnumerable<string> SearchCategories { get; set; }//Name for the associated Categories (Mainly used filtering pages using Categories)

        IEnumerable<string> Tags { get; set; }

        ImageModel Thumbnail { get; set; }

        string ShortDescription { get; set; }

        string ReadTime { get; set; }

        string Author { get; set; }

        XhtmlString RichText { get; set; }

        ContentArea BottomContentArea { get; set; }

        ContentArea RightContentArea { get; set; }
    }
}
