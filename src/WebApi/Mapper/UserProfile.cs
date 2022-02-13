using AutoMapper;
using WebApi.Data.Domains;
using WebApi.Models;

namespace WebApi.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}