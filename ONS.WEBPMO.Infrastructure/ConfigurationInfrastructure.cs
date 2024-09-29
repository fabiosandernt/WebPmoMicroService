using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ONS.WEBPMO.Domain.Repositories.Impl.Repositories;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;


namespace ONS.WEBPMO.Infrastructure
{
    public static class ConfigurationInfrastructure
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WEBPMODbContext>(c =>
            {
                c.UseSqlServer(connectionString);
            });


            services.AddScoped(typeof(Repository<>));
            services.AddScoped<IInsumoRepository, InsumoRepository>();

            return services;
        }
    }
}
