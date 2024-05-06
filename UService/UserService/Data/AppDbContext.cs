using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options){
        public DbSet<User> Users { get; set; }
    }
}