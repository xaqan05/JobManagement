using JobManagement.Domain.Entities.Common;

namespace JobManagement.Domain.Entities;
public class JobSeekerExperience : BaseEntity
{
    public Guid JobSeekerId { get; set; }
    public JobSeeker JobSeeker { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string PositionName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrentlyWorking { get; set; }
    public string? Responsibilities { get; set; }
}
