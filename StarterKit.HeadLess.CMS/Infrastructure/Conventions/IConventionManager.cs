using EPiServer.Core;

namespace StarterKit.HeadLess.CMS.Infrastructure.Conventions
{
    public interface IConventionManager
    {
        void ExcludeType<T>() where T : IContentData;

        void IncludeType<T>() where T : IContentData;
    }
}
