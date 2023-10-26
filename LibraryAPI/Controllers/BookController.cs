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
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooksAsync()
        {
            var books = await _bookRepository.GetBooksAsync();

            if (books.IsNullOrEmpty())
                return NotFound();

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

            return Ok(booksDto);
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookByIdAsync(int bookId)
        {
            if (bookId <= 0)
                return BadRequest("Invalid book ID");

            var book = await _bookRepository.GetBookByIdAsync(bookId);

            if (book == null)
                return NotFound();

            var bookDto = _mapper.Map<BookDto>(book);

            return Ok(bookDto);
        }

        [HttpGet("author/{author}")]
        public async Task<IActionResult> GetBookByAuthorAsync(string author)
        {
            if (author.IsNullOrEmpty())
                return BadRequest("Invalid author name");

            var books = await _bookRepository.GetBooksByAuthorAsync(author);

            if (books.IsNullOrEmpty())
                return NotFound();
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(booksDto);
        }

        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetBookByTitleAsync(string title)
        {
            if (title.IsNullOrEmpty())
                return BadRequest("Invalid title");

            var book = await _bookRepository.GetBookByTitleAsync(title);

            if (book == null)
                return NotFound();

            var bookDto = _mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }

        [HttpGet("available/{bookId}")]
        public async Task<IActionResult> CheckBookAvailabilityAsync(int bookId)
        {
            var isAvailable = await _bookRepository.CheckIfBookIsAvaibleAsync(bookId);

            if (!isAvailable)
                return BadRequest("Book is not available");

            return Ok("Book is available");
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> AddBookAsync([FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state");

            var book = _mapper.Map<Book>(bookDto);

            var createdBook = await _bookRepository.AddBookAsync(book);

            var createdBookDto = _mapper.Map<BookDto>(createdBook);

            return CreatedAtAction(nameof(GetBookByIdAsync), new { bookId = createdBookDto.Id }, createdBookDto);
        }

        [HttpPut("{bookId}")]
        public async Task<ActionResult<BookDto>> UpdateBookAsync([FromBody] BookDto updatedBook, int bookId)
        {
            if (updatedBook.Id != bookId)
                return BadRequest("Book with Id mismatch");

            if (!ModelState.IsValid)
                return BadRequest("Invalid model state");

            if (!await _bookRepository.CheckIfBookExistAsync(bookId))
                return NotFound($"Book with {bookId} Id not found");

            var book = _mapper.Map<Book>(updatedBook);

            await _bookRepository.UpdateBookAsync(book);

            return NoContent();
        }

        [HttpDelete("{bookId}")]
        public async Task<ActionResult> DeleteBookAsync(int bookId)
        {
            if (!await _bookRepository.CheckIfBookExistAsync(bookId))
                return NotFound("Book not found");

            await _bookRepository.DeleteBookAsync(bookId);
            return NoContent();
        }
    }
}
