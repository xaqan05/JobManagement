namespace JobManagement.Application.DTOs.JobSeeker;
public class JobSeekerLinkGetDto
{
    public Guid Id { get; set; }
    public Guid SocialPlatformId { get; set; }
    public string SocialPlatformName { get; set; } = null!;
    public string Url { get; set; } = null!;
}
