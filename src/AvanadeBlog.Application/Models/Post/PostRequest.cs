namespace AvanadeBlog.Application.Models.Post
{
    /// <summary>
    /// Blog Post Request Payload
    /// </summary>
    public class PostRequest
    {
        /// <summary>
        /// Title of the Blog Post
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Content of the Blog Post
        /// </summary>
        public string? Content { get; set; }
        public PostRequest() { }
    }
}
