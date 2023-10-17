using AutoMapper;
using LibraryAPI.Dto;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IMapper _mapper;
        public BorrowerController(IBorrowerRepository borrowerRepository, IMapper mapper)
        {
            _borrowerRepository = borrowerRepository;
            _mapper = mapper;
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

            if (borrowers == null)
            {
                return NotFound("Borrowers not found");
            }

            var borrowersDto = _mapper.Map<IEnumerable<BorrowerDto>>(borrowers);

            return Ok(borrowersDto);
        }
    }
}
