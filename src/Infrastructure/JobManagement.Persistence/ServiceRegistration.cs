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
        services.AddScoped<IJobSeekerRepository, JobSeekerRepository>();
        services.AddScoped<IJobSeekerPhoneRepository, JobSeekerPhoneRepository>();
        services.AddScoped<IJobSeekerEducationRepository, JobSeekerEducationRepository>();
        services.AddScoped<IJobSeekerExperienceRepository, JobSeekerExperienceRepository>();
        services.AddScoped<IJobSeekerLanguageRepository, JobSeekerLanguageRepository>();
        services.AddScoped<IJobSeekerSkillRepository, JobSeekerSkillRepository>();
        services.AddScoped<IJobSeekerLinkRepository, JobSeekerLinkRepository>();
        services.AddScoped<IJobSeekerCertificateRepository, JobSeekerCertificateRepository>();
        services.AddScoped<IEducationInstitutionRepository, EducationInstitutionRepository>();
        services.AddScoped<IJobSeekerJobCategoryRepository, JobSeekerJobCategoryRepository>();
        services.AddScoped<IJobSeekerJobPositionRepository, JobSeekerJobPositionRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<ISocialPlatformRepository, SocialPlatformRepository>();

        return services;
    }
}
