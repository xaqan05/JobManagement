namespace JobManagement.Application.DTOs.JobSeeker;
public class JobSeekerCertificateGetDto
{
    public Guid Id { get; set; }
    public string CertificateName { get; set; } = null!;
    public string IssuingOrganization { get; set; } = null!;
    public string? CertificateImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
