using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Repository
{
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly AppDbContext _context;
        public BorrowerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Borrower> AddBorrower(Borrower borrower)
        {
            await _context.Borrowers.AddAsync(borrower);
            return borrower;
        }

        public Task<Borrower> DeleteBorrower(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Borrower> GetBorrower(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Borrower>> GetBorrowers()
        {
            throw new NotImplementedException();
        }

        public Task<Borrower> UpdateBorrower(Borrower borrower)
        {
            throw new NotImplementedException();
        }
    }
}
