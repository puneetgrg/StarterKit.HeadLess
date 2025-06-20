using EPiServer.Core;
using EPiServer.PlugIn;
using Newtonsoft.Json;
using StarterKit.HeadLess.CMS.Implemenation.Models.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.ProeprtyLists
{
    [PropertyDefinitionTypePlugIn]
    public class JumpLinkPropertyList : PropertyList<JumpLink>
    {
        protected override JumpLink ParseItem(string value) => JsonConvert.DeserializeObject<JumpLink>(value);
    }
}
