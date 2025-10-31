using AvanadeBlog.Domain;
using AvanadeBlog.Domain.Entities;
using AvanadeBlog.Domain.Interfaces;
using AvanadeBlog.Domain.FilterParams.Base;
using AvanadeBlog.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AvanadeBlog.Infrastructure.Data.Repositories
{
    public class PostRepository : BaseRepository<PostEntity>, IPostRepository
    {
        public PostRepository(AvanadeBlogDbContext AvanadeBlogDbContext) : base(AvanadeBlogDbContext) { }

        public override async Task<PaginatedResult<PostEntity>> GetPaginatedAsync(BasePaginatedFilterParams<PostEntity> filterParams, IQueryable<PostEntity>? query = null)
        {
            query = _set.AsNoTracking()
                .Include(x => x.Comments);

            return await base.GetPaginatedAsync(filterParams, query);
        }

        public override Task<PostEntity?> GetByIdAsync(Guid id, IQueryable<PostEntity>? query = null)
        {
            query = _set.AsQueryable()
                .Include(x => x.Comments);

            return base.GetByIdAsync(id, query);
        }

        public async Task<bool> TitleExistsAsync(string title)
        {
            return await _set.AsQueryable()
               .AnyAsync(x => x.Title == title);
        }
    }
}
