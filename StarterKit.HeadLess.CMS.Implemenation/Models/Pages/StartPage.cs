using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Pages
{
    [ContentType(DisplayName = "Start Page", GUID = "2DBD2887-EAAF-4477-8873-73A9E0BA93EE", Description = "Start page for the Healdess website", GroupName = SystemTabNames.Content)]//Name TBD, Icon TBD
   
    public class StartPage : PageData
    {

        [ScaffoldColumn(false)]
        public virtual string Title { get; set; }

        [CultureSpecific]
        [Display(Name = "Main body", GroupName = SystemTabNames.Content, Order = 100)]
        public virtual XhtmlString MainBody { get; set; }

        [CultureSpecific]
        [Display(Name = "Main content area", GroupName = SystemTabNames.Content, Order = 190)]
        public virtual ContentArea MainContentarea { get; set; }
    }
}
