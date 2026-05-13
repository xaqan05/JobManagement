using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class JobSeekerRepository : GenericRepository<JobSeeker>, IJobSeekerRepository
{
    public JobSeekerRepository(AppDbContext _context) : base(_context)
    {
    }
}
