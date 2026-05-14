using JobManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Persistence.Context;
public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<JobSeeker> JobSeekers { get; set; }
    public DbSet<JobSeekerPhone> JobSeekerPhones { get; set; }
    public DbSet<JobSeekerJobCategory> JobSeekerJobCategories { get; set; }
    public DbSet<JobSeekerJobPosition> JobSeekerJobPositions { get; set; }
    public DbSet<EducationInstitution> EducationInstitutions { get; set; }
    public DbSet<JobSeekerEducation> JobSeekerEducations { get; set; }
    public DbSet<JobSeekerExperience> JobSeekerExperiences { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<JobSeekerLanguage> JobSeekerLanguages { get; set; }
    public DbSet<Skill> CommonSkills { get; set; }
    public DbSet<JobSeekerSkill> JobSeekerSkills { get; set; }
    public DbSet<SocialPlatform> SocialPlatforms { get; set; }
    public DbSet<JobSeekerLink> JobSeekerLinks { get; set; }
    public DbSet<JobSeekerCertificate> JobSeekerCertificates { get; set; }


    public DbSet<Vacancy> Vacancies { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>(entity =>
        {
            entity.ToTable("Users");
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Surname).IsRequired().HasMaxLength(100);
            entity.Property(x => x.PhotoUrl).HasMaxLength(500);
        });

        builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

        builder.Entity<Company>(entity =>
        {
            entity.HasIndex(x => x.UserId).IsUnique();
            entity.HasIndex(x => x.VOEN).IsUnique();
            entity.Property(x => x.CompanyName).IsRequired().HasMaxLength(200);
            entity.Property(x => x.VOEN).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Email).HasMaxLength(256);
            entity.Property(x => x.Phone).HasMaxLength(50);
            entity.Property(x => x.Website).HasMaxLength(300);
            entity.Property(x => x.Address).HasMaxLength(500);
            entity.Property(x => x.Location).HasMaxLength(200);

            entity.HasOne(x => x.User)
                .WithOne(x => x.Company)
                .HasForeignKey<Company>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<JobSeeker>(entity =>
        {
            entity.HasIndex(x => x.UserId).IsUnique();
            entity.Property(x => x.Email).HasMaxLength(256);
            entity.Property(x => x.Address).HasMaxLength(500);

            entity.HasOne(x => x.User)
                .WithOne(x => x.JobSeeker)
                .HasForeignKey<JobSeeker>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.JobCategory)
                .WithMany()
                .HasForeignKey(x => x.JobCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.JobPosition)
                .WithMany()
                .HasForeignKey(x => x.JobPositionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<JobSeekerPhone>(entity =>
        {
            entity.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);
            entity.Property(x => x.CountryCode).IsRequired().HasMaxLength(10);
            entity.HasOne(x => x.JobSeeker)
                .WithMany(x => x.Phones)
                .HasForeignKey(x => x.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<JobSeekerJobCategory>(entity =>
        {
            entity.Property(x => x.Name).IsRequired().HasMaxLength(150);
        });

        builder.Entity<JobSeekerJobPosition>(entity =>
        {
            entity.Property(x => x.Name).IsRequired().HasMaxLength(150);
            entity.HasOne(x => x.JobCategory)
                .WithMany(x => x.Positions)
                .HasForeignKey(x => x.JobCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<EducationInstitution>(entity =>
        {
            entity.HasIndex(x => x.Name).IsUnique();
            entity.Property(x => x.Name).IsRequired().HasMaxLength(250);
        });

        builder.Entity<JobSeekerEducation>(entity =>
        {
            entity.Property(x => x.SpecialtyName).IsRequired().HasMaxLength(200);
            entity.HasOne(x => x.JobSeeker)
                .WithMany(x => x.Educations)
                .HasForeignKey(x => x.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Institution)
                .WithMany()
                .HasForeignKey(x => x.InstitutionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<JobSeekerExperience>(entity =>
        {
            entity.Property(x => x.CompanyName).IsRequired().HasMaxLength(200);
            entity.Property(x => x.PositionName).IsRequired().HasMaxLength(200);
            entity.HasOne(x => x.JobSeeker)
                .WithMany(x => x.Experiences)
                .HasForeignKey(x => x.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Language>(entity =>
        {
            entity.HasIndex(x => x.Name).IsUnique();
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        builder.Entity<JobSeekerLanguage>(entity =>
        {
            entity.HasIndex(x => new { x.JobSeekerId, x.LanguageId }).IsUnique();
            entity.HasOne(x => x.JobSeeker)
                .WithMany(x => x.Languages)
                .HasForeignKey(x => x.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Language)
                .WithMany()
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Skill>(entity =>
        {
            entity.ToTable("CommonSkills");
            entity.HasIndex(x => x.Name).IsUnique();
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        builder.Entity<JobSeekerSkill>(entity =>
        {
            entity.HasIndex(x => new { x.JobSeekerId, x.SkillId }).IsUnique();
            entity.HasOne(x => x.JobSeeker)
                .WithMany(x => x.Skills)
                .HasForeignKey(x => x.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Skill)
                .WithMany()
                .HasForeignKey(x => x.SkillId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<SocialPlatform>(entity =>
        {
            entity.HasIndex(x => x.Name).IsUnique();
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        builder.Entity<JobSeekerLink>(entity =>
        {
            entity.Property(x => x.Url).IsRequired().HasMaxLength(500);
            entity.HasOne(x => x.JobSeeker)
                .WithMany(x => x.Links)
                .HasForeignKey(x => x.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.SocialPlatform)
                .WithMany()
                .HasForeignKey(x => x.SocialPlatformId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<JobSeekerCertificate>(entity =>
        {
            entity.Property(x => x.CertificateName).IsRequired().HasMaxLength(200);
            entity.Property(x => x.IssuingOrganization).IsRequired().HasMaxLength(200);
            entity.Property(x => x.CertificateImageUrl).HasMaxLength(500);
            entity.HasOne(x => x.JobSeeker)
                .WithMany(x => x.Certificates)
                .HasForeignKey(x => x.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
