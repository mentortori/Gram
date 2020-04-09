using Gram.Application.Interfaces;
using Gram.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gram.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AuditContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IAuditContext, AuditContext>();
            services.AddScoped<IDataContext, DataContext>();
            return services;
        }
    }
}
