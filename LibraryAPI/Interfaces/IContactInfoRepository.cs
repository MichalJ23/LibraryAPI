using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IContactInfoRepository
    {
        //Get
        Task<ContactInfo> GetContactInfoByBorrowerId(int borrowerId);
        Task<IEnumerable<ContactInfo>> GetContactInfosAsync();
        Task<bool> ContactInfoExistAsync(int id);

        //Post 
        Task<ContactInfo> AddContactInfoAsync(ContactInfo contactInfo);

    }
}
