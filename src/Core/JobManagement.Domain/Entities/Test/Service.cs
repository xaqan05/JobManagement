using JobManagement.Domain.Enums;

namespace JobManagement.Domain.Entities.Test;

public class Service
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ServiceCategory Category { get; set; }
    public string Unit { get; set; } = "ədəd";
    public string? Note { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<ServiceVariant> Variants { get; set; } = [];
}

