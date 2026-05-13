namespace JobManagement.Application.DTOs.JobSeeker;
public class JobSeekerCertificateCreateDto
{
    public string CertificateName { get; set; } = null!;
    public string IssuingOrganization { get; set; } = null!;
    public string? CertificateImageUrl { get; set; }
}
