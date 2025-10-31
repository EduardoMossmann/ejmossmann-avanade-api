namespace AvanadeBlog.Application.Middleware
{
    public class UnauthorizedException : HttpResponseException
    {
        public UnauthorizedException(string? message, object? value = null) : base(401, message, value) { }
    }
}
