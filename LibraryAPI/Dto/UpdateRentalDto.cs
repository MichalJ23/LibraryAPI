using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto
{
    public class UpdateRentalDto
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public bool Returned { get; set; }
    }
}
