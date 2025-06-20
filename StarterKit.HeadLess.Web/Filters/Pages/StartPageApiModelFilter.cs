using EPiServer.ContentApi.Core.Serialization.Internal;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.ContentApi.Core.Serialization;
using StarterKit.HeadLess.CMS.Implemenation.Models.Blocks;
using StarterKit.HeadLess.CMS.Implemenation.Models.Pages;
using StarterKit.HeadLess.CMS.Infrastructure.Builders;
using EPiServer.ServiceLocation;

namespace StarterKit.HeadLess.Web.Filters.Pages
{
    [ServiceConfiguration(ServiceType = typeof(IContentApiModelFilter), Lifecycle = ServiceInstanceScope.Singleton)]
    internal class StartPageApiModelFilter : ContentApiModelFilter<ContentApiModel>
    {
        private readonly IContentLoader _contentLoader;
        private readonly IBlockViewModelBuilder _blockViewModelBuilder;

        public StartPageApiModelFilter(IContentLoader contentLoader, IBlockViewModelBuilder blockViewModelBuilder)
        {
            _contentLoader = contentLoader;
            _blockViewModelBuilder = blockViewModelBuilder;
        }

        public override void Filter(ContentApiModel contentApiModel, ConverterContext converterContext)
        {
            try
            {
                if (!_contentLoader.TryGet(converterContext.ContentReference, out StartPage startPage) || startPage is not StartPage)
                {
                    return;
                }

                AddContentAreaItemsToContentApiModel(nameof(startPage.MainContentarea), startPage.MainContentarea, contentApiModel);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void AddContentAreaItemsToContentApiModel(string key, ContentArea contentArea, ContentApiModel contentApiModel)
        {
            var Filtereditems = contentArea.FilteredItems;
            var items = new Dictionary<string, object>();
            var index = 0; //Will be appended to the item name in the contentApiModel to prevent duplicate keys in case we have multiples of the same block type 

            foreach (var contentAreaItem in Filtereditems)
            {
                _contentLoader.TryGet<BlockData>(contentAreaItem.ContentLink, out var content);

                if (content == null)
                    continue;

                switch (content)
                {
                    case CallToActionButtonBlock callToActionButtonBlock:
                        {
                            items.Add(nameof(CallToActionButtonBlock) + "_" + index, _blockViewModelBuilder.GetCallToActionViewModel(callToActionButtonBlock));
                            index++;
                            break;
                        }
                    default:
                        {
                            index++;
                            break;
                        }
                }
            }

            contentApiModel.Properties.Add(key, items);
        }
    }
}
