using Bookstore.Core.Entities;
using Bookstore.Core.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IBaseService<Author> _service;

        public AuthorsController(IBaseService<Author> service)
        {
            _service = service;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _service.GetAll().ConfigureAwait(false);
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author author) => Ok(await _service.Add(author).ConfigureAwait(false));

        // PUT api/<AuthorsController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Author author)
        {
            var result = await _service.Update(author).ConfigureAwait(false);
            return result is null ? NotFound() : Ok(result);
        }

        // DELETE api/<AuthorsController>/5/test
        [HttpDelete("{id}/{pk}")]
        public async void Delete(string id, string pk) =>  await _service.Delete(id, pk).ConfigureAwait(false);
    }
}
