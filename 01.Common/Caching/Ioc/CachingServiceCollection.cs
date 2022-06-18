using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Caching.Impl;
using Caching.Interface;
using Core.Utils;

namespace Caching.Ioc
{
    public static class CachingServiceCollection
    {
        public static void AddCachingProcessServices(this IServiceCollection services)
        {
            switch (StaticVariable.CacheType)
            {
                case 1:
                    RedisCachedServiceCollection.RegisterIoCs(ref services);
                    break;

                default:
                    services.AddSingleton<ICached>(cd => { return new NoCached(); });
                    break;
            }
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICacheAppService, CacheAppService>();
        }
    }
}
