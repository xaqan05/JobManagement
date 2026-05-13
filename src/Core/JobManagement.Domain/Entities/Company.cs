using JobManagement.Domain.Entities.Common;

namespace JobManagement.Domain.Entities;
public class Company : BaseEntity
{

    public Guid UserId { get; set; }

    public AppUser User { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string VOEN { get; set; } = null!;

    public string? Website { get; set; }

    public string? Address { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
