using EPiServer.Core;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Infrastructure.Builders
{
    public interface IBlockViewModelBuilder
    {
        ICallToActionViewModel GetCallToActionViewModel(BlockData blockData);
        IPageBannerViewModel GetPageBannerViewModel(BlockData blockData);
        IEnumerable<ICategoryTagViewModel> GetBlogCategoryTagViewModel(IList<ContentReference> categories, string language = "");
        ISearchResultTileViewModel GetSearchResultTile(PageData pageData, string language = "en", string trackingId = "");
        IBlogArticleCardSubViewModel GetBlogArticleCardSubViewModel(PageData pageData, string language = "", string trackingId = "");
    }
}
