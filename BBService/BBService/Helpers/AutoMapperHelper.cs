using AutoMapper;
using BorrowedBookService.DTO;
using BorrowedBookService.Models;

namespace BorrowedBookService.Helpers
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<AdminBorrowedBookDTO, BorrowedBook>().ReverseMap();
            CreateMap<UserBorrowedBookDTO, BorrowedBook>().ReverseMap();
            CreateMap<RequestDTO, BorrowedBook>().ReverseMap();

        }
    }
}
