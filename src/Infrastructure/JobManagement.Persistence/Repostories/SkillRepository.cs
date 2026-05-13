using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class SkillRepository : GenericRepository<Skill>, ISkillRepository
{
    public SkillRepository(AppDbContext _context) : base(_context) { }
}
