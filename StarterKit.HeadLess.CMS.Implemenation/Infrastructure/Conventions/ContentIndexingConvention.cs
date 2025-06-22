using EPiServer.Find;
using EPiServer.Find.ClientConventions;
using StarterKit.HeadLess.CMS.Implemenation.Models.Blocks;
using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using StarterKit.HeadLess.CMS.Infrastructure.Conventions;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Conventions
{
    public class ContentIndexingConvention : IIndexingConvention
    {
        public void Apply(IConventionManager conventionManager, IClientConventions clientConventions)
        {
            clientConventions.ForInstancesOf<HeadlessBasePageData>().ApplyFieldConventions();

            //Exclude
            conventionManager.ExcludeType<StartPage>();
            //conventionManager.ExcludeType<HeadlessSearchPage>();
            //conventionManager.ExcludeType<BlogListingPage>();
            //conventionManager.ExcludeType<ProductListingPage>();
            //conventionManager.ExcludeType<SubProductListingPage>();
            //conventionManager.ExcludeType<JobListingPage>();
            //conventionManager.ExcludeType<ErrorPage>();
            //Add Error Pages here
            conventionManager.ExcludeType<CallToActionButtonBlock>();
            //conventionManager.ExcludeType<HeadlessMenuItem>();
            //conventionManager.ExcludeType<QuickLinksGroupBlock>();
            //conventionManager.ExcludeType<HeadlessLayoutSettings>();
        }
    }
}
