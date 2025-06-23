using AddOn.Episerver.Settings.Core;
using EPiServer.Core;
using EPiServer.Framework.Localization;
using StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Helper;
using StarterKit.HeadLess.CMS.Implemenation.Models.Categories;
using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using StarterKit.HeadLess.CMS.Implemenation.Models.SiteSettings;
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
        private readonly IBlockViewModelBuilder _blockViewModelBuilder;
        private readonly IDisplayViewModelBuilder _displayViewModelBuilder;
        public readonly ISettingsService _settingsService;

        public PageViewModelBuilder(LocalizationService localizationService, IBlockViewModelBuilder blockViewModelBuilder, IDisplayViewModelBuilder displayViewModelBuilder, ISettingsService settingsService)
        {
            _localizationService = localizationService;
            _blockViewModelBuilder = blockViewModelBuilder;
            _displayViewModelBuilder = displayViewModelBuilder;
            _settingsService = settingsService;
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

        public IBlogArticlePageViewModel GetBlogArticlePageViewModel(PageData pageData)
        {
            if (pageData is not BlogArticlePage blogArticle)
                return null;

            var viewModel = new BlogArticlePageViewModel
            {
                Categories = _blockViewModelBuilder.GetBlogCategoryTagViewModel(blogArticle.Categories),
                SearchCategories = CategoryHelper.GetCategoryNames<HeadlessBaseCategoryData>(blogArticle.Categories),
                ShortDescription = blogArticle.ShortDescription,
                Thumbnail = _displayViewModelBuilder.GetImage(blogArticle.Thumbnail),
                ReadTime = blogArticle.ReadTime == 0
                    ? blogArticle.ReadTime.ToString()
                    : _localizationService.GetString("Headless.BlogArticlePage.ReadTime").Replace("{0}", blogArticle.ReadTime.ToString()),
                RichText = XhtmlHelper.ResolveUrls(blogArticle.RichText),
                Author = blogArticle.Author,
                RightContentArea = blogArticle.RightContentArea
            };

            var blogSettings = _settingsService.GetSetting<BlogSettings>();

            var combinedContentArea = new ContentArea();

            // Add items from blogArticle.BottomContentArea if it exists and has items
            if (blogArticle.BottomContentArea != null)
            {
                if (blogArticle.BottomContentArea.Items != null && blogArticle.BottomContentArea.Items.Count > 0)
                {
                    foreach (var item in blogArticle.BottomContentArea.Items)
                    {
                        combinedContentArea.Items.Add(item);
                    }
                }
            }

            // Add items from blogSettings.BottomContentArea if IncludeGlobalBlogsInBottomContent is true
            if (blogArticle.IncludeGlobalBlogsInBottomContent)
            {
                if (blogSettings != null && blogSettings.BottomContentArea != null)
                {
                    if (blogSettings.BottomContentArea.Items != null && blogSettings.BottomContentArea.Items.Count > 0)
                    {
                        foreach (var item in blogSettings.BottomContentArea.Items)
                        {
                            combinedContentArea.Items.Add(item);
                        }
                    }
                }
            }


            viewModel.BottomContentArea = combinedContentArea;

            return viewModel;
        }

    }
}
