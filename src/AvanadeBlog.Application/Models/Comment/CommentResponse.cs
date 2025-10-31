namespace AvanadeBlog.Application.Models.Comment
{
    /// <summary>
    /// Comment Response Payload
    /// </summary>
    public class CommentResponse
    {
        /// <summary>
        /// GUID Identifier of the Comment
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Title of the Comment
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Content of the Comment
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}
