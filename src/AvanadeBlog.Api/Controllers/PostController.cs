using AvanadeBlog.Application.Interfaces;
using AvanadeBlog.Application.Models.Comment;
using AvanadeBlog.Application.Models.Post;
using AvanadeBlog.Domain;
using AvanadeBlog.Domain.FilterParams;
using AvanadeBlog.Domain.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvanadeBlog.Api.Controllers
{
    /// <summary>
    /// Post Controller
    /// </summary>
    [Authorize(Roles = IdentityUserAccessRoles.USER + "," + IdentityUserAccessRoles.ADMIN)]
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;

        private readonly IBlogService _blogService;

        /// <summary>
        /// Post Controller Constructor
        /// </summary>
        public PostController(ILogger<PostController> logger, IBlogService blogService)
        {
            _logger = logger;
            _blogService = blogService;
        }

        /// <summary>
        /// Search a paginated list of Posts
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PostResponse>), 200)]
        public Task<PaginatedResult<PostResponse>> GetAsync([FromQuery] PostFilterParams postFilterParams)
        {
            _logger.LogInformation("PostController - GetAsync. Request: {Request}", postFilterParams);
            return _blogService.GetAsync(postFilterParams);
        }

        /// <summary>
        /// Retrieve a Post by it's Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PostCompleteResponse), 200)]
        public Task<PostCompleteResponse> GetByIdAsync([FromRoute] Guid id)
        {
            _logger.LogInformation("PostController - GetByIdAsync. Request: {Request}", id);
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _blogService.GetByIdAsync(id);
        }

        /// <summary>
        /// Create a Post using Title and Content
        /// </summary>
        [HttpPost]
        [Authorize(Roles = IdentityUserAccessRoles.ADMIN)]
        [ProducesResponseType(typeof(PostCompleteResponse), 200)]
        public Task<PostCompleteResponse> PostAsync([FromBody] PostRequest postRequest)
        {
            _logger.LogInformation("PostController - PostAsync. Request: {Request}", postRequest);
            return _blogService.PostAsync(postRequest);
        }

        /// <summary>
        /// Add a Comment with Title and Content to an existing Post
        /// </summary>
        [HttpPost("{id:guid}/comments")]
        [ProducesResponseType(typeof(PostCompleteResponse), 200)]
        public Task<PostCompleteResponse> PostCommentsAsync([FromRoute] Guid id, [FromBody] CommentRequest commentRequest)
        {
            _logger.LogInformation("PostController - PostCommentsAsync. Request: {Request}", commentRequest);
            return _blogService.PostCommentsAsync(id, commentRequest);
        }
    }
}
