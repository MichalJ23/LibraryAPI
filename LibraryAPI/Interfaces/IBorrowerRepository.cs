using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IBorrowerRepository
    {
        //Get
        Task<IEnumerable<Borrower>> GetBorrowersAsync();
        Task<Borrower> GetBorrowerAsync(int id);
        Task<bool> BorrowerExistAsync(int id);

        //Post
        Task<Borrower> AddBorrowerAsync(Borrower borrower);
    }
}
