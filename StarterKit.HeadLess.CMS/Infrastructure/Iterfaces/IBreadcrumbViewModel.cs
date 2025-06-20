using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Infrastructure.Iterfaces
{
    public interface IBreadcrumbViewModel
    {
        IList<IBreadcrumbItemViewModel> Breadcrumbs { get; set; }
    }
    public interface IBreadcrumbItemViewModel
    {
        string Name { get; set; }
        string Url { get; set; }
    }
}
