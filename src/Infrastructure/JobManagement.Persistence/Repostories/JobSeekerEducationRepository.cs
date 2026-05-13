using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class JobSeekerEducationRepository : GenericRepository<JobSeekerEducation>, IJobSeekerEducationRepository
{
    public JobSeekerEducationRepository(AppDbContext _context) : base(_context) { }
}
