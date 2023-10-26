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
    public class RentalController : ControllerBase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowerRepository _borrowerRepository;

        public RentalController(IRentalRepository rentalRepository, IMapper mapper, IBookRepository bookRepository,
            IBorrowerRepository borrowerRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _borrowerRepository = borrowerRepository;
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

        [HttpPost]
        public async Task<IActionResult> AddRentalAsync([FromBody] CreateRentalDto createdRental, int bookId, int borrowerId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _bookRepository.CheckIfBookExistAsync(bookId))
            {
                ModelState.AddModelError("", "Book does not exist ");
                return NotFound(ModelState);
            }

            if (!await _borrowerRepository.BorrowerExistAsync(borrowerId))
            {
                ModelState.AddModelError("", "Borrower does not exist");
                return NotFound(ModelState);
            }

            if (!await _bookRepository.CheckIfBookIsAvaibleAsync(bookId))
            {
                ModelState.AddModelError("", "Book is not avaible");
                return NotFound(ModelState);
            }

            var rental = _mapper.Map<Rental>(createdRental);
            rental.BookId = bookId;
            rental.BorrowerId = borrowerId;
            var addedRental = await _rentalRepository.AddRentalAsync(rental);

            return CreatedAtAction(nameof(GetRentalById), new { rentalId = _mapper.Map<RentalDto>(addedRental).Id }, addedRental);
        }

        [HttpPut("{rentalId}")]
        public async Task<ActionResult> UpdateRentalAsync([FromBody] UpdateRentalDto updatedRental, int rentalId,
            [FromQuery] int bookId, [FromQuery] int borrowerId)
        {
            if (updatedRental.Id != rentalId)
            {
                return BadRequest("Id missmatch");
            }

            if (!await _rentalRepository.CheckIfRentalExistAsync(rentalId))
            {
                return NotFound($"Rental with {rentalId} id not found");
            }

            if (!await _bookRepository.CheckIfBookExistAsync(bookId))
            {
                return NotFound($"Book with {bookId} id not found");
            }

            if (!await _borrowerRepository.BorrowerExistAsync(borrowerId))
            {
                return NotFound($"Borrower with {borrowerId} id not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rental = _mapper.Map<Rental>(updatedRental);
            rental.BorrowerId = borrowerId;
            rental.BookId = bookId;
            await _rentalRepository.UpdateRentalAsync(rental, bookId, rentalId);
            return NoContent();
        }

        [HttpDelete("{rentalId}")]
        public async Task<ActionResult> DeleteRentalAsync(int rentalId)
        {
            if (!await _rentalRepository.CheckIfRentalExistAsync(rentalId))
                return NotFound("Rental not found");

            await _rentalRepository.DeleteRentalAsync(rentalId);
            return NoContent();
        }
    }
}
