namespace AvanadeBlog.Domain
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(int pageSize, int pageCount, int total, IEnumerable<T> data)
        {
            PageCount = pageCount;
            PageSize = pageSize;
            Total = total;
            Data = data;
        }

        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
