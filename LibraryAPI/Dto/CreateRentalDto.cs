using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto
{
    public class CreateRentalDto
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; }
    }
}
