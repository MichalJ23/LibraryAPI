using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IBookRepository
    {
        //GET
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> GetBookByTitleAsync(string title);
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author);
        Task<bool> CheckIfBookIsAvaibleAsync(int id);
        Task<bool> CheckIfBookExistAsync(int id);

        //Post
        Task<Book> AddBookAsync(Book book);
    }
}
