using JobManagement.Application.Common;
using JobManagement.Application.DTOs.JobSeeker;

namespace JobManagement.Application.Interfaces;
public interface IJobSeekerService
{
    Task<ApiResponse<JobSeekerCvGetDto>> CreateCvAsync(Guid userId, CreateCvDto dto);
    Task<ApiResponse<JobSeekerCvGetDto>> GetOwnCvAsync(Guid userId);
}
