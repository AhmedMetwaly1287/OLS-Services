using BorrowedBookService.Models;

namespace BorrowedBookService.DTO
{
    public class RequestDTO
    {
        public required int UserID { get; set; }
        public required int BookID { get; set; }

    }
}
