using EPiServer.Cms.UI.Admin.Internal;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using StarterKit.HeadLess.Core.Extensions;
using StarterKit.HeadLess.Core.Models.SiteSettings;
using StarterKit.HeadLess.Core.Services;
using System.Globalization;

namespace StarterKit.HeadLess.Web.Infrastructure.Cms.Settings
{
    public interface ISettingsService
    {
        ContentReference GlobalSettingsRoot { get; }

        T GetSiteSettings<T>(CultureInfo languge = null) where T : class, IContentData;

        //AdyenSettings GetAdyenSettings();

      
    }

    [ServiceConfiguration(ServiceType = typeof(ISettingsService), Lifecycle = ServiceInstanceScope.Singleton)]
    public class SettingsService : ISettingsService
    {
        private readonly IContentLoader _contentLoader;
        private readonly IRequestService _requestService;

        private readonly IDictionary<string, SettingsFolder> _rootSettingsFolders;

        public SettingsService(IContentLoader contentLoader, IRequestService requestService, ContentRootService contentRootService)
        {
            _contentLoader = contentLoader;
            _requestService = requestService;

            GlobalSettingsRoot = contentRootService.Get(SettingsFolder.SettingsRootName);

            if (GlobalSettingsRoot.IsNullOrEmpty())
            {
                contentRootService.Register<SettingsFolder>(SettingsFolder.SettingsRootName, SettingsFolder.SettingsRootGuid, ContentReference.RootPage);

                GlobalSettingsRoot = contentRootService.Get(SettingsFolder.SettingsRootName);
            }

            _rootSettingsFolders = _contentLoader
                                        .GetChildren<SettingsFolder>(GlobalSettingsRoot)
                                        .ToDictionary(entity => entity.Name, entity => entity);
        }

        public ContentReference GlobalSettingsRoot { get; private set; }

        public T GetSiteSettings<T>(CultureInfo languge = null) where T : class, IContentData
        {
            var key = "SettingsService:SiteDefinition";
            var currentSiteDefinition = GetValue<string>(key);

            if (currentSiteDefinition == null)
            {
                currentSiteDefinition = SiteDefinition.Current.Name ?? "store";

                SetValue(key, currentSiteDefinition);
            }

            if (currentSiteDefinition is null)
                return default;

            if (!_rootSettingsFolders.TryGetValue(currentSiteDefinition ?? string.Empty, out var _rootSettingsFolder))
                return default;

            var type = typeof(T);
            key = string.Format("SettingsService:{0}", type.Name);

            if (languge is not null)
                key = string.Format("SettingsService:{0}-{1}", type.Name, languge.Name);

            var value = GetValue<T>(key);

            if (value is not null)
                return value;

            value = _contentLoader
                            .GetChildren<T>(_rootSettingsFolder.ContentLink, new LoaderOptions { LanguageLoaderOption.FallbackWithMaster(languge) })
            .FirstOrDefault();
            SetValue(key, value);

            return value ?? default;
        }

        //public AdyenSettings GetAdyenSettings() => GetSiteSettings<AdyenSettings>();

       

        private T GetValue<T>(string key) where T : class => _requestService?.Get<T>(key);

        private void SetValue<T>(string key, T value) where T : class => _requestService?.Set(key, value);
    }
}
