using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels
{
    public class BreadcrumbViewModel : IBreadcrumbViewModel
    {
        public IList<IBreadcrumbItemViewModel> Breadcrumbs { get; set; }

        public BreadcrumbViewModel()
        {
            Breadcrumbs = new List<IBreadcrumbItemViewModel>();
        }
    }
}
