using JobManagement.Application.Implements;
using JobManagement.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JobManagement.Application;
public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IJobSeekerService, JobSeekerService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
