using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Persistence.Repostories;
public class JobSeekerRepository : GenericRepository<JobSeeker>, IJobSeekerRepository
{
    public JobSeekerRepository(AppDbContext _context) : base(_context)
    {
    }

    public async Task<JobSeeker?> GetByUserIdAsync(Guid userId)
    {
        return await GetWhere(x => x.UserId == userId).FirstOrDefaultAsync();
    }

    public async Task<JobSeeker?> GetByUserIdWithCvAsync(Guid userId)
    {
        return await GetWhere(x => x.UserId == userId)
            .Include(x => x.User)
            .Include(x => x.JobCategory)
            .Include(x => x.JobPosition)
            .Include(x => x.Phones)
            .Include(x => x.Educations)
                .ThenInclude(x => x.Institution)
            .Include(x => x.Experiences)
            .Include(x => x.Languages)
                .ThenInclude(x => x.Language)
            .Include(x => x.Skills)
                .ThenInclude(x => x.Skill)
            .Include(x => x.Links)
                .ThenInclude(x => x.SocialPlatform)
            .Include(x => x.Certificates)
            .FirstOrDefaultAsync();
    }
}
