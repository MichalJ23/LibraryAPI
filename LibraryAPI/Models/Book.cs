using System.ComponentModel;

namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateOnly PublishDate { get; set; }
        public string AvailableCopies { get; set; }
        public string TotalCopies { get; set; }
    }
}
