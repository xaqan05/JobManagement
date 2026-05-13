using JobManagement.Domain.Entities.Common;

namespace JobManagement.Domain.Entities;
public class JobSeekerCertificate : BaseEntity
{
    public Guid JobSeekerId { get; set; }
    public JobSeeker JobSeeker { get; set; } = null!;
    public string CertificateName { get; set; } = null!;
    public string IssuingOrganization { get; set; } = null!;
    public string? CertificateImageUrl { get; set; }
}
