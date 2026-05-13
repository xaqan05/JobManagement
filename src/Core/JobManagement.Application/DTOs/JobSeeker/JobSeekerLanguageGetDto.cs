using JobManagement.Domain.Enums.JobSeeker;

namespace JobManagement.Application.DTOs.JobSeeker;
public class JobSeekerLanguageGetDto
{
    public Guid Id { get; set; }
    public Guid LanguageId { get; set; }
    public string LanguageName { get; set; } = null!;
    public LanguageLevel Level { get; set; }
}
