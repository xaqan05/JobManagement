using JobManagement.Domain.Enums.Role;

namespace JobManagement.Application.DTOs.Auth;
public class AuthResponseDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = null!;
    public UserType UserType { get; set; }
    public string? PhotoUrl { get; set; }
    public string Token { get; set; } = null!;
}
