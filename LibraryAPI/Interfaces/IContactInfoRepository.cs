using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IContactInfoRepository
    {
        //Get
        Task<ContactInfo> GetContactInfoByBorrowerId(int borrowerId);

    }
}
