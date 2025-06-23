
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Api;
using EPiServer.Find.Cms;
using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using System.Text;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Extensions.Find
{
    public static class FindExtensions
    {
        public static ITypeSearch<T> FliterBySearchTerm<T>(this ITypeSearch<T> query, string q) where T : PageData
        {
            if (string.IsNullOrWhiteSpace(q))
                return query;

            //Espace Find Reserved characters mentioned in CTMVP-2617
            string updatedQuery = EsacpeSepcialCharacters(q);

            query = query.For(updatedQuery)
                .InField(x => (x as HeadlessBasePageData).SearchTitle)
                .InAllField()
                .UsingSynonyms()
                .ApplyBestBets()
                .BoostMatching(x => (x as HeadlessBasePageData).SearchTitle.Match(updatedQuery), 2);

            return query;
        }

        public static ITypeSearch<T> FliterBySearchCategories<T>(this ITypeSearch<T> query, string categories) where T : PageData
        {
            if (string.IsNullOrWhiteSpace(categories) || categories.Equals("all", StringComparison.InvariantCultureIgnoreCase))
                return query;

            List<string> items = Array.ConvertAll(categories.Split(','), p => p.Trim()).ToList();

            if (items == null || !items.Any())
                return query;

            var requiredFilter = new FilterBuilder<T>(query.Client);

            requiredFilter = requiredFilter.And(x => (x as HeadlessBasePageData).SearchCategories().In(items, true));

            return query.Filter(requiredFilter);
        }

        public static ITypeSearch<T> SortBy<T>(this ITypeSearch<T> query, string sort) where T : PageData
        {
            if (string.IsNullOrEmpty(sort))
                return query;

            sort = sort.ToLower();

            switch (sort)
            {
                case "newest":
                    {
                        query = query.OrderBy(x => x.StartPublish, null, SortOrder.Descending, false);
                        break;
                    }
                case "oldest":
                    {
                        query = query.OrderBy(x => x.StartPublish, null, SortOrder.Ascending, false);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return query;
        }

        public static string EsacpeSepcialCharacters(string query)
        {
            var dict = new Dictionary<string, string>
            {
                { "*", string.Empty },
                { "+", "\\+" },
                { "-", "\\-" },
                { "&", "\\&" },
                { "|", "\\|" },
                { "(", "\\(" },
                { ")", "\\)" },
                { "{", "\\{" },
                { "}", "\\}" },
                { "[", "\\[" },
                { "]", "\\]" }
            };

            var stringBuilder = new StringBuilder();
            string temp;

            foreach (char c in query)
            {
                if (dict.TryGetValue(c.ToString(), out temp))
                    stringBuilder.Append(temp);
                else
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }
    }
}
