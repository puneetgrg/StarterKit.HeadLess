using Microsoft.AspNetCore.Mvc.ModelBinding;
using StarterKit.HeadLess.CMS.Implemenation.Models.Search;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.ModelBinders
{
    public class HeadlessSearchRequestBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var skipValue = bindingContext.ValueProvider.GetValue(nameof(HeadlessSearchRequest.Skip)).FirstValue;
            var topValue = bindingContext.ValueProvider.GetValue(nameof(HeadlessSearchRequest.Top)).FirstValue;
            var sortValue = bindingContext.ValueProvider.GetValue(nameof(HeadlessSearchRequest.Sort)).FirstValue;
            var queryValue = bindingContext.ValueProvider.GetValue(nameof(HeadlessSearchRequest.Query)).FirstValue;
            var categoriesValue = bindingContext.ValueProvider.GetValue(nameof(HeadlessSearchRequest.Categories)).FirstValue;
            var languageValue = bindingContext.ValueProvider.GetValue(nameof(HeadlessSearchRequest.Language)).FirstValue;

            var query = queryValue != null ? queryValue.Replace("%20", " ") : null;

            var searchRequest = new HeadlessSearchRequest
            {
                Skip = int.TryParse(skipValue, out var skip) ? skip : 0,
                Top = int.TryParse(topValue, out var top) ? top : 12,
                Sort = string.IsNullOrWhiteSpace(sortValue) ? null : sortValue,
                Query = query, // Allow literal spaces to remain
                Categories = string.IsNullOrWhiteSpace(categoriesValue) ? null : categoriesValue,
                Language = string.IsNullOrWhiteSpace(languageValue) ? "en" : languageValue
            };

            bindingContext.Result = ModelBindingResult.Success(searchRequest);
            return Task.CompletedTask;
        }
    }
}
