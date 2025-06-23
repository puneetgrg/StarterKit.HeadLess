using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels.Properties
{
    public class CategoryTagViewModel : ICategoryTagViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
