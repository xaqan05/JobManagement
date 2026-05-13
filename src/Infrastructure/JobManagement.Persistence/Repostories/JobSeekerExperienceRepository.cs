using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class JobSeekerExperienceRepository : GenericRepository<JobSeekerExperience>, IJobSeekerExperienceRepository
{
    public JobSeekerExperienceRepository(AppDbContext _context) : base(_context) { }
}
