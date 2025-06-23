using EPiServer.Core;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using StarterKit.HeadLess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels.Pages
{
    public class BlogArticlePageViewModel : IBlogArticlePageViewModel
    {
        public IEnumerable<ICategoryTagViewModel> Categories { get; set; }

        public IEnumerable<string> SearchCategories { get; set; }

        public IEnumerable<string> Tags { get; set; }

        //public IHeadlessImageModel Thumbnail { get; set; }
        public ImageModel Thumbnail { get; set; }

        public string ShortDescription { get; set; }

        public string ReadTime { get; set; }

        public string Author { get; set; }

        public XhtmlString RichText { get; set; }

        public ContentArea BottomContentArea { get; set; }

        public ContentArea RightContentArea { get; set; }
    }
}
