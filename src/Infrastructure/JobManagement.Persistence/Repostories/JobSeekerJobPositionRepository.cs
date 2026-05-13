using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class JobSeekerJobPositionRepository : GenericRepository<JobSeekerJobPosition>, IJobSeekerJobPositionRepository
{
    public JobSeekerJobPositionRepository(AppDbContext _context) : base(_context) { }
}
