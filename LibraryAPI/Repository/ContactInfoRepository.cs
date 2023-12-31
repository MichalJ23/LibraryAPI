﻿using LibraryAPI.Data;
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

        public async Task<IEnumerable<ContactInfo>> GetContactInfosAsync()
        {
            return await _context.ContactInfos.ToListAsync();
        }

        public async Task<ContactInfo> AddContactInfoAsync(ContactInfo contactInfo)
        {
            var result = await _context.ContactInfos.AddAsync(contactInfo);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> ContactInfoExistAsync(int id)
        {
            return await _context.ContactInfos.AnyAsync(c => c.Id == id);
        }

        public async Task<ContactInfo> UpdateContactInfoAsync(ContactInfo contactInfo)
        {
            var contactInfoEntity = await _context.ContactInfos.FirstOrDefaultAsync(c => c.Id == contactInfo.Id);

            if (contactInfoEntity == null)
                return null;

            contactInfoEntity.Email = contactInfo.Email;
            contactInfoEntity.Phone = contactInfo.Phone;

            var result = _context.ContactInfos.Update(contactInfoEntity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
