using JobManagement.Domain.Enums.Role;
using Microsoft.AspNetCore.Identity;

namespace JobManagement.Domain.Entities;
public class AppUser : IdentityUser<Guid>
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? ProfilePictureUrl { get; set; }
    public bool IsEmailConfirmed { get; set; }

    public UserType UserType { get; set; }
    public DateTime CreatedAt { get; set; }
}
