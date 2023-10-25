using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Book> GetBookByIdAsync(int id) => await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author)
        {
            var books = await _context.Books
                .Where(b => b.Author.ToLower().Trim() == author.ToLower().Trim())
                .ToListAsync();

            return books;
        }

        public async Task<Book> GetBookByTitleAsync(string title)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Title.ToLower().Trim() == title.ToLower().Trim());
        }

        public async Task<IEnumerable<Book>> GetBooksAsync() => await _context.Books.ToListAsync();

        public async Task<bool> CheckIfBookIsAvaibleAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null || book.AvailableCopies <= 0)
                return false;

            return true;
        }

        public async Task<bool> CheckIfBookExistAsync(int id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}
