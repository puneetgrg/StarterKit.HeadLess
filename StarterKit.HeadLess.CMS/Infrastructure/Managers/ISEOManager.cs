using EPiServer.Core;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Infrastructure.Managers
{
    public interface ISEOManager
    {
        HtmlString LowerCaseCanonicalLink(IContent currentContent);
    }
}
