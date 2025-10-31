using AvanadeBlog.Application.Models.Comment;
using AvanadeBlog.Application.Models.Post;
using AvanadeBlog.Domain;
using AvanadeBlog.Domain.FilterParams;


namespace AvanadeBlog.Application.Interfaces
{
    public interface IBlogService
    {
        Task<PaginatedResult<PostResponse>> GetAsync(PostFilterParams postFilterParams);
        Task<PostCompleteResponse> PostAsync(PostRequest post);
        Task<PostCompleteResponse> GetByIdAsync(Guid id);
        Task<PostCompleteResponse> PostCommentsAsync(Guid id, CommentRequest commentRequest);
    }
}
