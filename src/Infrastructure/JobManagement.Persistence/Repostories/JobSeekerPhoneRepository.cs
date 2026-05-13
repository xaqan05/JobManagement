using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class JobSeekerPhoneRepository : GenericRepository<JobSeekerPhone>, IJobSeekerPhoneRepository
{
    public JobSeekerPhoneRepository(AppDbContext _context) : base(_context) { }
}
