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
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IMapper _mapper;
        private readonly IContactInfoRepository _contactInfoRepository;
        public BorrowerController(IBorrowerRepository borrowerRepository, IMapper mapper, IContactInfoRepository contactInfoRepository)
        {
            _borrowerRepository = borrowerRepository;
            _mapper = mapper;
            _contactInfoRepository = contactInfoRepository;
        }

        [HttpGet("{borrowerId}")]
        public async Task<IActionResult> GetBorrowerByIdAsync(int borrowerId)
        {
            if (borrowerId <= 0)
            {
                return BadRequest("Invalid borrower ID");
            }

            var borrower = await _borrowerRepository.GetBorrowerAsync(borrowerId);

            if (borrower == null)
            {
                return NotFound();
            }

            var borrowerDto = _mapper.Map<BorrowerDto>(borrower);

            return Ok(borrowerDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetBorrowersAsync()
        {
            var borrowers = await _borrowerRepository.GetBorrowersAsync();

            if (borrowers.IsNullOrEmpty())
            {
                return NotFound("Borrowers not found");
            }

            var borrowersDto = _mapper.Map<IEnumerable<BorrowerDto>>(borrowers);

            return Ok(borrowersDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddBorrowerAsync([FromBody] BorrowerDto createdBorrower, [FromQuery] int contactInfoId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _contactInfoRepository.ContactInfoExistAsync(contactInfoId))
            {
                return NotFound("ContactInfo not found");
            }

            var borrower = _mapper.Map<Borrower>(createdBorrower);

            borrower.ContactInfoId = contactInfoId;

            await _borrowerRepository.AddBorrowerAsync(borrower);

            return Ok(borrower);
        }

        [HttpPut("{borrowerId}")]
        public async Task<IActionResult> UpdateBorrowerAsync(int borrowerId, [FromBody] BorrowerDto updatedBorrower)
        {
            if (borrowerId != updatedBorrower.Id)
                return BadRequest("Borrower ID mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _borrowerRepository.BorrowerExistAsync(borrowerId))
            {
                return NotFound($"Borrower with {borrowerId} not found");
            }

            var borrower = _mapper.Map<Borrower>(updatedBorrower);

            await _borrowerRepository.UpdateBorrowerAsync(borrower);

            return NoContent();
        }

        [HttpDelete("{borrowerId}")]
        public async Task<ActionResult> DeleteBorrowerAsync(int borrowerId)
        {
            if (!await _borrowerRepository.BorrowerExistAsync(borrowerId))
                return NotFound("Borrower not found");

            await _borrowerRepository.DeleteBorrowerAsync(borrowerId);
            return NoContent();
        }
    }
}
