using EPiServer.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarterKit.HeadLess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Infrastructure.Builders
{
    public interface IDisplayViewModelBuilder
    {
        ImageModel GetImage(ContentReference contentReference, string width = "", bool absolutePath = false);
    }
}
