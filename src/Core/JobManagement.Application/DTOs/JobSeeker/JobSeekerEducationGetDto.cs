using JobManagement.Domain.Enums.JobSeeker;

namespace JobManagement.Application.DTOs.JobSeeker;
public class JobSeekerEducationGetDto
{
    public Guid Id { get; set; }
    public Guid InstitutionId { get; set; }
    public string InstitutionName { get; set; } = null!;
    public string SpecialtyName { get; set; } = null!;
    public EducationLevel EducationLevel { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrentlyStudying { get; set; }
}
