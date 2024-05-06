using AutoMapper;
using ArchiveService.DTO;
using ArchiveService.Models;

namespace ArchiveService.Helpers
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<AdminArchiveDTO, Archive>().ReverseMap();
            CreateMap<UserArchiveDTO, Archive>().ReverseMap();
            CreateMap<AddArchiveDTO, Archive>().ReverseMap();
        }
    }
}
