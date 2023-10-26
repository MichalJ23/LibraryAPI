using AutoMapper;
using LibraryAPI.Dto;
using LibraryAPI.Models;

namespace LibraryAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Borrower, BorrowerDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();
            CreateMap<Rental, RentalDto>().ReverseMap();
            CreateMap<CreateRentalDto, Rental>().ReverseMap();
            CreateMap<UpdateRentalDto, Rental>().ReverseMap();
        }
    }
}
