namespace JobManagement.Application.DTOs.JobSeeker;
public class JobSeekerPhoneGetDto
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string CountryCode { get; set; } = null!;
}
