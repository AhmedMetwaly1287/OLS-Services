using System.Drawing.Text;
using ArchiveService.Repository;
using ArchiveService.Helpers;
using ArchiveService.DTO;
using AutoMapper;
using ArchiveService.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ArchiveService.Services
{
    public class ArchiveServices : IArchiveService
    {
        private readonly IArchiveRepository _aRepo;
        private readonly IMapper _mapper;
        public ArchiveServices(IArchiveRepository aRepo, IMapper mapper)
        {
            _aRepo = aRepo;
            _mapper = mapper;
        }
        public async Task AddToArchive(AddArchiveDTO ArchiveDTO)
        {
            var Archive = _mapper.Map<Archive>(ArchiveDTO);
            await _aRepo.AddToArchive(Archive); 
        }

        public async Task<List<AdminArchiveDTO>> GetAll()
        {
            var Archives = await _aRepo.GetAll();
            var ArchivesDTO = Archives.Select(Archive => new AdminArchiveDTO
            {
                ArchiveID = Archive.ArchiveID,
                BorrowedBookRequestID = Archive.BorrowedBookRequestID,
                UserID = Archive.UserID,
                BookID = Archive.BookID,
                ReturnDate = Archive.ReturnDate,
            }).ToList();
            return ArchivesDTO;
        }
        public async Task<List<UserArchiveDTO>> GetArchiveByID(int UserID)
        {
            var Archives = await _aRepo.GetArchiveByID(UserID);
            var ArchivesDTO = Archives.Select(Archive => new UserArchiveDTO
            {
                ArchiveID = Archive.ArchiveID,
                BorrowedBookRequestID = Archive.BorrowedBookRequestID,
                BookID = Archive.BookID,
                ReturnDate = Archive.ReturnDate,
            }).ToList();
            return ArchivesDTO; ;
        }


    }
}

