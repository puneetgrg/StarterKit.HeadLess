namespace StarterKit.HeadLess.CMS.Infrastructure.Iterfaces.Search
{
    public interface IHeadlessSearchRequest
    {
        int Skip { get; set; }       //Pagee number

        int Top { get; set; }   //Page Size

        string Sort { get; set; }    //Sorting

        string Query { get; set; }

        public string Categories { get; set; }  //Comma separated list

        string Language { get; set; }
    }
}
