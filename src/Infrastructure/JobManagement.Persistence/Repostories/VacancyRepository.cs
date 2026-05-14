using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;

public class VacancyRepository : GenericRepository<Vacancy>, IVacancyRepository
{
    public VacancyRepository(AppDbContext _context) : base(_context)
    {
    }
}
