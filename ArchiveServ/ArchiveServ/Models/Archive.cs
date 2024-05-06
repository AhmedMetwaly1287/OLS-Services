using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ArchiveService.Models
{
    public class Archive
    {
        [Key]
        [JsonIgnore]
        public int ArchiveID { get; set; }

        public required int BorrowedBookRequestID {  get; set; }

        public required int UserID { get; set; } //FK from BB

        public required int BookID { get; set; } //FK from BB

        public required DateTime ReturnDate { get; set; } //From BB

    }
}
