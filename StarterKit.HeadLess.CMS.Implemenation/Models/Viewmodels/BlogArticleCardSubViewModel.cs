using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;
using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using StarterKit.HeadLess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels
{
    public class BlogArticleCardSubViewModel : IBlogArticleCardSubViewModel
    {
        public List<string> ContentType { get; set; }
        public ContentModelReference ContentLink { get; set; }
        public string Title { get; set; }
        public string TitleUrl { get; set; }
        public string ShortDescription { get; set; }
        public ImageModel ImageData { get; set; }
        public IEnumerable<ICategoryTagViewModel> Categories { get; set; }
        public bool ShowCategory { get; set; }
        public string ReadTime { get; set; }
        public ICallToActionViewModel Cta { get; set; }
        //Only has value when generated for the search results
        public string TrackingId { get; set; }

        public BlogArticleCardSubViewModel(HeadlessBasePageData headelessPage)
        {
            ContentType = new List<string> { "Block", nameof(BlogArticlePage) }; // Use Block when the page is designated to be used inside a content area to make easier to the FE 

            var pageContent = headelessPage as IContent;

            if (pageContent != null)
            {
                ContentLink = new ContentModelReference
                {
                    Id = pageContent.ContentLink?.ID,
                    GuidValue = new Guid?(pageContent.ContentGuid)
                };
            }
        }
    }
}
