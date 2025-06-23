using Comptia.Headless.CMS.Implementation.Models.Search;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Extensions.Find;
using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using StarterKit.HeadLess.CMS.Implemenation.Models.Search;
using StarterKit.HeadLess.CMS.Infrastructure.Builders;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces.Search;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using EPiServer.Logging;
using EPiServer.Find.Statistics;
using StarterKit.HeadLess.CMS.Implemenation.Infrastructure.ModelBinders;

namespace StarterKit.HeadLess.CMS.Implemenation.SearchApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchApiController : ControllerBase
    {
        private readonly IHeadlessSearchManager _headlessSearchManager;
        private readonly IBlockViewModelBuilder _blockViewModelBuilder;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IDataProtector _dataProtector;
        private const string _purpose = "tracking";

        private static readonly ILogger _logger = LogManager.GetLogger(typeof(SearchApiController));

        public SearchApiController(IHeadlessSearchManager headlessSearchManager, IBlockViewModelBuilder blockViewModelBuilder, IDataProtectionProvider dataProtectionProvider)
        {
            _headlessSearchManager = headlessSearchManager;
            _blockViewModelBuilder = blockViewModelBuilder;
            _dataProtectionProvider = dataProtectionProvider;

            _dataProtector = _dataProtectionProvider.CreateProtector(_purpose);
        }

        [HttpGet("GetBlogArticles")]
        public ActionResult<HeadlessSearchResultModel> GetBlogArticles([FromQuery] HeadlessSearchRequest headlessSearchRequest)
        {
            if (!string.IsNullOrEmpty(Request.Headers["Accept-Language"]))
                headlessSearchRequest.Language = Request.Headers["Accept-Language"];

            var searchResults = _headlessSearchManager.SearchContent<BlogArticlePage>(headlessSearchRequest);

            var response = new HeadlessSearchResultModel
            {
                TotalMatching = searchResults.TotalMatching,
                Results = searchResults.TotalMatching > 0 ?
                    searchResults.Results.Select(x => _blockViewModelBuilder.GetBlogArticleCardSubViewModel(x as BlogArticlePage, headlessSearchRequest.Language, _dataProtector.Protect(_headlessSearchManager.GenerateTrackingId(x as BlogArticlePage))))
                    : new List<IBlogArticleCardSubViewModel>()
            };

            return Ok(response);
        }


        [HttpGet]
        [Route("GetContent")]
        public ActionResult<HeadlessSearchResultModel> GetContent([ModelBinder(BinderType = typeof(HeadlessSearchRequestBinder))] HeadlessSearchRequest headlessSearchRequest)
        {
            if (!string.IsNullOrEmpty(Request.Headers["Accept-Language"]))
                headlessSearchRequest.Language = Request.Headers["Accept-Language"];

            var searchResults = _headlessSearchManager.SearchContent<HeadlessBasePageData>(headlessSearchRequest);

            var response = new HeadlessSearchResultModel
            {
                TotalMatching = searchResults.TotalMatching,
                Results = searchResults.TotalMatching > 0 ?
                    searchResults.Results.Select(x => _blockViewModelBuilder.GetSearchResultTile(x as HeadlessBasePageData, headlessSearchRequest.Language, _dataProtector.Protect(_headlessSearchManager.GenerateTrackingId(x as HeadlessBasePageData))))
                    : new List<ISearchResultTileViewModel>()
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("TrackResult")]
        public void TrackResult([FromBody] HeadlessSearchTrackingRequest searchTrackingRequest)
        {
            if (string.IsNullOrEmpty(searchTrackingRequest.Query) || string.IsNullOrEmpty(searchTrackingRequest.TrackingId))
                return;

            try
            {
                var client = _headlessSearchManager.GetClient();
                string query = FindExtensions.EsacpeSepcialCharacters(searchTrackingRequest.Query);
                var trackingId = _dataProtector.Unprotect(searchTrackingRequest.TrackingId);

                client.Statistics().TrackHit(query, trackingId);
            }
            catch (Exception ex)
            {
                _logger.Error("SearchApiController:: ", ex.Message);
            }
        }
    }
}
