using EPiServer.ContentApi.Core.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Infrastructure.Iterfaces
{
    public interface IBaseBlockViewModel
    {
        List<string> ContentType { get; set; }

        ContentModelReference ContentLink { get; set; }
    }
}
