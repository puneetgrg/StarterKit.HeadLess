using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Infrastructure.Iterfaces
{
    public interface IHeadlessSiteSettingsManager
    {
        ContentReference GlobalSettingsRoot { get; }

        T GetSiteSettings<T>(CultureInfo language = null) where T : class, IContentData;
    }
}
