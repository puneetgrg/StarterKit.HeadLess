using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.Core.Models.SiteSettings
{
    public abstract class SettingsBase : StandardContentBase
    {
        private const StringSplitOptions _options = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

        protected static HashSet<string> GetValues(string values)
        {
            if (string.IsNullOrEmpty(values))
                return new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

            return values
                    .Split(new[] { ',' }, _options)
                    .ToHashSet(StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
