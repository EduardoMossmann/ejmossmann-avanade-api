using AvanadeBlog.Domain.Entities.Base;

namespace AvanadeBlog.Domain.Entities
{
    public class PostEntity : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public IList<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
    }
}
