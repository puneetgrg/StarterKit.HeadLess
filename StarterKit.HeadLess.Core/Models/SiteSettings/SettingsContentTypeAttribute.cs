using EPiServer.DataAnnotations;

namespace StarterKit.HeadLess.Core.Models.SiteSettings
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public sealed class SettingsContentTypeAttribute : ContentTypeAttribute
    {
        public string SettingsName { get; set; }
    }
}
