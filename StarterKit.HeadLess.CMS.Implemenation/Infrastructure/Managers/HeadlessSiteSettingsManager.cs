using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer;
using System.Globalization;
using EPiServer.Web;
using EPiServer.ServiceLocation;
using EPiServer.Globalization;
using StarterKit.HeadLess.CMS.Infrastructure.Iterfaces;
using StarterKit.HeadLess.Core.Services;
using StarterKit.HeadLess.Core.Models.SiteSettings;
using StarterKit.HeadLess.Core.Extensions;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Managers
{
    [ServiceConfiguration(ServiceType = typeof(IHeadlessSiteSettingsManager), Lifecycle = ServiceInstanceScope.Singleton)]
    public class HeadlessSiteSettingsManager : IHeadlessSiteSettingsManager
    {
        private readonly IContentLoader _contentLoader;
        private readonly IRequestService _requestService;
        private readonly IDictionary<string, SettingsFolder> _rootSettingsFolders;
        public ContentReference GlobalSettingsRoot { get; private set; }

        public HeadlessSiteSettingsManager(IContentLoader contentLoader, IRequestService requestService, ContentRootService contentRootService)
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

        public T GetSiteSettings<T>(CultureInfo language = null) where T : class, IContentData
        {
            var key = "SettingsService:SiteDefinition";
            var currentSiteDefinition = GetValue<string>(key);

            if (language == null)
                language = ContentLanguage.PreferredCulture;

            if (currentSiteDefinition == null)
            {
                currentSiteDefinition = SiteDefinition.Current.Name ?? "Headless";

                SetValue(key, currentSiteDefinition);
            }

            if (!_rootSettingsFolders.TryGetValue(currentSiteDefinition, out var _rootSettingsFolder))
                return default;

            var type = typeof(T);
            key = string.Format("SettingsService:{0}", type.Name);

            if (language is not null)
                key = string.Format("SettingsService:{0}-{1}", type.Name, language.Name);

            var value = GetValue<T>(key);

            if (value is not null)
                return value;

            value = _contentLoader
                            .GetChildren<T>(_rootSettingsFolder.ContentLink, new LoaderOptions { LanguageLoaderOption.FallbackWithMaster(language) })
                            .FirstOrDefault();

            SetValue(key, value);

            return value ?? default;
        }

        private T GetValue<T>(string key) where T : class => _requestService?.Get<T>(key);

        private void SetValue<T>(string key, T value) where T : class => _requestService?.Set(key, value);
    }
}
