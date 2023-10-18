using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class ContactInfoRepository : IContactInfoRepository
    {
        private readonly AppDbContext _context;
        public ContactInfoRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<ContactInfo> GetContactInfoByBorrowerId(int borrowerId)
        {
            var contactInfo = await _context.Borrowers
                .Where(b => b.Id == borrowerId)
                .Select(b => b.ContactInfo)
                .FirstOrDefaultAsync();
            return contactInfo;
        }
    }
}
