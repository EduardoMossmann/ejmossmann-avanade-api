using AutoMapper;
using AvanadeBlog.Application.Models.Post;
using AvanadeBlog.Domain.Entities;

namespace AvanadeBlog.Application.AutoMapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostRequest, PostEntity>();
            CreateMap<PostEntity, PostResponse>()
                .ForMember(x => x.NumberOfComments, x => x.MapFrom(v => v.Comments.Count()));
            CreateMap<PostEntity, PostCompleteResponse>();
        }
    }
}
