using EPiServer.Core;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Infrastructure.Builders
{
    public interface IBreadcrumbViewModelBuilder
    {
        IBreadcrumbViewModel GenerateBreadcrumbs(PageData pageData);
    }
}
