using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class JobSeekerCertificateRepository : GenericRepository<JobSeekerCertificate>, IJobSeekerCertificateRepository
{
    public JobSeekerCertificateRepository(AppDbContext _context) : base(_context) { }
}
