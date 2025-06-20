using EPiServer.Logging;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ServiceLocation;
using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using StarterKit.HeadLess.CMS.Infrastructure.Builders;
using StarterKit.HeadLess.CMS.Infrastructure.Managers;
using StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Builders;

namespace StarterKit.HeadLess.Web.Filters.Pages
{
    [ServiceConfiguration(ServiceType = typeof(IContentApiModelFilter), Lifecycle = ServiceInstanceScope.Singleton)]
    internal class ContentPageApiModelFilter : BasePageApiModelFilter
    {
        private readonly IContentLoader _contentLoader;
        private readonly IPageViewModelBuilder _pageViewModelBuilder;
        private static readonly EPiServer.Logging.ILogger _logger = LogManager.GetLogger(typeof(ContentPageApiModelFilter));

        public ContentPageApiModelFilter(IContentLoader contentLoader, ISEOManager seoManager, IBlockViewModelBuilder blockViewModelBuilder, IBreadcrumbViewModelBuilder breadcrumbViewModelBuilder, 
            IPageViewModelBuilder pageViewModelBuilder)
              : base(contentLoader, seoManager, blockViewModelBuilder, breadcrumbViewModelBuilder,  pageViewModelBuilder)
        {
            _contentLoader = contentLoader;
            _pageViewModelBuilder = pageViewModelBuilder;
        }

        public override void Filter(ContentApiModel contentApiModel, ConverterContext converterContext)
        {

            try
            {
                if (!_contentLoader.TryGet(converterContext.ContentReference, out ContentPage contentPage) || contentPage is not ContentPage)
                {
                    return;
                }

                //Remove the base properties from the contentApiModel
                contentApiModel.Properties.Remove(nameof(contentPage.Title));
                contentApiModel.Properties.Remove(nameof(contentPage.Subheadline));
                contentApiModel.Properties.Remove(nameof(contentPage.ShowBackTopButton));
                contentApiModel.Properties.Remove(nameof(contentPage.JumpLinks));

                var viewModel = _pageViewModelBuilder.GetContentPageViewModel(contentPage);

                //AddSeoMetaData(contentPage, contentApiModel);
                AddBreadcrumbs(contentPage, contentApiModel);
                //AddQuickLinks(contentApiModel);
                //AddHeader(contentApiModel);
                //AddFooter(contentPage, contentApiModel);

                contentApiModel.Properties.Add(nameof(viewModel.Title), viewModel.Title);
                contentApiModel.Properties.Add(nameof(viewModel.Subheadline), viewModel.Subheadline);
                contentApiModel.Properties.Add(nameof(viewModel.JumpLinks), viewModel.JumpLinks);
                contentApiModel.Properties.Add(nameof(viewModel.BackToTopLabel), viewModel.BackToTopLabel);
                contentApiModel.Properties.Add(nameof(viewModel.ShowBackTopButton), viewModel.ShowBackTopButton);
                AddContentAreaItemsToContentApiModel(nameof(viewModel.RightContentArea), viewModel.RightContentArea, contentApiModel);
                AddContentAreaItemsToContentApiModel(nameof(viewModel.MainContentArea), viewModel.MainContentArea, contentApiModel);

            }
            catch (Exception ex)
            {
                _logger.Error("{className}:: {error}", $"{this.GetType().Name}", ex.Message);
            }
        }
    }
}
