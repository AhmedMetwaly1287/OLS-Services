using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using BorrowedBookService.Models;

namespace BorrowedBookService.Data{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options){
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    }
}