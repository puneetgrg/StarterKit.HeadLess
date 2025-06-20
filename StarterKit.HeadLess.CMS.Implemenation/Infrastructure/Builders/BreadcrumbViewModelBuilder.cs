using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.Web.Routing;
using EPiServer.Web;
using EPiServer;
using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using StarterKit.HeadLess.CMS.Infrastructure.Builders;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Builders
{
    public class BreadcrumbViewModelBuilder : IBreadcrumbViewModelBuilder
    {
        private readonly IContentLoader _contentLoader;
        public readonly IUrlResolver _urlResolver;

        public BreadcrumbViewModelBuilder(IContentLoader contentLoader, IUrlResolver urlResolver)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
        }

        public IBreadcrumbViewModel GenerateBreadcrumbs(PageData pageData)//add another method for webproducts
        {
            var currentPage = _contentLoader.Get<HeadlessBasePageData>(pageData.ContentLink);
            var ancestors = _contentLoader.GetAncestors(pageData.ContentLink).OfType<HeadlessBasePageData>().Reverse().ToList();

            var urlResolverArguments = new UrlResolverArguments
            {
                ContextMode = ContextMode.Default,
                ForceAbsolute = true
            };

            var model = new BreadcrumbViewModel();

            if (ancestors.Count != 0)
            {
                foreach (var (page, url) in from ancestor in ancestors
                                            let url = _urlResolver.GetUrl(ancestor.ContentLink, ContentLanguage.PreferredCulture.Name, urlResolverArguments)
                                            let page = _contentLoader.Get<HeadlessBasePageData>(ancestor.ContentLink)
                                            select (page, url))
                {

                    var item = new BreadcrumbItemViewModel(page)
                    {
                        Url = url
                    };

                    if (!string.IsNullOrEmpty(url))
                        model.Breadcrumbs.Add(item);
                }
            }

            //breadcrumb of the current page
            if (model.Breadcrumbs.Any())
            {
                var currentBreadcrumb = new BreadcrumbItemViewModel(currentPage);
                model.Breadcrumbs.Add(currentBreadcrumb);
            }

            return model;
        }
    }
}
