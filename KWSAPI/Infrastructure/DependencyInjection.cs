
using KWS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace KWS.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddKWSDBConn(this IServiceCollection services, AppSettings appSettings)
        {
            if (appSettings.DbServer == DbServer.MSSQL.ToString())
            {
                services.AddDbContext<KWSDBContext>(options =>
                options.UseSqlServer(
                    appSettings.ConnectionString,
                    b => b.MigrationsAssembly(typeof(KWSDBContext).Assembly.FullName))
                );
            }
            else if (appSettings.DbServer == DbServer.MySQL.ToString())
            {
               // services.AddDbContext<kwsDBContext>(options =>
               //options.UseMySql(appSettings.ConnectionString, MySqlServerVersion.LatestSupportedServerVersion,
               //b => b.MigrationsAssembly(typeof(SBMDbContext).Assembly.FullName))
               //);
            }
            services.AddScoped<IKWSDBContext>(provider => provider.GetService<KWSDBContext>());
        }
    }
}
