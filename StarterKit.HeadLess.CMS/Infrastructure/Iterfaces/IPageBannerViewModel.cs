using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Infrastructure.Iterfaces
{
    public interface IPageBannerViewModel
    {
        string Headline { get; set; }
        XhtmlString Description { get; set; }
        List<ICallToActionViewModel> CtaButtons { get; set; }
    }
}
