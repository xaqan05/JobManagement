namespace JobManagement.Application.DTOs.Auth;
public class CompanyRegisterDto
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string VOEN { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
