using AutoMapper;
using AvanadeBlog.Application.Models.Comment;
using AvanadeBlog.Domain.Entities;

namespace AvanadeBlog.Application.AutoMapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile() 
        {
            CreateMap<CommentRequest, CommentEntity>();
            CreateMap<CommentEntity, CommentResponse>();
        }
    }
}
