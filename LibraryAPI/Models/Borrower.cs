using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("ContactInfo")]
        public int ContactInfoId { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
