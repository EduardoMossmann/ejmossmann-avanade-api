using AutoMapper;
using AvanadeBlog.Domain;

namespace AvanadeBlog.Application.AutoMapper
{
    public class BaseProfile : Profile
    {
        public BaseProfile()
        {
            CreateMap(typeof(PaginatedResult<>), typeof(PaginatedResult<>));
        }
    }
}
