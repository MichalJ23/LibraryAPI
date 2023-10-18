using AutoMapper;
using LibraryAPI.Dto;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;
        public RentalController(IRentalRepository rentalRepository, IMapper mapper)
        {
            _mapper = mapper;
            _rentalRepository = rentalRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetRentals()
        {
            var rentals = await _rentalRepository.GetRentalsAsync();

            if (rentals.IsNullOrEmpty())
                return NotFound();

            var rentalsDto = _mapper.Map<IEnumerable<RentalDto>>(rentals);
            return Ok(rentalsDto);
        }

        [HttpGet("borrower/{borrowerId}")]
        public async Task<IActionResult> GetBorrowerRentals(int borrowerId)
        {
            if (borrowerId < 1)
                return BadRequest();

            var rentals = await _rentalRepository.GetBorrowerRentalsAsync(borrowerId);

            if (rentals.IsNullOrEmpty())
                return NotFound();

            var rentalsDto = _mapper.Map<IEnumerable<RentalDto>>(rentals);
            return Ok(rentalsDto);
        }

        [HttpGet("{rentalId}")]
        public async Task<IActionResult> GetRentalById(int rentalId)
        {
            if (rentalId < 1)
                return BadRequest();

            var rental = await _rentalRepository.GetRentalAsync(rentalId);

            if (rental == null)
                return NotFound();

            var rentalDto = _mapper.Map<RentalDto>(rental);
            return Ok(rentalDto);
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetBookRentals(int bookId)
        {
            if (bookId < 1)
                return BadRequest();

            var rentals = await _rentalRepository.GetBookRentalsAsync(bookId);

            if (rentals.IsNullOrEmpty())
                return NotFound();

            var rentalsDto = _mapper.Map<IEnumerable<RentalDto>>(rentals);
            return Ok(rentalsDto);
        }
    }
}
