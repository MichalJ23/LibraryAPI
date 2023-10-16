namespace LibraryAPI.Models
{
    public class Rentals
    {
        public int Id { get; set; }
        public DateOnly DueDate { get; set; }
        public bool Returned { get; set; }

        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
