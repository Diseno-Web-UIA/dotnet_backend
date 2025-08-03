using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(lastprojectContext context) : ControllerBase
    {
        private readonly lastprojectContext _context = context;

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<Persona>> Get()
        {
            var clients = _context.Persona.ToList();
            return clients;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public Persona Get(int id)
        {
            try
            {
                var client = _context.Persona.FirstOrDefault(c => c.idPersona == id);
                return client;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return null; // or handle it as appropriate
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
