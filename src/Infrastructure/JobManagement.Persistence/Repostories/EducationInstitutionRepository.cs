using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class EducationInstitutionRepository : GenericRepository<EducationInstitution>, IEducationInstitutionRepository
{
    public EducationInstitutionRepository(AppDbContext _context) : base(_context) { }
}
