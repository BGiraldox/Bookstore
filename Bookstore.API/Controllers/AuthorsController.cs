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
    public class AuthorsController : ControllerBase
    {
        private readonly IBaseService<Author> _service;
        private readonly IValidator<QueryFilter<Author>> _filterValidator;

        public AuthorsController(IBaseService<Author> service, IValidator<QueryFilter<Author>> filterValidator)
        {
            _service = service;
            _filterValidator = filterValidator;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public async Task<IEnumerable<Author>> GetAll() => await _service.GetAll().ConfigureAwait(false);

        // Post: api/<AuthorsController>/filter
        [HttpPost("filter")]
        public async Task<IActionResult> GetAllByFilter([FromBody] AuthorsQueryFilter filterFields)
        {
            var validationResult = await _filterValidator.ValidateAsync(filterFields);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { Field = e.PropertyName, Error = e.ErrorMessage }));  
            }
            
            return Ok(await _service.GetAllByFilter(filterFields).ConfigureAwait(false));
        }
            

        // GET api/<AuthorsController>/test/5
        [HttpGet("{pk}/{id}")]
        public async Task<IActionResult> Get(string id, string pk)
        {
            var result = await _service.GetById(id, pk).ConfigureAwait(false);
            return result is null ? NotFound() : Ok(result);
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author author) => Ok(await _service.Add(author).ConfigureAwait(false));

        // PUT api/<AuthorsController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Author author)
        {
            var result = await _service.Update(author).ConfigureAwait(false);
            return result is null ? NotFound() : Ok(result);
        }

        // DELETE api/<AuthorsController>/test/5
        [HttpDelete("{pk}/{id}")]
        public async void Delete(string id, string pk) => await _service.Delete(id, pk).ConfigureAwait(false);
    }
}
