using JobManagement.Domain.Entities.Common;

namespace JobManagement.Domain.Entities;
public class JobSeeker : BaseEntity
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public string? About { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public DateTime? BirthDate { get; set; }

    public int? Gender { get; set; }
    public int? FamilyStatus { get; set; }
    public int? Citizenship { get; set; }
    public int? MilitaryStatus { get; set; }
    public int? DriverLicense { get; set; }

    public bool HasEducation { get; set; }
    public bool HasExperience { get; set; }

    public bool IsPublic { get; set; }
    public bool IsAnonym { get; set; }
}
