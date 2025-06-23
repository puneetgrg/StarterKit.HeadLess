using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces.Search;
using StarterKit.HeadLess.Core.Models;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Search
{
    public class SearchResultTileViewModel : ISearchResultTileViewModel
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public /*IHeadlessImageModel*/ ImageModel Thumbnail { get; set; }

        public ICallToActionViewModel Cta { get; set; }
        //Only has value when generated for the search results
        public string TrackingId { get; set; }
    }
}
