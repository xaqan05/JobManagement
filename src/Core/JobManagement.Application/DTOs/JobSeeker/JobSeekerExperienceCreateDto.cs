namespace JobManagement.Application.DTOs.JobSeeker;
public class JobSeekerExperienceCreateDto
{
    public string CompanyName { get; set; } = null!;
    public string PositionName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrentlyWorking { get; set; }
    public string? Responsibilities { get; set; }
}
