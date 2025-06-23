using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Geta.Optimizely.Categories;
using StarterKit.HeadLess.CMS.Implemenation.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Helper
{
    public static class CategoryHelper
    {
        private static readonly Lazy<ICategoryContentLoader> _categoryContentLoader =
            new(() => ServiceLocator.Current.GetInstance<ICategoryContentLoader>());


        private static readonly Lazy<IContentInCategoryLocator> _contentInCategoryLocator =
            new(() => ServiceLocator.Current.GetInstance<IContentInCategoryLocator>());

        private static readonly Lazy<IUrlResolver> _urlResolver =
            new(() => ServiceLocator.Current.GetInstance<IUrlResolver>());

        //Returns the Display Name of Categories
        public static List<string> GetCategoryDisplayNames<T>(IList<ContentReference> categoryList) where T : CategoryData, IDisplayable
        {
            if (categoryList == null)
                return new List<string>();

            List<string> catnames = new List<string>();

            foreach (var categoryId in categoryList)
            {
                _categoryContentLoader.Value.TryGet(categoryId, out T cpage);

                if (cpage != null)
                {
                    catnames.Add(cpage.DisplayName ?? cpage.Name ?? "");
                }
            }

            return catnames;
        }

        //Returns the Name of Categories
        public static List<string> GetCategoryNames<T>(IList<ContentReference> categoryList) where T : CategoryData, IDisplayable
        {
            if (categoryList == null)
                return new List<string>();

            List<string> catnames = new List<string>();

            foreach (var categoryId in categoryList)
            {
                _categoryContentLoader.Value.TryGet(categoryId, out T cpage);

                if (cpage != null)
                {
                    catnames.Add(cpage.Name);
                }
            }

            return catnames;
        }

        public static List<string> GetSiteCategories<T>() where T : CategoryData, IDisplayable
        {
            var categoryList = _categoryContentLoader.Value.GetSiteCategories<T>();
            if (categoryList == null)
                return new List<string>();

            return categoryList.Select(x => x.DisplayName).ToList();

        }

        // it will get list of sub category category
        public static List<string> GetSiteCategoriesChildren<T>() where T : CategoryData, IDisplayable
        {
            var categoryList = _categoryContentLoader.Value.GetSiteCategories<T>();
            if (!categoryList.Any())
                return new List<string>();

            var categoryItemList = new List<string>();

            foreach (var category in categoryList)
            {
                var categoryChildren = _categoryContentLoader.Value.GetChildren<T>(category.ContentLink);

                if (!categoryChildren.Any())
                    return new List<string>();

                categoryItemList.AddRange(categoryChildren.Select(x => x.DisplayName));
            }

            return categoryItemList;
        }
    }
}
