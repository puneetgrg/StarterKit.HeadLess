using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.Web.Routing;
using EPiServer.Web;
using EPiServer;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using StarterKit.HeadLess.CMS.Infrastructure.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Managers
{
    public class SEOManager : ISEOManager
    {
      
        private readonly IHttpContextAccessor _httpContext;
        private readonly IContentLoader _contentLoader;
        private readonly UrlResolver _urlResolver;

        public const string CanonicalLinksFormat = "<link href=\"{0}\" rel=\"canonical\" />";
        public const string AlternateLanguageLinksFormat = "<link rel=\"alternate\" href=\"{0}\" hreflang=\"{1}\" />";
        public const string XDefault = "x-default";
        public const string DefaultCulture = "en-US";

        public SEOManager( IHttpContextAccessor httpContext, IContentLoader contentLoader, UrlResolver urlResolver)
        {
            _httpContext = httpContext;
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
        }

        public HtmlString LowerCaseCanonicalLink(IContent currentContent)
        {
            if (_httpContext == null || _httpContext.HttpContext == null)
                return new HtmlString(string.Empty);

            try
            {
                if (currentContent is not PageData || currentContent.ContentLink == null || _httpContext.HttpContext.Request.GetDisplayUrl().Contains("/ErrorHandling/"))
                    return new HtmlString(string.Empty);

              

                if (currentContent is StartPage )
                {
                    return new HtmlString(string.Format(CanonicalLinksFormat, _httpContext.HttpContext?.Request.Scheme + "://" + _httpContext.HttpContext?.Request.Host + "/").ToLower());
                }
                else
                {
                    var virtualPathArguments = new VirtualPathArguments
                    {
                        ContextMode = ContextMode.Default,
                        ForceAbsolute = true
                    };

                    var url = GetUrlWithTrailingSlash(currentContent.ContentLink,
                        ContentLanguage.PreferredCulture.Name, virtualPathArguments);

                    return new HtmlString(string.Format(CanonicalLinksFormat, url.ToLower()));
                }
            }
            catch
            {
                return new HtmlString(string.Empty);
            }
        }

        //public HtmlString CreateHrefLangData(IContent currentContent)
        //{
        //    if (_httpContext == null || _httpContext.HttpContext == null)
        //        return new HtmlString(string.Empty);

        //    if (currentContent is not PageData || currentContent.ContentLink == null || _httpContext.HttpContext.Request.GetDisplayUrl().Contains("/ErrorHandling/"))
        //        return new HtmlString(string.Empty);

        //    var virtualPathArguments = new VirtualPathArguments
        //    {
        //        ContextMode = ContextMode.Default,
        //        ForceAbsolute = true
        //    };

        //    StringBuilder stringBuilder = new StringBuilder();

        //    var enabledCultures = "en";

        //    var currentLanguage = "en";

        //    bool contentExistsInEnglish = false;

        //    //For en-US => generate every alternate since every language falls back to en-US
        //    if (currentLanguage.Equals(DefaultCulture, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        foreach (var (culture, url) in from culture in enabledCultures
        //                                       let url = GetUrlWithTrailingSlash(currentContent.ContentLink, culture.Name, virtualPathArguments)
        //                                       select (culture, url))
        //        {
        //            if (string.IsNullOrEmpty(url))
        //            {
        //                continue;
        //            }

        //            if (culture.Name.Equals(DefaultCulture, StringComparison.InvariantCultureIgnoreCase))
        //            {
        //                if (currentContent is StartPage)
        //                {
        //                    var displayUrl = _httpContext.HttpContext?.Request.GetDisplayUrl();

        //                    //In case of the Home page, follow the URL in the request (with or without the language segement)
        //                    stringBuilder.AppendLine(string.Format(AlternateLanguageLinksFormat, displayUrl, XDefault));
        //                }
        //                else
        //                {
        //                    stringBuilder.AppendLine(string.Format(AlternateLanguageLinksFormat, url, XDefault));
        //                }
        //            }

        //            stringBuilder.AppendLine(string.Format(AlternateLanguageLinksFormat, url, culture.Name.ToLower()));
        //        }
        //    }
        //    //For non en-US => check if the page exists before adding it to the generated list 
        //    else
        //    {
        //        foreach (var (culture, content) in from culture in enabledCultures
        //                                           let content = _contentLoader.Get<IContent>(currentContent.ContentLink, culture)
        //                                           select (culture, content))
        //        {
        //            if (content == null && !contentExistsInEnglish)
        //            {
        //                continue;
        //            }

        //            var url = GetUrlWithTrailingSlash(currentContent.ContentLink, culture.Name, virtualPathArguments);

        //            if (string.IsNullOrEmpty(url))
        //            {
        //                continue;
        //            }

        //            if (culture.Name.Equals(DefaultCulture, StringComparison.InvariantCultureIgnoreCase))
        //            {
        //                stringBuilder.AppendLine(string.Format(AlternateLanguageLinksFormat, url, XDefault));
        //                contentExistsInEnglish = true;
        //            }

        //            stringBuilder.AppendLine(string.Format(AlternateLanguageLinksFormat, url, culture.Name.ToLower()));
        //        }
        //    }

        //    return new HtmlString(stringBuilder.ToString().ToLower());
        //}

        //Meta tags
        //GetOgTags
        //GetTwitterTags
        //GetRobotsTag

        public string GetUrlWithTrailingSlash(ContentReference contentLink, string language, VirtualPathArguments virtualPathArguments)
        {
            var url = _urlResolver.GetUrl(contentLink, language, virtualPathArguments);

            if (!url.EndsWith('/'))
            {
                url = url + "/";
            }

            return url;
        }
    }
}
