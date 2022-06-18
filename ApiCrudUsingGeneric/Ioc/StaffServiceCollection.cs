using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Caching.Ioc;
using Core.Utils;
using DataProcess.Ioc;

namespace SV.HRM.Service.ThongTinNhanSu.Ioc
{
    public static class StaffServiceCollection
    {
        public static void RegisterIoCs(this IServiceCollection services, IConfiguration configuration)
        {
            AppSettings.Instance.SetConfiguration(configuration);
            services.AddDataProcessServices();
            services.AddCachingProcessServices();
            services.AddStaffAutoMapper();
            //services.AddScoped<IStaffHandler, StaffHandler>();
        }
    }
}
