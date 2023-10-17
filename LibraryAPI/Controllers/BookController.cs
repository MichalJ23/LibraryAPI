using AutoMapper;
using LibraryAPI.Dto;
using LibraryAPI.Interfaces;
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
    }
}
