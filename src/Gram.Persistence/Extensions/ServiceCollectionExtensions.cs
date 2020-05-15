using Gram.Application.Interfaces;
using Gram.Persistence.Audit;
using Gram.Persistence.Data;
using Gram.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gram.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<AuditContext>(options => options.UseSqlServer(connectionString))
                .AddDbContext<DataContext>(options => options.UseSqlServer(connectionString))
                .AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString))
                .AddScoped<IAuditContext, AuditContext>()
                .AddScoped<IDataContext, DataContext>();
    }
}
