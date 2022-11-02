using Bookstore.Core.Entities;
using Bookstore.Core.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBaseService<Book> _service;

        public BooksController(IBaseService<Book> service)
        {
            _service = service;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Book>> GetAll() => await _service.GetAll().ConfigureAwait(false);

        // GET api/<ValuesController>/5
        [HttpGet("{pk}/{id}")]
        public async Task<IActionResult> Get(string id, string pk)
        {
            var result = await _service.GetById(id, pk).ConfigureAwait(false);
            return result is null ? NotFound() : Ok(result);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book) => Ok(await _service.Add(book).ConfigureAwait(false));

        // PUT api/<ValuesController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Book book)
        {
            var result = await _service.Update(book).ConfigureAwait(false);
            return result is null ? NotFound() : Ok(result);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{pk}/{id}")]
        public async void Delete(string id, string pk) => await _service.Delete(id, pk).ConfigureAwait(false);
    }
}
