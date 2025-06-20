using EPiServer.ContentApi.Core.Serialization;
using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Serialization
{
    public class CustomContentConverterProvider : IContentConverterProvider
    {
        private readonly CustomContentConvertor _customContentConvertor;

        public CustomContentConverterProvider(CustomContentConvertor customContentConvertor) => _customContentConvertor = customContentConvertor;

        public int SortOrder => 50;

        public IContentConverter Resolve(IContent content) => _customContentConvertor;
    }
}
