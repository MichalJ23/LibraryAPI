using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public bool Returned { get; set; }

        [ForeignKey("Borrower")]
        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }

        public Rental()
        {
            SetDueDate();
        }
        private void SetDueDate()
        {
            DueDate = RentalDate.Date.AddDays(30);
        }

        //TU się zatrzymałem
    }
}
