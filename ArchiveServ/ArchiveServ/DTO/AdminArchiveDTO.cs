namespace ArchiveService.DTO
{
    public class AdminArchiveDTO
    {
        public required int ArchiveID {  get; set; }
        public required int BorrowedBookRequestID { get; set; }
        public int UserID { get; set; }
        public  int BookID { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
