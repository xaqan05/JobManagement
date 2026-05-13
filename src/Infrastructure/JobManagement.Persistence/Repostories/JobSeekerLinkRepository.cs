using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class JobSeekerLinkRepository : GenericRepository<JobSeekerLink>, IJobSeekerLinkRepository
{
    public JobSeekerLinkRepository(AppDbContext _context) : base(_context) { }
}
