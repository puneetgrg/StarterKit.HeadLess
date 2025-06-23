using EPiServer.ServiceLocation;

namespace StarterKit.HeadLess.Core.Models.Factories
{
    public static class LazyFactory
    {
        public static Lazy<T> CreateInstance<T>()
        {
            return new Lazy<T>(() =>
            {
                return ServiceLocator.Current.GetInstance<T>();
            });
        }
    }
}
