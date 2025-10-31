namespace AvanadeBlog.Application.Models.Post
{
    /// <summary>
    /// Blog Post Response Payload
    /// </summary>
    public class PostResponse
    {
        /// <summary>
        /// GUID Identifier of the Blog Post
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Title of the Blog Post
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Number of comments related with the Blog Post
        /// </summary>
        public int NumberOfComments { get; set; }
    }
}
