using AutoMapper;
using UserService.DTO;
using UserService.Models;

namespace UserService.Helpers
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<RegisterDTO, User>().ReverseMap(); 
            CreateMap<LoginDTO, User>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
