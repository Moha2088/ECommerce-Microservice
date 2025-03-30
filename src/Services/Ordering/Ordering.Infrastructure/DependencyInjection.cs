using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DBConnection");
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.AddInterceptors(new AuditableEntityInterceptor());
                opt.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}