using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities.Common;
using JobManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JobManagement.Persistence.Repostories;

public class GenericRepository<T>(AppDbContext _context) : IGenericRepository<T> where T : BaseEntity, new()
{
    protected DbSet<T> Table => _context.Set<T>();
    public async Task CreateAsync(T entity)
    {
        await Table.AddAsync(entity);

    }

    // GET ALL
    public IQueryable<T> GetAll(bool tracking = true)
        => tracking ? Table : Table.AsNoTracking();


    public async Task<T?> GetByIdAsync(Guid id) => await Table.FindAsync(id);


    // FILTER
    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true)
        => tracking
            ? Table.Where(expression)
            : Table.AsNoTracking().Where(expression);

    public async Task<bool> IsExistAsync(Guid id) => await Table.AnyAsync(t => t.Id == id);

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression) => await Table.AnyAsync(expression);

    public void Remove(T entity) => Table.Remove(entity);

    public async Task<bool> RemoveAsync(Guid id)
    {
        int result = await Table.Where(x => x.Id == id).ExecuteDeleteAsync();

        return result > 0;
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
}
