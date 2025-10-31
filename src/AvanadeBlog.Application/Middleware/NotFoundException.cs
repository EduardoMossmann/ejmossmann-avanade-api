namespace AvanadeBlog.Application.Middleware
{
    public class NotFoundException : HttpResponseException
    {
        public NotFoundException(string? message, object? value = null) : base(404, message, value) { }
    }
}
