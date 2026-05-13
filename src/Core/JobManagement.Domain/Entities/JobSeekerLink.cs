using JobManagement.Domain.Entities.Common;

namespace JobManagement.Domain.Entities;
public class JobSeekerLink : BaseEntity
{
    public Guid JobSeekerId { get; set; }
    public JobSeeker JobSeeker { get; set; } = null!;
    public Guid SocialPlatformId { get; set; }
    public SocialPlatform SocialPlatform { get; set; } = null!;
    public string Url { get; set; } = null!;
}
