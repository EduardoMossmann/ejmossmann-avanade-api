using AvanadeBlog.Domain.Entities.Base;

namespace AvanadeBlog.Domain.FilterParams.Base
{
    public class BasePaginatedFilterParams<T> where T : BaseEntity
    {
        public int PageSize { get; set; } = 9999;
        public int PageNumber { get; set; } = 0;
        public string OrderBy { get; set; } = nameof(BaseEntity.Created);
        public bool OrderByDescending { get; set; } = true;
    }
}
