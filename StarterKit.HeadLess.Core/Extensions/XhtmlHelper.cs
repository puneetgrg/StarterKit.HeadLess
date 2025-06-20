using EPiServer.Core;
using EPiServer.Core.Html.StringParsing;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;

namespace StarterKit.HeadLess.Core.Extensions
{
    public static class XhtmlHelper
    {
        private static readonly Lazy<IUrlResolver> _urlResolver =
            new Lazy<IUrlResolver>(() => ServiceLocator.Current.GetInstance<IUrlResolver>());

        public static XhtmlString ResolveUrls(XhtmlString xhtmlString)
        {
            if (xhtmlString == null)
                return null;

            var newXhtml = new XhtmlString();

            for (var i = 0; i < xhtmlString.Fragments.GetFilteredFragments().Count; i++)
            {
                var fragments = xhtmlString.Fragments.GetFilteredFragments()[i];

                switch (fragments)
                {
                    case UrlFragment urlFragment:
                        {
                            var content = _urlResolver.Value.Route(new EPiServer.UrlBuilder(urlFragment.InternalFormat));

                            if (content != null && !content.ContentLink.IsExternalProvider)
                            {
                                var resolvedUrl = _urlResolver.Value.GetUrl(urlFragment.InternalFormat);
                                newXhtml.Fragments.Add(new UrlFragment(resolvedUrl));
                            }

                            break;
                        }
                    default:
                        {
                            newXhtml.Fragments.Add(fragments);
                            break;
                        }
                }
            }

            return newXhtml;
        }
    }
}
