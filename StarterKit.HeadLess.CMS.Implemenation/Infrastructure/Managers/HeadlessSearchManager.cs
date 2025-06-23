using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces.Search;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;

using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Framework.Statistics;
using StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Extensions.Find;
using StarterKit.HeadLess.CMS.Implemenation.Models.Search;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Managers
{
    public class HeadlessSearchManager : IHeadlessSearchManager
    {
        private readonly IClient _findClient;

        public HeadlessSearchManager(IClient findClient) => _findClient = findClient;

        public IClient GetClient()
        {
            return _findClient;
        }

        public IHeadlessSearchResultModel SearchContent<T>(IHeadlessSearchRequest searchRequest) where T : PageData
        {
            var searchResults = CreateBaseQuery<T>(searchRequest)
                .FliterBySearchCategories(searchRequest.Categories)
                .SortBy(searchRequest.Sort)
                .Track()
                .GetContentResult();  //cached by default for 600 sec

            return new HeadlessSearchResultModel
            {
                TotalMatching = searchResults.TotalMatching,
                Results = searchResults.ToList()
            };
        }

        private ITypeSearch<T> CreateBaseQuery<T>(IHeadlessSearchRequest searchRequest) where T : PageData
        {
            //for now, the default is in en
            List<string> languages = new List<string>() { searchRequest.Language };

            var query = _findClient.Search<T>()
                .FliterBySearchTerm(searchRequest.Query)
                .ExcludeDeleted()
                .ExcludeContentFolders()
                .CurrentlyPublished()
                .FilterOnLanguages(languages)
                .Take(searchRequest.Top)
                .Skip(searchRequest.Skip);

            return query;
        }

        public string GenerateTrackingId(PageData page)
        {
            try
            {
                return _findClient.Conventions.TypeNameConvention.GetTypeName(page.GetType()) + "/" + _findClient.Conventions.IdConvention.GetId(page);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
