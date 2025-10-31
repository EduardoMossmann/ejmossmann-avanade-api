namespace AvanadeBlog.Application.Models.Comment
{
    /// <summary>
    /// Comment Request Payload
    /// </summary>
    public class CommentRequest
    {
        /// <summary>
        /// Title of the Comment
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Content of the Comment
        /// </summary>
        public string? Content { get; set; }
    }
}
