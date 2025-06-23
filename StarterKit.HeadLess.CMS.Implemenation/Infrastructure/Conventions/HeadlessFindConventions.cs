using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using EPiServer.Find.ClientConventions;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Conventions
{
    public static class HeadlessFindConventions
    {
        public static TypeConventionBuilder<HeadlessBasePageData> ApplyFieldConventions(this TypeConventionBuilder<HeadlessBasePageData> builder)
        {
            builder.ExcludeField(i => i.Thumbnail);
            builder.ExcludeField(i => i.BackgroundImage);

            //builder.IncludeField(i => i.SearchCategories());
            //builder.IncludeField(i => i.BackgroundImage());
            //builder.IncludeField(i => i.Thumbnail());
            builder.IncludeField(i => i.StartPublish);

            return builder;
        }
        //public static TypeConventionBuilder<BlogArticlePage> ApplyFieldConventions(this TypeConventionBuilder<BlogArticlePage> builder)
        //{
        //    builder.ExcludeField(i => i.ReadTime);

        //    builder.IncludeField(i => i.ReadTime());
        //    builder.IncludeField(i => i.StartPublish);

        //    return builder;
        //}

    }
}
