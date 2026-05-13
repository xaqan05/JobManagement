using JobManagement.Domain.Enums.JobSeeker;

namespace JobManagement.Application.DTOs.JobSeeker;
public class JobSeekerLanguageCreateDto
{
    public Guid LanguageId { get; set; }
    public LanguageLevel Level { get; set; }
}
