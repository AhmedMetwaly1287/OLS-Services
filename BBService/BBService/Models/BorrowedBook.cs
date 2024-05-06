using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BorrowedBookService.Models
{
    public class BorrowedBook
    {
        [Key]
        [JsonIgnore]
        public required int RequestID { get; set; }

        public required int UserID { get; set; }

        public required int BookID { get; set; }

        [JsonIgnore]
        public DateTime BorrowedDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public DateTime? ReturnDate { get; set; }

        public required string RequestStatus { get; set; } = "Pending";

        [DefaultValue(false)]
        public required bool BookBorrowed { get; set; }
    }
}
