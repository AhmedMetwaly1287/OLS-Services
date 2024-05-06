namespace ArchiveService.DTO
{
    public class AddArchiveDTO
    {
        public required int BorrowedBookRequestID { get; set; }

        public required int UserID { get; set; } //FK from BB

        public required int BookID { get; set; } //FK from BB

        public required DateTime ReturnDate { get; set; } //From BB
    }
}
