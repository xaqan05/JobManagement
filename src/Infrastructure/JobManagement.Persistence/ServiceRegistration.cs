using JobManagement.Application.Repositories;
using JobManagement.Persistence.Context;
using JobManagement.Persistence.Repostories;
using Microsoft.Extensions.DependencyInjection;

namespace JobManagement.Persistence;
public static class ServiceRegistration
{
    public static IServiceCollection AddMsSql(this IServiceCollection services, string connStr)
    {
        services.AddSqlServer<AppDbContext>(connStr);
        return services;
    }

    public static IServiceCollection AddRepos(this IServiceCollection services)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();


        return services;
    }
}
