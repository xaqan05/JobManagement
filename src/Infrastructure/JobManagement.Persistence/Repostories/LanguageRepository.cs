using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
{
    public LanguageRepository(AppDbContext _context) : base(_context) { }
}
