
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Geta.Optimizely.Categories;
using StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using IUrlResolver = EPiServer.Web.Routing.IUrlResolver;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Extensions
{
    public static class ContentExtensions
    {
        private static readonly Lazy<IUrlResolver> UrlResolver =
            new Lazy<IUrlResolver>(() => ServiceLocator.Current.GetInstance<IUrlResolver>());

        private static readonly Lazy<ISiteDefinitionResolver> SiteDefinitionResolver =
            new Lazy<ISiteDefinitionResolver>(() => ServiceLocator.Current.GetInstance<ISiteDefinitionResolver>());

        private static readonly Lazy<IContentLoader> contentLoader =
            new Lazy<IContentLoader>(() => ServiceLocator.Current.GetInstance<IContentLoader>());

        /// <summary>
        ///     Helper method to get a URL string for an IContent
        /// </summary>
        /// <param name="content">The routable content item to get the URL for.</param>
        /// <param name="isAbsolute">Whether the full URL including protocol and host should be returned.</param>
        public static string GetUrl<T>(this T content, bool isAbsolute = false) where T : IContent, ILocale, IRoutable
        {
            try
            {
                return GetUri(content.ContentLink, content.Language.Name, isAbsolute).ToString();
            }
            catch
            {
                //do nothing
            }

            return string.Empty;
        }

        public static Uri GetUri(this ContentReference contentRef, string lang, bool isAbsolute = false)
        {
            try
            {
                var urlString =
                UrlResolver?.Value?.GetUrl(contentRef, lang, new UrlResolverArguments { ForceCanonical = true }) ?? "";
                if (string.IsNullOrEmpty(urlString))
                {
                    return null;
                }

                //if we're not getting an absolute URL, we don't need to work out the correct host name so exit here
                var uri = new Uri(urlString, UriKind.RelativeOrAbsolute);
                if (uri.IsAbsoluteUri || !isAbsolute)
                {
                    return uri;
                }

                //Work out the correct domain to use from the hosts defined in the site definition
                var siteDefinition = SiteDefinitionResolver.Value.GetByContent(contentRef, true, true);
                var host = siteDefinition.Hosts.FirstOrDefault(h => h.Type == HostDefinitionType.Primary) ??
                           siteDefinition.Hosts.FirstOrDefault(h => h.Type == HostDefinitionType.Undefined);

                var temp = $"http{((host?.UseSecureConnection ?? false) ? "s" : string.Empty)}://{host?.Name}";

                var baseUrl = (host?.Name ?? "*").Equals("*")
                    ? siteDefinition.SiteUrl
                    : new Uri(temp);

                return new Uri(baseUrl, urlString);
            }
            catch
            {
                //do nothing
            }


            return new Uri("");
        }

        public static IEnumerable<T> FilterByType<T>(this ContentArea contentArea) where T : IContentData
        {
            if (contentArea == null || contentLoader == null)
            {
                return Enumerable.Empty<T>();
            }

            return contentArea.FilteredItems
                .Select(item => contentLoader.Value.Get<IContent>(item.ContentLink))
                .OfType<T>();
        }

        public static IEnumerable<T> FilterByType<T>(this ContentArea contentArea, ICategoryContentLoader categoryContentLoader) where T : CategoryData
        {
            if (contentArea == null || categoryContentLoader == null)
            {
                return Enumerable.Empty<T>();
            }

            return contentArea.FilteredItems
                .Select(item => categoryContentLoader.Get<CategoryData>(item.ContentLink))
                .OfType<T>();
        }

        public static ICallToActionViewModel CreateCallToActionFromLinkItem(this LinkItem linkItem)
        {
            if (linkItem == null)
                return new CallToActionViewModel();

            return new CallToActionViewModel()
            {
                CtaLabel = linkItem.Text,
                CtaAriaLabel = string.IsNullOrEmpty(linkItem.Title) ? linkItem.Text : linkItem.Title,
                CtaUrl = UrlResolver.Value.GetUrl(linkItem.Href),
                CtaTarget = string.IsNullOrEmpty(linkItem.Target) ? "_self" : linkItem.Target,
            };
        }
    }
}
