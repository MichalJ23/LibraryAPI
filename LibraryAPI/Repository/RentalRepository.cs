using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class RentalRepository : IRentalRepository
    {
        private readonly AppDbContext _context;
        public RentalRepository(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Rental>> GetBookRentalsAsync(int bookId)
        {
            var rentals = await _context.Rentals
                .Where(r => r.BookId == bookId)
                .ToListAsync();

            return rentals;
        }

        public async Task<IEnumerable<Rental>> GetBorrowerRentalsAsync(int borrowerId)
        {
            var rentals = await _context.Rentals
                .Where(r => r.BorrowerId == borrowerId)
                .ToListAsync();
            return rentals;
        }

        public async Task<Rental> GetRentalAsync(int id)
        {
            return await _context.Rentals.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Rental>> GetRentalsAsync()
        {
            return await _context.Rentals.ToListAsync();
        }

        public async Task<Rental> AddRentalAsync(Rental rental)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == rental.BookId);
            book.AvailableCopies--;
            var rentalDate = rental.RentalDate;
            rental.DueDate = rentalDate.AddDays(30);
            var result = await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
