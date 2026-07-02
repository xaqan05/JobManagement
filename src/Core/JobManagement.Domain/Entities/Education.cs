using JobManagement.Domain.Entities.Common;
using JobManagement.Domain.Enums.JobSeeker;

namespace JobManagement.Domain.Entities;
public class Education : BaseEntity
{
    public Guid JobSeekerId { get; set; }
    public JobSeeker JobSeeker { get; set; } = null!;
    public Guid InstitutionId { get; set; }
    public EducationInstitution Institution { get; set; } = null!;
    public string SpecialtyName { get; set; } = null!;
    public EducationLevel EducationLevel { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrentlyStudying { get; set; }
}
