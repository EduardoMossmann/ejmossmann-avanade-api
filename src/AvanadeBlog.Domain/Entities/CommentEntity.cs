using AvanadeBlog.Domain.Entities.Base;

namespace AvanadeBlog.Domain.Entities
{
    public class CommentEntity : BaseEntity
    {
        public string? Title { get; set; }
        public string Content { get; set; } = string.Empty;
        public CommentEntity() { }
    }
}
