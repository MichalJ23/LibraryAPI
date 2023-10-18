using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
    }
}
