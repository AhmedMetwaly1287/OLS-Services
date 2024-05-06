using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using ArchiveService.Models;

namespace ArchiveService.Data{
    public class AppDbContext : DbContext{

        public DbSet<Archive> Archives { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
      


    }
}