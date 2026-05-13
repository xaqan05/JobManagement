using JobManagement.Domain.Entities;

namespace JobManagement.Application.Interfaces;
public interface IJwtService
{
    string GenerateToken(AppUser user);
}
