using Microsoft.AspNetCore.Mvc;
using ArchiveService.Models;

namespace ArchiveService.Repository
{
    public interface IArchiveRepository
    {
        Task AddToArchive(Archive archive);
        Task<List<Archive>> GetAll();
        Task<List<Archive>> GetArchiveByID(int UserID);

    }
}
