
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces.Search;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Search
{
    public class HeadlessSearchRequest : IHeadlessSearchRequest
    {
        public int Skip { get; set; }       //Pagee number

        public int Top { get; set; }   //Page Size

        public string Sort { get; set; }    //Sorting

        public string Query { get; set; }

        public string Categories { get; set; }  //Comma separated list

        public string Language { get; set; }

        public HeadlessSearchRequest() 
        {
            Skip = 0;
            Top = 12;
            Language = "en";
        }
    }
}
