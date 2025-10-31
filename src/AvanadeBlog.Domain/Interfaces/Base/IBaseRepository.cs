using AvanadeBlog.Domain.Entities.Base;
using AvanadeBlog.Domain.FilterParams.Base;

namespace AvanadeBlog.Domain.Interfaces.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<PaginatedResult<T>> GetPaginatedAsync(BasePaginatedFilterParams<T> filterParams, IQueryable<T>? query = null);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T?> GetByIdAsync(Guid id, IQueryable<T>? query = null);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
