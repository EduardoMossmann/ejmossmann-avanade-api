namespace AvanadeBlog.Application.Middleware
{
    public class BadRequestException : HttpResponseException
    {
        public BadRequestException(string? message, object? value = null) : base(400, message, value) { }
    }
}
