using EPiServer.Find;
using EPiServer.Find.Framework;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using StarterKit.HeadLess.CMS.Infrastructure.Conventions;


namespace StarterKit.HeadLess.CMS.Implemenation.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(InitializationModule), typeof(ServiceContainerInitialization))]
    public class HeadlessSearchServiceModule : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            //No code
        }

        public void Initialize(InitializationEngine context)
        {
            IConventionManager conventionManager = context.Locate.Advanced.GetInstance<IConventionManager>();

            IEnumerable<IIndexingConvention> conventions =
                context.Locate.Advanced.GetAllInstances<IIndexingConvention>();

            IClientConventions clientConventions = SearchClient.Instance.Conventions;

            foreach (IIndexingConvention convention in conventions)
                convention.Apply(conventionManager, clientConventions);
        }

        public void Uninitialize(InitializationEngine context)
        {
            //No code
        }
    }
}
