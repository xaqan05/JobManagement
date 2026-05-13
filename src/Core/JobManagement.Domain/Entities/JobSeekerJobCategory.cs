using JobManagement.Domain.Entities.Common;

namespace JobManagement.Domain.Entities;
public class JobSeekerJobCategory : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<JobSeekerJobPosition> Positions { get; set; } = new List<JobSeekerJobPosition>();
}
