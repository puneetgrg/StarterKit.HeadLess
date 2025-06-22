using StarterKit.HeadLess.CMS.Infrastructure.Conventions;
using EPiServer.Core;
using EPiServer.Find.Cms;
using EPiServer.Find.Cms.Conventions;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Conventions
{
    class ConventionManager : IConventionManager
    {
        private readonly IContentIndexer _contentIndexer;

        public ConventionManager(IContentIndexer indexer)
        {
            _contentIndexer = indexer;
        }

        public void ExcludeType<T>() where T : IContentData
        {
            _contentIndexer.Conventions.ForInstancesOf<T>().ShouldIndex(c => false);
        }

        public void IncludeType<T>() where T : IContentData
        {
            _contentIndexer.Conventions.ForInstancesOf<T>().ShouldIndex(c => true);
        }
    }
}
