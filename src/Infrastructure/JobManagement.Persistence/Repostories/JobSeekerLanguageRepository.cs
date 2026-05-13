using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class JobSeekerLanguageRepository : GenericRepository<JobSeekerLanguage>, IJobSeekerLanguageRepository
{
    public JobSeekerLanguageRepository(AppDbContext _context) : base(_context) { }
}
