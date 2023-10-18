using AutoMapper;
using LibraryAPI.Dto;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IMapper _mapper;
        public ContactInfoController(IContactInfoRepository contactInfoRepository, IMapper mapper)
        {
            _contactInfoRepository = contactInfoRepository;
            _mapper = mapper;
        }

        [HttpGet("{borrowerId}")]
        public async Task<IActionResult> GetContactInfoByBorrowerId(int borrowerId)
        {
            var contactInfo = await _contactInfoRepository.GetContactInfoByBorrowerId(borrowerId);
            if (contactInfo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ContactInfoDto>(contactInfo));
        }
    }
}
