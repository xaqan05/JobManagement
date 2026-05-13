namespace JobManagement.Application.DTOs.JobSeeker;
public class JobSeekerLinkCreateDto
{
    public Guid SocialPlatformId { get; set; }
    public string Url { get; set; } = null!;
}
