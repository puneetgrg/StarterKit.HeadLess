using EPiServer.ContentApi.Core.Serialization.Internal;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ServiceLocation;
using StarterKit.HeadLess.CMS.Implemenation.Models.Blocks;
using StarterKit.HeadLess.CMS.Infrastructure.Builders;
using StarterKit.HeadLess.CMS.Infrastructure.Managers;

namespace StarterKit.HeadLess.Web.Filters.Pages
{
    /// <summary>
    /// This base class for all the shared code in all page types ContentApiModelFilter
    /// </summary>
    [ServiceConfiguration(ServiceType = typeof(IContentApiModelFilter), Lifecycle = ServiceInstanceScope.Singleton)]
    public class BasePageApiModelFilter : ContentApiModelFilter<ContentApiModel>
    {
        private readonly IContentLoader _contentLoader;
        private readonly ISEOManager _seoManager;
        private readonly IBlockViewModelBuilder _blockViewModelBuilder;
        private readonly IBreadcrumbViewModelBuilder _breadcrumbViewModelBuilder;

        //private readonly IPageViewModelBuilder _pageViewModelBuilder;
        //private readonly IFormsViewModelBuilder _formsViewModelBuilder;

        public BasePageApiModelFilter(IContentLoader contentLoader, ISEOManager seoManager, IBlockViewModelBuilder blockViewModelBuilder, IBreadcrumbViewModelBuilder breadcrumbViewModelBuilder)
        {
            _contentLoader = contentLoader;
            _blockViewModelBuilder = blockViewModelBuilder;
            _seoManager = seoManager;
            _breadcrumbViewModelBuilder = breadcrumbViewModelBuilder;
        }

        public override void Filter(ContentApiModel contentApiModel, ConverterContext converterContext) { }

        public void AddContentAreaItemsToContentApiModel(string key, ContentArea contentArea, ContentApiModel contentApiModel)
        {
            var items = new Dictionary<string, object>();

            if (contentArea?.FilteredItems == null)
            {
                contentApiModel.Properties.Add(key, items);
                return;
            }

            var index = 0; // Prevent duplicate keys in case of multiple blocks of the same type

            foreach (var contentAreaItem in contentArea.FilteredItems)
            {
                if (!_contentLoader.TryGet<IContent>(contentAreaItem.ContentLink, out var content))
                {
                    continue;
                }

                var viewModel = GetViewModelForContent(content);

                if (viewModel == null)
                {
                    continue;
                }

                var contentName = content.GetType().BaseType?.Name ?? content.GetType().Name;
                string itemKey = $"{contentName}_{index}";
                items.Add(itemKey, viewModel);
                index++;
            }

            contentApiModel.Properties.Add(key, items);
        }

        private object GetViewModelForContent(IContent content)
        {
            return content switch
            {
                CallToActionButtonBlock block => _blockViewModelBuilder.GetCallToActionViewModel(block),
                PageBannerBlock block => _blockViewModelBuilder.GetPageBannerViewModel(block),
                _ => null
            };
        }

        public void AddBreadcrumbs(PageData pageData, ContentApiModel contentApiModel)
        {
            var breadcrumbs = _breadcrumbViewModelBuilder.GenerateBreadcrumbs(pageData);
            contentApiModel.Properties.Add(nameof(breadcrumbs), breadcrumbs.Breadcrumbs);
        }
    //public void AddSeoMetaData(PageData pageData, ContentApiModel contentApiModel)
    //{
    //    contentApiModel.Properties.Add("htmlLanguage", pageData.Language.Name);

    //    //Href Lang
    //    var hrefLang = _seoManager.CreateHrefLangData(pageData);
    //    if (!string.IsNullOrEmpty(hrefLang.ToString()))
    //    {
    //        contentApiModel.Properties.Add(nameof(hrefLang), hrefLang);
    //    }

    //    //Canonicals
    //    var canonicals = _seoManager.CreateLowerCaseCanonicalLink(pageData);
    //    if (!string.IsNullOrEmpty(canonicals.ToString()))
    //    {
    //        contentApiModel.Properties.Add(nameof(canonicals), canonicals);
    //    }

    //    //OGTags
    //    var ogTags = _seoManager.CreateOgTags(pageData);
    //    if (!string.IsNullOrEmpty(ogTags.ToString()))
    //    {
    //        contentApiModel.Properties.Add(nameof(ogTags), ogTags);
    //    }

    //    //RobotsTags
    //    var robotsTag = _seoManager.CreateRobotsTag(pageData);
    //    if (!string.IsNullOrEmpty(robotsTag.ToString()))
    //    {
    //        contentApiModel.Properties.Add(nameof(robotsTag), robotsTag);
    //    }

    //    //MetaTags
    //    var metaTags = _seoManager.CreateMetaTags(pageData);
    //    if (!string.IsNullOrEmpty(metaTags.ToString()))
    //    {
    //        contentApiModel.Properties.Add(nameof(metaTags), metaTags);
    //    }
    //}



    //public void AddFooter(PageData pageData, ContentApiModel contentApiModel)
    //{
    //    var footer = _pageViewModelBuilder.GetFooterViewModel(pageData);
    //    contentApiModel.Properties.Add(nameof(footer), footer);
    //}

    //public void AddQuickLinks(ContentApiModel contentApiModel)
    //{
    //    var quickLinks = _pageViewModelBuilder.GetQuickLinksViewModel();
    //    contentApiModel.Properties.Add(nameof(quickLinks), quickLinks);
    //}

    //public void AddHeader(ContentApiModel contentApiModel)
    //{
    //    var header = _pageViewModelBuilder.GetHeaderViewModel();
    //    contentApiModel.Properties.Add(nameof(header), header);
    //}
}
}
