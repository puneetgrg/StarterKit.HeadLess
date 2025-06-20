using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.Web.Routing;
using EPiServer.Web;
using EPiServer;
using StarterKit.HeadLess.CMS.Infrastructure.Builders;
using StarterKit.HeadLess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Builders
{
    public class DisplayViewModelBuilder : IDisplayViewModelBuilder
    {
        private readonly IContentRepository _contentRepository;
        private readonly UrlResolver _urlResolver;

        public DisplayViewModelBuilder(UrlResolver urlResolver, IContentRepository contentRepository)
        {
            _urlResolver = urlResolver;
            _contentRepository = contentRepository;
        }

        public ImageModel GetImage(ContentReference contentReference, string width = "", bool absolutePath = false)
        {
            if (ContentReference.IsNullOrEmpty(contentReference))
                return new ImageModel();

            _contentRepository.TryGet<ImageData>(contentReference, out var image);
            if (image == null)
                return new ImageModel();

            var imageAlt = string.IsNullOrWhiteSpace(image.Property["AltText"]?.Value?.ToString())
                ? string.Empty
                : image.Property["AltText"]?.Value?.ToString();

            if (string.IsNullOrEmpty(imageAlt))
            {
                imageAlt = string.IsNullOrWhiteSpace(image.Property["Description"]?.Value?.ToString())
                    ? AltFileNameFallback(image.Name)
                    : image.Property["Description"]?.Value?.ToString();
            }

            var virtualPathArguments = new VirtualPathArguments
            {
                ContextMode = ContextMode.Default,
                ForceAbsolute = true
            };

            var url = absolutePath ?
                    _urlResolver.GetUrl(image.ContentLink, ContentLanguage.PreferredCulture.Name, virtualPathArguments) :
                    _urlResolver.GetUrl(image.ContentLink);

            if (!string.IsNullOrWhiteSpace(width))
                url = $"{url}?width={width}";

            var fallback = AltFileNameFallback(image.Name);

            return new ImageModel
            {
                Src = new Url(url).Uri.ToString(),
                Alt = !string.IsNullOrEmpty(imageAlt) ? imageAlt : fallback
            };
        }

        private static string AltFileNameFallback(string fileName)
        {
            var name = Path.GetFileNameWithoutExtension(fileName);

            return name.Replace("_", " ").Replace("-", " ");
        }
    }
}
