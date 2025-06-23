
using StarterKit.HeadLess.Core.Models;

namespace StarterKit.HeadLess.CMS.Infrastructure.Iterfaces.Search
{
    public interface ISearchResultTileViewModel
    {
        string Title { get; set; }

        string ShortDescription { get; set; }

        ImageModel Thumbnail { get; set; }

        ICallToActionViewModel Cta { get; set; }

        //Only has value when generated for the search results
        string TrackingId { get; set; }
    }
}
