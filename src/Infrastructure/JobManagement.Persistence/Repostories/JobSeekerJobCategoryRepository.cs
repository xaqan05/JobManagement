using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class JobSeekerJobCategoryRepository : GenericRepository<JobSeekerJobCategory>, IJobSeekerJobCategoryRepository
{
    public JobSeekerJobCategoryRepository(AppDbContext _context) : base(_context) { }
}
