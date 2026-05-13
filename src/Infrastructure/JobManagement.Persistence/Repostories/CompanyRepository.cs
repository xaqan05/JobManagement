using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    public CompanyRepository(AppDbContext _context) : base(_context)
    {
    }
}
