using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels
{
    public class BreadcrumbItemViewModel : IBreadcrumbItemViewModel
    {
        public BreadcrumbItemViewModel(HeadlessBasePageData pageData)//Add another constructor for webproduct
        {
            Name = pageData.Title ?? pageData.Name;
            Url = string.Empty;
        }

        public string Name { get; set; }
        public string Url { get; set; }
    }
}
