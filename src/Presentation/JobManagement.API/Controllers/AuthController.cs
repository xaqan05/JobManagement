using JobManagement.Application.DTOs.Auth;
using JobManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register/company")]
    public async Task<IActionResult> RegisterCompany(CompanyRegisterDto dto)
    {
        var response = await _authService.RegisterCompanyAsync(dto);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("register/jobseeker")]
    public async Task<IActionResult> RegisterJobSeeker(JobSeekerRegisterDto dto)
    {
        var response = await _authService.RegisterJobSeekerAsync(dto);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var response = await _authService.LoginAsync(dto);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }
}
