using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Persistence.Context;

namespace JobManagement.Persistence.Repostories;
public class SocialPlatformRepository : GenericRepository<SocialPlatform>, ISocialPlatformRepository
{
    public SocialPlatformRepository(AppDbContext _context) : base(_context) { }
}
