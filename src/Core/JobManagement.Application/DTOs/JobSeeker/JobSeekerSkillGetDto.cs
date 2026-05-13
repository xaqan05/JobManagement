namespace JobManagement.Application.DTOs.JobSeeker;
public class JobSeekerSkillGetDto
{
    public Guid Id { get; set; }
    public Guid SkillId { get; set; }
    public string SkillName { get; set; } = null!;
    public bool IsSoft { get; set; }
}
