using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Cms.Conventions;

namespace StarterKit.HeadLess.CMS.Infrastructure.Conventions
{
    public interface IIndexingConvention
    {
        void Apply(IConventionManager conventionManager, IClientConventions clientConventions);
    }
}
