using StarterKit.HeadLess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Infrastructure.Iterfaces
{
    public interface ICallToActionViewModel
    {
        public string CtaLabel { get; set; }
        public string CtaAriaLabel { get; set; }
        public string CtaType { get; set; }
        public string CtaUrl { get; set; }
        public string CtaTarget { get; set; }
        ImageModel ImageData { get; set; }
    }
    public interface IHeadlessImageModel
    {
        public string Src { get; set; }

        public string Alt { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public bool IsExternal { get; set; }

        public string Extension { get; set; }

        public string AssetId { get; set; }
    }
}
