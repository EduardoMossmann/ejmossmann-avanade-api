using AutoMapper;
using AvanadeBlog.Application.Interfaces;
using AvanadeBlog.Application.Middleware;
using AvanadeBlog.Application.Models.Comment;
using AvanadeBlog.Application.Models.Post;
using AvanadeBlog.Application.Validators;
using AvanadeBlog.Domain;
using AvanadeBlog.Domain.Entities;
using AvanadeBlog.Domain.Entities.Base;
using AvanadeBlog.Domain.FilterParams;
using AvanadeBlog.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AvanadeBlog.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly CommentRequestValidator _commentRequestValidator;
        private readonly PostRequestValidator _postRequestValidator;
        private readonly ILogger<BlogService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BlogService(
            IPostRepository postRepository, 
            IMapper mapper,
            CommentRequestValidator commentRequestValidator,
            PostRequestValidator postRequestValidator,
            ILogger<BlogService> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _commentRequestValidator = commentRequestValidator;
            _postRequestValidator = postRequestValidator;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<PaginatedResult<PostResponse>> GetAsync(PostFilterParams postFilterParams)
        {
            var postsPaginatedResult = await _postRepository.GetPaginatedAsync(postFilterParams);

            var response = _mapper.Map<PaginatedResult<PostResponse>>(postsPaginatedResult);
            _logger.LogInformation("PostService - GetAsync success. Response: {Response}", response);
            return response;
        }

        public async Task<PostCompleteResponse> GetByIdAsync(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);

            var response = _mapper.Map<PostCompleteResponse>(post);
            _logger.LogInformation("PostService - GetByIdAsync success. Response: {Response}", response);
            return response;
        }

        public async Task<PostCompleteResponse> PostAsync(PostRequest postRequest)
        {
            var validation = await _postRequestValidator.ValidateAsync(postRequest);

            if (!validation.IsValid)
            {
                _logger.LogError("PostService - PostAsync - Validation Error. ValidationError: {ValidationError}", validation.ToDictionary());
                throw new BadRequestException("Validation Error", validation.ToDictionary());
            }

            var postEntity = _mapper.Map<PostEntity>(postRequest);
            PopulateUserInfo(postEntity);

            var titleAlreadyExists = await _postRepository.TitleExistsAsync(postRequest.Title);

            if(titleAlreadyExists)
            {
                _logger.LogError("PostService - PostAsync - Validation Error, Title already exists. Request: {Request}", postRequest);
                throw new BadRequestException("Title already exists");
            }

            var result = await _postRepository.AddAsync(postEntity);
            await _postRepository.SaveChangesAsync();
           
            var response = _mapper.Map<PostCompleteResponse>(result);
            _logger.LogInformation("PostService - PostAsync success. Response: {Response}", response);
            return response;
        }


        public async Task<PostCompleteResponse> PostCommentsAsync(Guid postId, CommentRequest commentRequest)
        {
            var validation = await _commentRequestValidator.ValidateAsync(commentRequest);

            if (!validation.IsValid)
            {
                _logger.LogError("PostService - PostCommentsAsync - Validation Error. ValidationError: {ValidationError}", validation.ToDictionary());
                throw new BadRequestException("Validation Error", validation.ToDictionary());
            }

            var post = await _postRepository.GetByIdAsync(postId);

            if (post == null)
            {
                _logger.LogError("PostService - PostCommentsAsync - Post Not Found Error. PostId: {PostId}", postId);
                throw new NotFoundException("Post not found");
            }

            var commentEntity = _mapper.Map<CommentEntity>(commentRequest);
            PopulateUserInfo(commentEntity);

            post.Comments.Add(commentEntity);

            var result = await _postRepository.UpdateAsync(post);
            await _postRepository.SaveChangesAsync();

            var response = _mapper.Map<PostCompleteResponse>(result);
            _logger.LogInformation("PostService - PostCommentsAsync success. Response: {Response}", response);
            return response;
        }

        /// <summary>
        /// Ideally this process would be done by accessing the Identity Users directly from the Infrastructure.Data project.
        /// As the authentication process developed is simple, it will remain here and retrieve the information from 
        /// the Claims defined by the Authenticated User.
        /// </summary>
        /// <returns></returns>
        private void PopulateUserInfo(BaseEntity baseEntity)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;
            baseEntity.SetCreatedBy(currentUser);

        }
    }
}
