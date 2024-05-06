﻿using System.ComponentModel;
using BorrowedBookService.Models;

namespace BorrowedBookService.DTO
{
    public class UserBorrowedBookDTO
    {
        public required int RequestID { get; set; }
        public required int UserID { get; set; }
        public required int BookID { get; set; }
        public required string RequestStatus { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
       
}
