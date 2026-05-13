using JobManagement.Domain.Entities.Common;
using JobManagement.Domain.Enums.JobSeeker;

namespace JobManagement.Domain.Entities;
public class JobSeeker : BaseEntity
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public string? About { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public DateTime? BirthDate { get; set; }

    public Guid? JobCategoryId { get; set; }
    public JobSeekerJobCategory? JobCategory { get; set; }
    public Guid? JobPositionId { get; set; }
    public JobSeekerJobPosition? JobPosition { get; set; }

    public Gender? Gender { get; set; }
    public FamilyStatus? FamilyStatus { get; set; }
    public Citizenship? Citizenship { get; set; }
    public MilitaryStatus? MilitaryStatus { get; set; }
    public DriverLicense? DriverLicense { get; set; }

    public bool HasEducation { get; set; }
    public bool HasExperience { get; set; }

    public bool IsPublic { get; set; }
    public bool IsAnonym { get; set; }

    public ICollection<JobSeekerPhone> Phones { get; set; } = new List<JobSeekerPhone>();
    public ICollection<JobSeekerEducation> Educations { get; set; } = new List<JobSeekerEducation>();
    public ICollection<JobSeekerExperience> Experiences { get; set; } = new List<JobSeekerExperience>();
    public ICollection<JobSeekerLanguage> Languages { get; set; } = new List<JobSeekerLanguage>();
    public ICollection<JobSeekerSkill> Skills { get; set; } = new List<JobSeekerSkill>();
    public ICollection<JobSeekerLink> Links { get; set; } = new List<JobSeekerLink>();
    public ICollection<JobSeekerCertificate> Certificates { get; set; } = new List<JobSeekerCertificate>();
}
