using EPiServer.Core;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;

namespace StarterKit.HeadLess.CMS.Infrastructure.Builders
{
    public interface IPageViewModelBuilder
    {
        IContentPageViewModel GetContentPageViewModel(PageData pageData);
    }
}
