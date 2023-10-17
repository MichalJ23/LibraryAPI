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
    }
}
