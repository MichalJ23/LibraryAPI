using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IBorrowerRepository
    {
        Task<IEnumerable<Borrower>> GetBorrowers();
        Task<Borrower> GetBorrower(int id);
        Task<Borrower> AddBorrower(Borrower borrower);
        Task<Borrower> UpdateBorrower(Borrower borrower);
        Task<Borrower> DeleteBorrower(int id);
    }
}
