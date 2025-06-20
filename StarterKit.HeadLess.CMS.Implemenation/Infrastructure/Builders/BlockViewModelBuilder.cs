using EPiServer;
using EPiServer.Core;
using EPiServer.SpecializedProperties;
using EPiServer.Web.Routing;
using StarterKit.HeadLess.CMS.Implemenation.Models.Blocks;
using StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels;
using StarterKit.HeadLess.CMS.Infrastructure.Builders;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Builders
{
    public class BlockViewModelBuilder : IBlockViewModelBuilder
    {
        public readonly IUrlResolver _urlResolver;
        public readonly IContentLoader _contentLoader;

        public readonly IDisplayViewModelBuilder _displayViewModelBuilder;
        public BlockViewModelBuilder(IUrlResolver urlResolver, IContentLoader contentLoader, IDisplayViewModelBuilder displayViewModelBuilder)
        {
            _urlResolver = urlResolver;
            _contentLoader = contentLoader;
            _displayViewModelBuilder = displayViewModelBuilder;
        }
        public ICallToActionViewModel GetCallToActionViewModel(BlockData blockData)
        {
            if (blockData is not CallToActionButtonBlock callToActionBlock)
                return null;

            var linkData = callToActionBlock.Link != null && callToActionBlock.Link.Any() ? callToActionBlock.Link[0] : new LinkItem();

            if (string.IsNullOrEmpty(linkData.Text) || string.IsNullOrEmpty(linkData.Href))
                return null;

            return new CallToActionViewModel(callToActionBlock)
            {
                CtaLabel = linkData.Text,
                CtaAriaLabel = string.IsNullOrEmpty(linkData.Title) ? linkData.Text : linkData.Title,
                CtaType = string.IsNullOrEmpty(callToActionBlock.Style) ? "PrimaryButton" : callToActionBlock.Style,
                CtaUrl = _urlResolver.GetUrl(linkData.Href),
                CtaTarget = string.IsNullOrEmpty(linkData.Target) ? "_self" : linkData.Target,
                Alignment = string.IsNullOrEmpty(callToActionBlock.Alignment) ? "Alignment.RightAlignment.ToLower()" : callToActionBlock.Alignment,
                ImageData = _displayViewModelBuilder.GetImage(callToActionBlock.Icon)
            };
        }

    }
}
