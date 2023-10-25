using AutoMapper;
using LibraryAPI.Dto;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet]
        public async Task<IActionResult> GetContactInfosAsync()
        {
            var contactInfos = await _contactInfoRepository.GetContactInfosAsync();
            if (contactInfos.IsNullOrEmpty())
            {
                return NotFound("ContactInfos not found");
            }
            return Ok(_mapper.Map<IEnumerable<ContactInfoDto>>(contactInfos));
        }

        [HttpPost]
        public async Task<ActionResult<ContactInfoDto>> AddContactInfoAsync([FromBody] ContactInfoDto createdContactInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contactInfo = _mapper.Map<ContactInfo>(createdContactInfo);
            var newContactInfo = await _contactInfoRepository.AddContactInfoAsync(contactInfo);
            var contactInfoDto = _mapper.Map<ContactInfoDto>(newContactInfo);

            return CreatedAtAction(nameof(GetContactInfoByBorrowerId), contactInfoDto.Id, contactInfoDto);
        }

        [HttpPut("{infoId}")]
        public async Task<ActionResult<ContactInfoDto>> UpdateContactInfoAsync([FromBody] ContactInfoDto contactInfoDto, int infoId)
        {
            if (contactInfoDto.Id != infoId)
            {
                return BadRequest("Id missmatch");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _contactInfoRepository.ContactInfoExistAsync(infoId))
            {
                return NotFound($"ContactInfo with {infoId} id not found");
            }

            var contactInfo = _mapper.Map<ContactInfo>(contactInfoDto);
            await _contactInfoRepository.UpdateContactInfoAsync(contactInfo);
            return NoContent();
        }
    }
}
