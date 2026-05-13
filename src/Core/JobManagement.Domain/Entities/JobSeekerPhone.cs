using JobManagement.Domain.Entities.Common;

namespace JobManagement.Domain.Entities;
public class JobSeekerPhone : BaseEntity
{
    public Guid JobSeekerId { get; set; }
    public JobSeeker JobSeeker { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string CountryCode { get; set; } = null!;
}
