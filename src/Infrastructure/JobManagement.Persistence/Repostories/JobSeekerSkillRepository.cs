using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class JobSeekerSkillRepository : GenericRepository<JobSeekerSkill>, IJobSeekerSkillRepository
{
    public JobSeekerSkillRepository(AppDbContext _context) : base(_context) { }
}
