using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        public string AvailableCopies { get; set; }
        public string TotalCopies { get; set; }
    }
}
