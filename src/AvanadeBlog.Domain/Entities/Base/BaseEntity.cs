
namespace AvanadeBlog.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime Created { get; protected set; }
        public string? CreatedBy { get; protected set; }
        public void SetCreatedBy(string createdBy) => CreatedBy = createdBy;
    }
}
