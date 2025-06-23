using StarterKit.HeadLess.CMS.Infrastructure.Builders;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Microsoft.Extensions.DependencyInjection;
using StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Serialization;
using StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Builders;
using StarterKit.HeadLess.CMS.Infrastructure.Managers;
using StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Managers;
using StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Conventions;
using StarterKit.HeadLess.CMS.Infrastructure.Conventions;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;

namespace StarterKit.HeadLess.CMS.Implemenation.Bootstrapper
{
    [ModuleDependency(typeof(InitializationModule))]
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    public class CmsImplementationBootstrapper : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            var _services = context.Services;


            //API Filters
            _services.AddSingleton<IContentFilter, PreSerializationContentFilter>();
            _services.AddSingleton<IContentApiModelFilter, PostSerializationApiModelFilter>();

            //Convertor
            _services.AddSingleton<IContentConverterProvider, CustomContentConverterProvider>();

            //Builders
            _services.AddSingleton<IDisplayViewModelBuilder, DisplayViewModelBuilder>();
            _services.AddSingleton<IBlockViewModelBuilder, BlockViewModelBuilder>();
            _services.AddSingleton<IBreadcrumbViewModelBuilder, BreadcrumbViewModelBuilder>();
            _services.AddSingleton<IPageViewModelBuilder, PageViewModelBuilder>();
            //Managers
            _services.AddSingleton<ISEOManager, SEOManager>();

            //Indexing Convention
            _services.AddSingleton<IConventionManager, ConventionManager>();
            _services.AddSingleton<IIndexingConvention, ContentIndexingConvention>();
        }

        public void Initialize(InitializationEngine context)
        { //not implmeneted
        }

        public void Uninitialize(InitializationEngine context)
        { //not implmeneted
        }
    }
}
