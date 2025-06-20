using EPiServer.Core;
using StarterKit.HeadLess.CMS.Implemenation.Models.Blocks;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Viewmodels
{
    public class PageBannerViewModel : BaseBlockViewModel, IPageBannerViewModel
    {
        public PageBannerViewModel(PageBannerBlock blockData) : base(blockData)
        {
            ContentType = new List<string> { "Block", typeof(PageBannerBlock).Name };
        }
        public string Eyebrow { get; set; }
        public string Headline { get; set; }
        public XhtmlString Description { get; set; }
        public List<ICallToActionViewModel> CtaButtons { get; set; }
    }
}
