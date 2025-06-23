using EPiServer.ServiceLocation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.Core.Services
{
    public interface IRequestService
    {
        T Get<T>(string key) where T : class;

        void Set<T>(string key, T value) where T : class;

        void Remove(string key);
    }

    [ServiceConfiguration(ServiceType = typeof(IRequestService), Lifecycle = ServiceInstanceScope.Singleton)]
    public class RequestService : IRequestService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestService(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public T Get<T>(string key) where T : class
        {
            if (Items == null)
                return null;

            return Items[key] as T;
        }
        public void Remove(string key)
        {
            if (Items == null)
                return;

            Items.Remove(key);
        }

        public void Set<T>(string key, T value) where T : class
        {
            if (Items == null)
                return;

            if (value == null)
                Remove(key);
            else
                Items[key] = value;
        }

        private IDictionary<object, object?> Items => _httpContextAccessor?.HttpContext?.Items;
    }
}
