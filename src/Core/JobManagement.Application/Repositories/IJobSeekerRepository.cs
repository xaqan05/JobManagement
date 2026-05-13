using JobManagement.Domain.Entities;

namespace JobManagement.Application.Repositories;
public interface IJobSeekerRepository : IGenericRepository<JobSeeker>
{
    Task<JobSeeker?> GetByUserIdAsync(Guid userId);
    Task<JobSeeker?> GetByUserIdWithCvAsync(Guid userId);
}
