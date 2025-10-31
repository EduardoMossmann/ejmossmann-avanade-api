using AvanadeBlog.Domain.Entities;
using AvanadeBlog.Domain.Interfaces.Base;

namespace AvanadeBlog.Domain.Interfaces
{
    public interface IPostRepository : IBaseRepository<PostEntity> 
    {
        public Task<bool> TitleExistsAsync(string title); 
    }
}
