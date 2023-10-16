namespace LibraryAPI.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int ContactInfoId { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
