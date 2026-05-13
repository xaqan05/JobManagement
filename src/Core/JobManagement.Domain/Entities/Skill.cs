using JobManagement.Domain.Entities.Common;

namespace JobManagement.Domain.Entities;
public class Skill : BaseEntity
{
    public string Name { get; set; } = null!;
    public bool IsSoft { get; set; }
}
