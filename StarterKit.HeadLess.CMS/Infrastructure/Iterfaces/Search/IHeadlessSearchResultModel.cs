namespace StarterKit.HeadLess.CMS.Infrastructure.Iterfaces.Search
{
    public interface IHeadlessSearchResultModel
    {
        int TotalMatching { get; set; }

        IEnumerable<object> Results { get; set; }
    }
}
