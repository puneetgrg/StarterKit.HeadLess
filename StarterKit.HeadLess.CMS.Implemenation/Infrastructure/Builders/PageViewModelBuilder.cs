using EPiServer.Core;
using EPiServer.Framework.Localization;
using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels.Pages;
using StarterKit.HeadLess.CMS.Infrastructure.Builders;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using StarterKit.HeadLess.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Builders
{
    public class PageViewModelBuilder : IPageViewModelBuilder
    {
        private readonly LocalizationService _localizationService;

        public PageViewModelBuilder(LocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public IContentPageViewModel GetContentPageViewModel(PageData pageData)
        {
            if (pageData is not ContentPage contentPage)
                return null;

            ContentPageViewModel viewModel = new ContentPageViewModel
            {
                Title = string.IsNullOrWhiteSpace(contentPage.Title) ? contentPage.Name : contentPage.Title,
                Subheadline = XhtmlHelper.ResolveUrls(contentPage.Subheadline),
                JumpLinks = contentPage.JumpLinks == null ? new List<JumpLinkItemViewModel>() : contentPage.JumpLinks?.Select(x => new JumpLinkItemViewModel { Title = x.Title, AnchorId = x.AnchorId }),
                BackToTopLabel = _localizationService.GetString("Headless.LegalTemplatePage.BackToTop"),
                ShowBackTopButton = contentPage.ShowBackTopButton,
                MainContentArea = contentPage.MainContentArea,
                RightContentArea = contentPage.RightContentArea
            };

            return viewModel;
        }


    }
}
