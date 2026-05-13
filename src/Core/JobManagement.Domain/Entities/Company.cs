using JobManagement.Domain.Entities.Common;

namespace JobManagement.Domain.Entities;
public class Company : BaseEntity
{

    public Guid UserId { get; set; }

    public AppUser User { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string VOEN { get; set; } = null!;

    public DateTime? FoundedDate { get; set; }
    public int? EmployeeCount { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Description { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }
    public string? Location { get; set; }
}
