
using Newtonsoft.Json;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces.Search;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Search
{
    public class HeadlessSearchResultModel : IHeadlessSearchResultModel
    {
        [JsonProperty("totalMatching")]
        public int TotalMatching { get; set; }

        [JsonProperty("results")]
        public IEnumerable<object> Results { get; set; }
    }
}
