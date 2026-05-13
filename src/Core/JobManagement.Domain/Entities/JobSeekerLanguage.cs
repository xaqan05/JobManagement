using JobManagement.Domain.Entities.Common;
using JobManagement.Domain.Enums.JobSeeker;

namespace JobManagement.Domain.Entities;
public class JobSeekerLanguage : BaseEntity
{
    public Guid JobSeekerId { get; set; }
    public JobSeeker JobSeeker { get; set; } = null!;
    public Guid LanguageId { get; set; }
    public Language Language { get; set; } = null!;
    public LanguageLevel Level { get; set; }
}
