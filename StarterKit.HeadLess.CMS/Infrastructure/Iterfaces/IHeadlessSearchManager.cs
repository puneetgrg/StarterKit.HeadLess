using EPiServer.Core;
using EPiServer.Find;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces.Search;

namespace StarterKit.HeadLess.CMS.Infrastructure.Iterfaces
{
    public interface IHeadlessSearchManager
    {
        IClient GetClient();

        IHeadlessSearchResultModel SearchContent<T>(IHeadlessSearchRequest searchRequest) where T : PageData;

        string GenerateTrackingId(PageData page);
    }
}
