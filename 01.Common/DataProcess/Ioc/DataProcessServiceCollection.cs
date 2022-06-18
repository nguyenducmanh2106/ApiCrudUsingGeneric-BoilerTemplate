using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Core.Utils;
using DataProcess.Infrastructure.Impl;
using DataProcess.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace DataProcess.Ioc
{
    public static class DataProcessServiceCollection
    {
        public static void AddDataProcessServices(this IServiceCollection services)
        {
            var connectionsDic = AppSettings.Instance.Get<Dictionary<string, string>>("Databases:MSSQL:ConnectionStrings");
            var savisConnection = connectionsDic["SavisCoreFWEntities"];
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(savisConnection));
            services.AddScoped<IDapperUnitOfWork, DapperUnitOfWork>();
        }
    }
}
