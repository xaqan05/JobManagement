using JobManagement.Application.Common;
using JobManagement.Application.DTOs.Auth;

namespace JobManagement.Application.Interfaces;
public interface IAuthService
{
    Task<ApiResponse<AuthResponseDto>> RegisterCompanyAsync(CompanyRegisterDto dto);
    Task<ApiResponse<AuthResponseDto>> RegisterJobSeekerAsync(JobSeekerRegisterDto dto);
    Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto dto);
}
