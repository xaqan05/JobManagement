using JobManagement.Domain.Entities.Common;

namespace JobManagement.Domain.Entities;
public class JobSeekerJobPosition : BaseEntity
{
    public Guid JobCategoryId { get; set; }
    public JobSeekerJobCategory JobCategory { get; set; } = null!;
    public string Name { get; set; } = null!;
}
