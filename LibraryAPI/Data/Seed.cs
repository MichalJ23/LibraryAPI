using LibraryAPI.Models;

namespace LibraryAPI.Data
{
    public class Seed
    {
        private readonly AppDbContext _context;
        public Seed(AppDbContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if (_context.Rentals.Any() || _context.ContactInfos.Any() || _context.Borrowers.Any() || _context.Books.Any())
            {
                return;
            }

            var contactInfos = new List<ContactInfo>
            {
                new ContactInfo { Email = "john.doe@email.com", Phone = "123-456-7890" },
                new ContactInfo { Email = "jane.smith@email.com", Phone = "987-654-3210" },
                new ContactInfo { Email = "james.brown@email.com", Phone = "555-123-4567" },
                new ContactInfo { Email = "susan.johnson@email.com", Phone = "777-987-6543" }
            };

            var borrowers = new List<Borrower>
            {
                new Borrower { FirstName = "John", LastName = "Doe", ContactInfo = contactInfos[0] },
                new Borrower { FirstName = "Jane", LastName = "Smith", ContactInfo = contactInfos[1] },
                new Borrower { FirstName = "James", LastName = "Brown", ContactInfo = contactInfos[2] },
                new Borrower { FirstName = "Susan", LastName = "Johnson", ContactInfo = contactInfos[3] }
            };

            var books = new List<Book>
            {
                new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", PublishDate = new DateTime(1925, 4, 10), AvailableCopies = 5, TotalCopies = 10 },
                new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", PublishDate = new DateTime(1960, 7, 11), AvailableCopies = 3, TotalCopies = 8 },
                new Book { Title = "Pride and Prejudice", Author = "Jane Austen", PublishDate = new DateTime(1813, 1, 28), AvailableCopies = 4, TotalCopies = 6 },
                new Book { Title = "1984", Author = "George Orwell", PublishDate = new DateTime(1949, 6, 8), AvailableCopies = 2, TotalCopies = 5 },
            };

            var rentals = new List<Rental>
            {
                new Rental { RentalDate = new DateTime(2023, 10, 1), Returned = false, Borrower = borrowers[0], Book = books[0] },
                new Rental { RentalDate = new DateTime(2023, 10, 15), Returned = false, Borrower = borrowers[1], Book = books[1] },
                new Rental { RentalDate = new DateTime(2023, 10, 5), Returned = false, Borrower = borrowers[2], Book = books[2] },
                new Rental { RentalDate = new DateTime(2023, 10, 20), Returned = false, Borrower = borrowers[3], Book = books[3] },
            };

            _context.AddRange(rentals);
            _context.SaveChanges();
        }
    }
}
