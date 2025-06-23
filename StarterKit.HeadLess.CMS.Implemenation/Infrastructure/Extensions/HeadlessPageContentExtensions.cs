
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Helper;
using StarterKit.HeadLess.CMS.Implemenation.Models.Categories;
using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using StarterKit.HeadLess.CMS.Infrastructure.Builders;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;

namespace Comptia.Headless.CMS.Implementation.Infrastructure.Extensions
{
    public static class HeadlessPageContentExtensions
    {
        private static readonly Lazy<IDisplayViewModelBuilder> DisplayViewModelBuilder =
            new Lazy<IDisplayViewModelBuilder>(() => ServiceLocator.Current.GetInstance<IDisplayViewModelBuilder>());

        private static readonly Lazy<LocalizationService> LocalizationService =
            new Lazy<LocalizationService>(() => ServiceLocator.Current.GetInstance<LocalizationService>());

        //public static IHeadlessImageModel BackgroundImage<T>(this T content) where T : HeadlessBasePageData
        //{
        //    try
        //    {
        //        return DisplayViewModelBuilder.Value.GetImage(content.BackgroundImage);
        //    }
        //    catch
        //    {
        //        return new HeadlessImageModel();
        //    }
        //}

        //public static IHeadlessImageModel Thumbnail<T>(this T content) where T : HeadlessBasePageData
        //{
        //    try
        //    {
        //        return DisplayViewModelBuilder.Value.GetImage(content.Thumbnail);
        //    }
        //    catch
        //    {
        //        return new HeadlessImageModel();
        //    }
        //}

        public static IEnumerable<string> SearchCategories<T>(this T content) where T : HeadlessBasePageData
        {
            try
            {
                return CategoryHelper.GetCategoryDisplayNames<HeadlessBaseCategoryData>(content.Categories);
            }
            catch
            {
                return new List<string>();
            }
        }

        public static string ReadTime<T>(this T content) where T : BlogArticlePage
        {
            try
            {
                return content.ReadTime > 0 ? LocalizationService.Value.GetString("Headless.BlogArticlePage.ReadTime").Replace("{0}", content.ReadTime.ToString()) : "0";
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
