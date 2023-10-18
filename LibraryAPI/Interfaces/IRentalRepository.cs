using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IRentalRepository
    {
        //Get
        Task<IEnumerable<Rental>> GetRentalsAsync();
        Task<Rental> GetRentalAsync(int id);
        Task<IEnumerable<Rental>> GetBorrowerRentalsAsync(int borrowerId);
        Task<IEnumerable<Rental>> GetBookRentalsAsync(int bookId);
    }
}
