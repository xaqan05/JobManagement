using JobManagement.Domain.Entities;
using JobManagement.Domain.Entities.Test;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Persistence.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    //public DbSet<Company> Companies { get; set; }
    //public DbSet<JobSeeker> JobSeekers { get; set; }
    //public DbSet<JobSeekerPhone> JobSeekerPhones { get; set; }
    //public DbSet<JobSeekerJobCategory> JobSeekerJobCategories { get; set; }
    //public DbSet<JobSeekerJobPosition> JobSeekerJobPositions { get; set; }
    //public DbSet<EducationInstitution> EducationInstitutions { get; set; }
    //public DbSet<JobSeekerEducation> JobSeekerEducations { get; set; }
    //public DbSet<JobSeekerExperience> JobSeekerExperiences { get; set; }
    //public DbSet<Language> Languages { get; set; }
    //public DbSet<JobSeekerLanguage> JobSeekerLanguages { get; set; }
    //public DbSet<Skill> CommonSkills { get; set; }
    //public DbSet<JobSeekerSkill> JobSeekerSkills { get; set; }
    //public DbSet<SocialPlatform> SocialPlatforms { get; set; }
    //public DbSet<JobSeekerLink> JobSeekerLinks { get; set; }
    //public DbSet<JobSeekerCertificate> JobSeekerCertificates { get; set; }


    //public DbSet<Vacancy> Vacancies { get; set; }

    public DbSet<Service> Services => Set<Service>();
    public DbSet<ServiceVariant> ServiceVariants => Set<ServiceVariant>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Operator> Operators => Set<Operator>();



    protected override void OnModelCreating(ModelBuilder b)
    {
        // Sequence-lərin yaradılması
        b.HasSequence<long>("sale_code_seq").StartsAt(100001);
        b.HasSequence<long>("customer_code_seq").StartsAt(10000001);

        b.Entity<Service>(e =>
        {
            e.Property(x => x.Name).HasMaxLength(200);
            e.Property(x => x.Unit).HasMaxLength(20);
            e.Property(x => x.Note).HasMaxLength(200);
        });

        b.Entity<ServiceVariant>(e =>
        {
            e.Property(x => x.Price).HasPrecision(10, 2);

            // DÜZƏLİŞ: 'b.HasOne' yox, 'e.HasOne' olmalıdır!
            e.HasOne(x => x.Service)
             .WithMany(s => s.Variants)
             .HasForeignKey(x => x.ServiceId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(x => new { x.ServiceId, x.Variant }).IsUnique();
        });

        b.Entity<Country>(e =>
        {
            e.Property(x => x.Name).HasMaxLength(100);
            e.Property(x => x.IsoCode).HasMaxLength(2);
            e.HasIndex(x => x.IsoCode).IsUnique();
            e.HasIndex(x => x.LegacyId).IsUnique();
        });

        b.Entity<Customer>(e =>
        {
            e.HasIndex(x => x.IdentityNo).IsUnique();
            e.HasIndex(x => x.CustomerCode).IsUnique();

            // 🛠️ DÜZƏLİŞ: PostgreSQL 'nextval' silindi, SQL Server üçün rəsmi sintaksis yazıldı
            e.Property(x => x.CustomerCode).HasDefaultValueSql("NEXT VALUE FOR customer_code_seq");

            e.Property(x => x.IdentityNo).HasMaxLength(20);
            e.Property(x => x.FirstName).HasMaxLength(100);
            e.Property(x => x.LastName).HasMaxLength(100);
            e.Property(x => x.CompanyName).HasMaxLength(200);
            e.HasOne(x => x.CitizenshipCountry).WithMany().HasForeignKey(x => x.CitizenshipCountryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        b.Entity<Sale>(e =>
        {
            e.HasIndex(x => x.SaleCode).IsUnique();

            // 🛠️ DÜZƏLİŞ: PostgreSQL 'nextval' silindi, SQL Server üçün rəsmi sintaksis yazıldı
            e.Property(x => x.SaleCode).HasDefaultValueSql("NEXT VALUE FOR sale_code_seq");

            e.HasIndex(x => x.ClientRequestId).IsUnique();
            e.HasIndex(x => x.Status);
            e.Property(x => x.CustomerNameSnapshot).HasMaxLength(200);
            e.Property(x => x.CustomerIdentitySnapshot).HasMaxLength(20);
            e.Property(x => x.Phone).HasMaxLength(30);
            e.Property(x => x.CargoType).HasMaxLength(200);
            e.Property(x => x.VehiclePlate).HasMaxLength(20);
            e.Property(x => x.TotalAmount).HasPrecision(10, 2);

            e.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.DriverCitizenship).WithMany().HasForeignKey(x => x.DriverCitizenshipId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🛠️ DÜZƏLİŞ (Qərar #3): PostgreSQL "xmin" (uint) kölgə sahəsi silindi. 
            // Yerınə SQL Server üçün standart olan byte[] tipli RowVersion (timestamp) əlavə edildi.
            e.Property<byte[]>("RowVersion")
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        b.Entity<SaleItem>(e =>
        {
            e.Property(x => x.ServiceName).HasMaxLength(200);
            e.Property(x => x.VariantName).HasMaxLength(50);
            e.Property(x => x.Unit).HasMaxLength(20);
            e.Property(x => x.UnitPrice).HasPrecision(10, 2);
            e.Property(x => x.Total).HasPrecision(10, 2);
            e.HasOne(x => x.Sale).WithMany(s => s.Items).HasForeignKey(x => x.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
