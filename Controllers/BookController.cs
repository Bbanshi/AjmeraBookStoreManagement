using AjmeraBookStoreManagement.ApiRequestModels;
using AjmeraBookStoreManagement.ApiResponseModels;
using AutoMapper;
using BookStoreManagement.BusinessLayer;
using BookStoreManagement.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraBookStoreManagement.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;
        private IMapper Mapper;

        public BookController(ILogger<BookController> logger, IBookService bookService, IMapper mapper)
        {
            _logger = logger;
            _bookService = bookService;
            Mapper = mapper;
             
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var response = Mapper.Map<List<BookAuthorResponse>>(_bookService.GetAllBookDetails());
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception in BookController --- GetAll(). Exception Message: {ex.Message}, Stacktrace: {ex.StackTrace}");
                return StatusCode(500);
            }
       
        }


        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            try
            {
                var response = Mapper.Map<BookAuthorResponse>(_bookService.GetBookDetailsById(id));
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Book doesn't exist corresponding to the requested id.");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception in BookController --- GetById(). Exception Message: {ex.Message}, Stacktrace: {ex.StackTrace}");
                return StatusCode(500);
            }
            
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] AddBookRequest addBookRequest)
        {
            try
            {
                if (_bookService.AddBook(Mapper.Map<AddBookModel>(addBookRequest)))
                {
                    return Ok(addBookRequest);
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception in BookController --- AddBook(). Exception Message: {ex.Message}, Stacktrace: {ex.StackTrace}");
                return StatusCode(500);
            }
           
        }
    }
}
