using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}
