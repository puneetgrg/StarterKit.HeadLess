using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using StarterKit.HeadLess.CMS.Implemenation.Models.Blocks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Pages
{
    [ContentType(DisplayName = "Start Page", GUID = "2DBD2887-EAAF-4477-8873-73A9E0BA93EE", Description = "Start page for the Healdess website", GroupName = SystemTabNames.Content)]//Name TBD, Icon TBD
   
    public class StartPage : HeadlessBasePageData
    {
        [UIHint(UIHint.Image)]
        [Display(Name = "Background Image",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 40)]
        public override ContentReference BackgroundImage { get; set; }

        [Ignore]
        public override ContentArea TopContentArea { get; set; }
       
    }
}
