using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly AppDbContext _context;
        public BorrowerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Borrower> GetBorrowerAsync(int id)
        {
            var borrower = await _context.Borrowers.FirstOrDefaultAsync(b => b.Id == id);
            return borrower;
        }

        public async Task<IEnumerable<Borrower>> GetBorrowersAsync()
        {
            return await _context.Borrowers.ToListAsync();
        }
        public async Task<bool> BorrowerExistAsync(int id) => await _context.Borrowers.AnyAsync(b => b.Id == id);

        public async Task<Borrower> AddBorrowerAsync(Borrower borrower)
        {
            await _context.Borrowers.AddAsync(borrower);
            await _context.SaveChangesAsync();
            return borrower;
        }
    }
}
