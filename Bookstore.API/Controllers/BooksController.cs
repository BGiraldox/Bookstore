using Bookstore.Core.Entities;
using Bookstore.Core.Interfaces.Common;
using Bookstore.Core.Utility.QueryHandler;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBaseService<Book> _service;
        private readonly IValidator<QueryFilter<Book>> _filterValidator;

        public BooksController(IBaseService<Book> service, IValidator<QueryFilter<Book>> filterValidator)
        {
            _service = service;
            _filterValidator = filterValidator;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Book>> GetAll() => await _service.GetAll().ConfigureAwait(false);

        // POST: api/<ValuesController>/filter
        [HttpPost("/filter")]
        public async Task<IActionResult> GetAllByFilter([FromBody] BooksQueryFilter filterFields)
        {
            var validationResult = await _filterValidator.ValidateAsync(filterFields);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { Field = e.PropertyName, Error = e.ErrorMessage }));
            }

            return Ok(await _service.GetAllByFilter(filterFields).ConfigureAwait(false));
        }

        // GET api/<ValuesController>/test/5
        [HttpGet("{pk}/{id}")]
        public async Task<IActionResult> Get(string id, string pk)
        {
            var result = await _service.GetById(id, pk).ConfigureAwait(false);
            return result is null ? NotFound() : Ok(result);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book) => Ok(await _service.Add(book).ConfigureAwait(false));

        // PUT api/<ValuesController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Book book)
        {
            var result = await _service.Update(book).ConfigureAwait(false);
            return result is null ? NotFound() : Ok(result);
        }

        // DELETE api/<ValuesController>/test/5
        [HttpDelete("{pk}/{id}")]
        public async void Delete(string id, string pk) => await _service.Delete(id, pk).ConfigureAwait(false);
    }
}
