using Microsoft.EntityFrameworkCore;
using ArchiveService.Data;
using ArchiveService.Models;
using System.Security.Claims;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ArchiveService.Repository
{
    public class ArchiveRepository : IArchiveRepository
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _configuration;
        public ArchiveRepository(AppDbContext context, IConfiguration configuration)
        {
            _db = context;
            _configuration = configuration;

        }
        public async Task AddToArchive(Archive archive)
        {
            _db.Archives.Add(archive);
            await _db.SaveChangesAsync();
        }


        public async Task<List<Archive>> GetAll()
        {
            return await _db.Archives
                        .ToListAsync();
        }
        public async Task<List<Archive>> GetArchiveByID(int UserID)
        {
            return await _db.Archives
                    .Where(a=>a.UserID == UserID)
                    .ToListAsync();
        }
    }
}
