using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.Web.Routing;
using EPiServer;
using Microsoft.AspNetCore.Http.HttpResults;
using StarterKit.HeadLess.Core.Models.Factories;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StarterKit.HeadLess.Core.Extensions
{
    public static class ContentReferenceExtensions
    {
        private static readonly Lazy<IUrlResolver> _urlResolver = LazyFactory.CreateInstance<IUrlResolver>();
        private static readonly Lazy<IContentLoader> _contentLoader = LazyFactory.CreateInstance<IContentLoader>();

        public static T GetContent<T>(this ContentReference contentReference) where T : IContent
        {
            if (ContentReference.IsNullOrEmpty(contentReference))
                return default;

            return _contentLoader.Value.Get<T>(contentReference);
        }     

        public static string GetAbsoluteContentUrl(this ContentReference contentReference) => GetAbsoluteContentUrl(contentReference, ContentLanguage.PreferredCulture.Name);

        public static string GetAbsoluteContentUrl(this ContentReference contentReference, string language)
        {
            if (contentReference.IsNullOrEmpty())
                return null;

            var contentUrl = _urlResolver
                                    .Value
                                    .GetUrl(contentReference, language, new UrlResolverArguments { ForceAbsolute = true });

            if (!string.IsNullOrEmpty(contentUrl))
                contentUrl = contentUrl.ToLowerInvariant();

            return contentUrl;
        }

        public static string GetContentUrl(this ContentReference contentReference, string language = null, string action = null, NameValueCollection query = null)
        {
            if (contentReference.IsNullOrEmpty())
                return null;

            if (string.IsNullOrEmpty(language))
                language = ContentLanguage.PreferredCulture.Name;

            var contentUrl = _urlResolver
                                    .Value
                                    .GetUrl(contentReference, language);

            if (!string.IsNullOrEmpty(contentUrl))
                contentUrl = contentUrl.ToLowerInvariant();

            if (action != null)
                contentUrl = $"{contentUrl}{action.ToLower()}/";

            if (query != null && query.Count > 0)
            {
                var queryString = HttpUtility.ParseQueryString(string.Empty);

                foreach (string key in query)
                    queryString.Set(key, query[key]);

                contentUrl = $"{contentUrl}?{queryString}";
            }

            return contentUrl;
        }

        public static IList<T> GetFilteredItemsOfType<T>(this ContentArea contentArea) where T : IContentData
        {
            var items = new List<T>();

            if (contentArea == null || contentArea.IsEmpty)
                return items;

            foreach (var contentAreaItem in contentArea.FilteredItems)
            {
                if (!_contentLoader.Value.TryGet(contentAreaItem.ContentLink, out IContentData item))
                {
                    if (contentAreaItem.InlineBlock is null)
                        continue;

                    item = contentAreaItem.InlineBlock;
                }

                if (item is T t)
                    items.Add(t);
            }

            return items;
        }

        public static bool IsContentAreaItemOfType<T>(this ContentAreaItem contentAreaItem)
        {
            if (_contentLoader.Value.TryGet(contentAreaItem?.ContentLink, out IContentData data))
                return data is T;

            return false;
        }

        public static bool IsNullOrEmpty(this ContentArea contentArea, bool isPersonalized = true)
        {
            if (contentArea == null)
            {
                return true;
            }

            var items = isPersonalized ? contentArea.FilteredItems : contentArea.Items;
            return items == null || !items.Any();
        }

        public static bool IsNullOrEmpty(this ContentReference contentReference)
        {
            return contentReference == null || ContentReference.IsNullOrEmpty(contentReference);
        }

        public static bool IsNullOrEmpty(this XhtmlString xHtmlString)
        {
            return xHtmlString == null || xHtmlString.IsEmpty;
        }

        public static ContentReference GetContentByUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            var path = new UrlBuilder(url).Path;

            return _urlResolver.Value.Route(new UrlBuilder(path))?.ContentLink;
        }

        #region commerce 
        //public static IEnumerable<T> GetAllChildren<T>(this ContentReference contentLink, CultureInfo culture) where T : CatalogContentBase
        //{
        //    foreach (var nodeContent in GetChildren<NodeContent>(contentLink, culture))
        //        foreach (var entry in GetAllChildren<T>(nodeContent.ContentLink, culture))
        //            yield return entry;

        //    foreach (var entry in GetChildren<T>(contentLink, culture))
        //        yield return entry;
        //}

        //public static IEnumerable<T> GetAllParentChildren<T>(this ContentReference contentLink, CultureInfo culture) where T : CatalogContentBase
        //{
        //    foreach (var entry in GetChildren<T>(contentLink, culture))
        //        yield return entry;
        //}

        //private static IEnumerable<T> GetChildren<T>(ContentReference contentLink, CultureInfo culture) where T : IContent
        //{
        //    var startIndex = 0;
        //    var maxRows = 100;

        //    while (true)
        //    {
        //        var children = _contentLoader.Value.GetChildren<T>(contentLink, culture, startIndex, maxRows);

        //        if (!children.Any())
        //            yield break;

        //        foreach (var child in children)
        //        {
        //            if (!contentLink.CompareToIgnoreWorkID(child.ParentLink))
        //                continue;

        //            yield return child;
        //        }

        //        startIndex += maxRows;
        //    }
        //}
        #endregion
    }
}
