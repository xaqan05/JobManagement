using JobManagement.Domain.Enums.JobSeeker;

namespace JobManagement.Application.DTOs.JobSeeker;
public class CreateCvDto
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? About { get; set; }
    public string? Address { get; set; }
    public DateTime? BirthDate { get; set; }
    public Guid? JobCategoryId { get; set; }
    public Guid? JobPositionId { get; set; }
    public Gender? Gender { get; set; }
    public FamilyStatus? FamilyStatus { get; set; }
    public Citizenship? Citizenship { get; set; }
    public MilitaryStatus? MilitaryStatus { get; set; }
    public DriverLicense? DriverLicense { get; set; }
    public List<JobSeekerPhoneCreateDto>? Phones { get; set; }
    public List<JobSeekerEducationCreateDto>? Educations { get; set; }
    public List<JobSeekerExperienceCreateDto>? Experiences { get; set; }
    public List<JobSeekerLanguageCreateDto>? Languages { get; set; }
    public List<JobSeekerSkillCreateDto>? Skills { get; set; }
    public List<JobSeekerLinkCreateDto>? Links { get; set; }
    public List<JobSeekerCertificateCreateDto>? Certificates { get; set; }
}
