using BorrowedBookService.Models;
using BorrowedBookService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BorrowedBookService.Services
{
    public interface IBorrowedBookService
    {
        Task AddRequest([FromBody] RequestDTO RequestDTO);
        Task<List<AdminBorrowedBookDTO>> GetAll();
        Task<List<UserBorrowedBookDTO>> GetBorrowedBooksByID(int UserID);
        Task<bool> RequestExists(int RequestID);
        Task<AdminBorrowedBookDTO> GetRequest(int RequestID);
        Task ApproveRequest(int RequestID);
        Task RejectRequest(int RequestID);
        Task ReturnBook(int RequestID);
        Task DeleteRequest(int RequestID);
        Task<int> GetNumberOfRequests(int UserID);
    }
}
