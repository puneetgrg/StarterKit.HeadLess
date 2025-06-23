using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.Localization;
using System.Configuration;
using StarterKit.HeadLess.Core.Models.Factories;
using System.Runtime.InteropServices;

namespace StarterKit.HeadLess.Core.Models.SiteSettings
{
    [ContentType(GUID = "2791B381-AAA6-4438-A02A-4BFFBECF6DEA")]
    [AvailableContentTypes(Include = new[] { typeof(SettingsBase), typeof(SettingsFolder) })]
    public class SettingsFolder : ContentFolder
    {
        public const string SettingsRootName = "SettingsRoot";
        public readonly static Guid SettingsRootGuid = new Guid("A95DFA00-524E-4437-9AA1-0C0410318358");
     
        private static readonly Lazy<ContentRootService> _rootService = LazyFactory.CreateInstance<ContentRootService>();
        private static readonly Lazy<LocalizationService> _localizationService = LazyFactory.CreateInstance<LocalizationService>();

        public static ContentReference SettingsRoot => GetSettingsRoot();

        public override string Name
        {
            get
            
            {
                if (ContentLink.CompareToIgnoreWorkID(SettingsRoot))
                    return _localizationService.Value.GetString("/contentrepositories/globalsettings/Name");

                return base.Name;
            }
            set => base.Name = value;
        }

        private static ContentReference GetSettingsRoot() => _rootService.Value.Get(SettingsRootName);
    }
}
