using ArchiveService.Models;
using ArchiveService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ArchiveService.Services
{
    public interface IArchiveService
    {
        Task AddToArchive(AddArchiveDTO Archive);
        Task<List<AdminArchiveDTO>> GetAll();
        Task<List<UserArchiveDTO>> GetArchiveByID(int UserID);
    }
}
