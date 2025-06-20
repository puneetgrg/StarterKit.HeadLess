using EPiServer.ContentApi.Core.Serialization.Internal;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.ContentApi.Core.Serialization;
using StarterKit.HeadLess.CMS.Implemenation.Models.Blocks;
using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using StarterKit.HeadLess.CMS.Infrastructure.Builders;
using EPiServer.ServiceLocation;
using StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Builders;
using StarterKit.HeadLess.CMS.Infrastructure.Managers;
using EPiServer.Logging;

namespace StarterKit.HeadLess.Web.Filters.Pages
{
    [ServiceConfiguration(ServiceType = typeof(IContentApiModelFilter), Lifecycle = ServiceInstanceScope.Singleton)]
    internal class StartPageApiModelFilter : BasePageApiModelFilter
    {
        private readonly IContentLoader _contentLoader;
        private readonly IDisplayViewModelBuilder _displayViewModelBuilder;
        private static readonly EPiServer.Logging.ILogger _logger = LogManager.GetLogger(typeof(StartPageApiModelFilter));

        public StartPageApiModelFilter(IContentLoader contentLoader, ISEOManager seoManager, IBlockViewModelBuilder blockViewModelBuilder,
            IDisplayViewModelBuilder displayViewModelBuilder , IBreadcrumbViewModelBuilder breadcrumbViewModelBuilder) :
            base(contentLoader, seoManager, blockViewModelBuilder , breadcrumbViewModelBuilder)
        {
            _contentLoader = contentLoader;
            _displayViewModelBuilder = displayViewModelBuilder;
        }

        public override void Filter(ContentApiModel contentApiModel, ConverterContext converterContext)
        {
            try
            {
                if (!_contentLoader.TryGet(converterContext.ContentReference, out StartPage startPage) || startPage is not StartPage)
                {
                    return;
                }

                //Remove the base properties from the contentApiModel
                contentApiModel.Properties.Remove(nameof(startPage.BackgroundImage));

                var backgroundImage = _displayViewModelBuilder.GetImage(startPage.BackgroundImage);

                //AddSeoMetaData(startPage, contentApiModel);
                AddBreadcrumbs(startPage, contentApiModel);
                //AddHeader(contentApiModel);
                //AddFooter(startPage, contentApiModel);
                //AddQuickLinks(contentApiModel);

                if (!string.IsNullOrEmpty(backgroundImage.Src))
                {
                    contentApiModel.Properties.Add(nameof(backgroundImage), backgroundImage);
                }

                AddContentAreaItemsToContentApiModel(nameof(startPage.MainContentArea), startPage.MainContentArea, contentApiModel);
                AddContentAreaItemsToContentApiModel(nameof(startPage.BottomContentArea), startPage.BottomContentArea, contentApiModel);
            }
            catch (Exception ex)
            {
                _logger.Error("{className}:: {error}", $"{this.GetType().Name}", ex.Message);
            }
        }
    }
}
