using JobManagement.Domain.Enums.JobSeeker;

namespace JobManagement.Application.DTOs.JobSeeker;
public class JobSeekerCvGetDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhotoUrl { get; set; }
    public string? About { get; set; }
    public string? Address { get; set; }
    public DateTime? BirthDate { get; set; }
    public Guid? JobCategoryId { get; set; }
    public string? JobCategoryName { get; set; }
    public Guid? JobPositionId { get; set; }
    public string? JobPositionName { get; set; }
    public Gender? Gender { get; set; }
    public FamilyStatus? FamilyStatus { get; set; }
    public Citizenship? Citizenship { get; set; }
    public MilitaryStatus? MilitaryStatus { get; set; }
    public DriverLicense? DriverLicense { get; set; }
    public bool HasEducation { get; set; }
    public bool HasExperience { get; set; }
    public bool IsPublic { get; set; }
    public bool IsAnonym { get; set; }
    public List<JobSeekerPhoneGetDto> Phones { get; set; } = new();
    public List<JobSeekerEducationGetDto> Educations { get; set; } = new();
    public List<JobSeekerExperienceGetDto> Experiences { get; set; } = new();
    public List<JobSeekerLanguageGetDto> Languages { get; set; } = new();
    public List<JobSeekerSkillGetDto> Skills { get; set; } = new();
    public List<JobSeekerLinkGetDto> Links { get; set; } = new();
    public List<JobSeekerCertificateGetDto> Certificates { get; set; } = new();
}
