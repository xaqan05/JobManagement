using JobManagement.Domain.Entities.Common;

namespace JobManagement.Domain.Entities;
public class Language : BaseEntity
{
    public string Name { get; set; } = null!;
}
