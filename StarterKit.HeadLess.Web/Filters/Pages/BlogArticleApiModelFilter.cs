using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ServiceLocation;
using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using StarterKit.HeadLess.CMS.Infrastructure.Builders;
using StarterKit.HeadLess.CMS.Infrastructure.Managers;
using EPiServer.Logging;

namespace StarterKit.HeadLess.Web.Filters.Pages
{
    [ServiceConfiguration(ServiceType = typeof(IContentApiModelFilter), Lifecycle = ServiceInstanceScope.Singleton)]
    internal class BlogArticleApiModelFilter : BasePageApiModelFilter
    {
        private readonly IContentLoader _contentLoader;
        private readonly IPageViewModelBuilder _pageViewModelBuilder;
        private static readonly EPiServer.Logging.ILogger _logger = LogManager.GetLogger(typeof(BlogArticleApiModelFilter));

        public BlogArticleApiModelFilter(IContentLoader contentLoader, ISEOManager seoManager, IBlockViewModelBuilder blockViewModelBuilder, IBreadcrumbViewModelBuilder breadcrumbViewModelBuilder,IPageViewModelBuilder pageViewModelBuilder)
            : base(contentLoader, seoManager, blockViewModelBuilder, breadcrumbViewModelBuilder,  pageViewModelBuilder)
        {
            _contentLoader = contentLoader;
            _pageViewModelBuilder = pageViewModelBuilder;
        }

        public override void Filter(ContentApiModel contentApiModel, ConverterContext converterContext)
        {
            try
            {
                if (!_contentLoader.TryGet(converterContext.ContentReference, out BlogArticlePage blogArticle) || blogArticle is not BlogArticlePage)
                {
                    return;
                }

                //Remove the base properties from the contentApiModel
                contentApiModel.Properties.Remove(nameof(blogArticle.ShortDescription));
                contentApiModel.Properties.Remove(nameof(blogArticle.ReadTime));
                contentApiModel.Properties.Remove(nameof(blogArticle.RichText));
                contentApiModel.Properties.Remove(nameof(blogArticle.Author));
                contentApiModel.Properties.Remove(nameof(blogArticle.Categories));

                var viewModel = _pageViewModelBuilder.GetBlogArticlePageViewModel(blogArticle);

                //AddSeoMetaData(blogArticle, contentApiModel);
                AddBreadcrumbs(blogArticle, contentApiModel);
                //AddHeader(contentApiModel);
                //AddFooter(blogArticle, contentApiModel);
                //AddQuickLinks(contentApiModel);

                contentApiModel.Properties.Add(nameof(viewModel.Thumbnail), viewModel.Thumbnail);

                if (!string.IsNullOrEmpty(viewModel.ShortDescription))
                {
                    contentApiModel.Properties.Add(nameof(viewModel.ShortDescription), viewModel.ShortDescription);
                }

                if (viewModel.Categories.Any())
                {
                    contentApiModel.Properties.Add(nameof(viewModel.Categories), viewModel.Categories);
                }

                if (viewModel.SearchCategories.Any())
                {
                    contentApiModel.Properties.Add(nameof(viewModel.SearchCategories), viewModel.SearchCategories); //used when filtering pages in Search
                }

                if (!string.IsNullOrEmpty(viewModel.ReadTime))
                {
                    contentApiModel.Properties.Add(nameof(viewModel.ReadTime), viewModel.ReadTime);
                }

                if (!string.IsNullOrEmpty(viewModel.Author))
                {
                    contentApiModel.Properties.Add(nameof(viewModel.Author), viewModel.Author);
                }

                contentApiModel.Properties.Add(nameof(viewModel.RichText), viewModel.RichText);

                //The logic to determine if the Bottom content area is coming from the page or the site settings is in the ViewModelBuilder
                AddContentAreaItemsToContentApiModel(nameof(viewModel.BottomContentArea), viewModel.BottomContentArea, contentApiModel);

                AddContentAreaItemsToContentApiModel(nameof(viewModel.RightContentArea), viewModel.RightContentArea, contentApiModel);
            }
            catch (Exception ex)
            {
                _logger.Error("{className}:: {error}", $"{this.GetType().Name}", ex.Message);
            }
        }
    }
}
