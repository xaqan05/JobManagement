using JobManagement.Domain.Entities.Common;
using System.Linq.Expressions;

namespace JobManagement.Application.Repositories;
public interface IGenericRepository<T> where T : BaseEntity, new()
{
    IQueryable<T> GetAll(bool tracking = true);
    Task<T?> GetByIdAsync(Guid id);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true);
    Task<bool> IsExistAsync(Guid id);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
    Task CreateAsync(T entity);
    void Remove(T entity);
    Task<bool> RemoveAsync(Guid id);
    Task<int> SaveAsync();

}
