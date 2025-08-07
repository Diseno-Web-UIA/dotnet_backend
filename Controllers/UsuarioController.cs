using Azure;
using backend.Models;
using backend.Utils;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(lastprojectContext context) : ControllerBase
    {
        private readonly lastprojectContext _context = context;

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTO.Usuario.GET>>> List(int idPersona)
        {
            return Ok(await _context.Usuario
                .Where(u => u.Persona_idPersona == idPersona)
                .Select(u => new DTO.Usuario.GET
                {
                    idUsuario = u.idUsuario,
                    estado = u.estado,
                    Clave = u.Clave,
                    Persona_idPersona = u.Persona_idPersona
                })
                .ToArrayAsync());
        }

        // GET api/<UsersController>/5
        [HttpGet("{userName}")]
        public async Task<ActionResult<DTO.Usuario.GET>> Get(String userName)
        {
            var user = await _context.Usuario.FirstOrDefaultAsync(u => u.idUsuario.Equals(userName));
            return user != null ? Ok(new DTO.Usuario.GET
            {
                idUsuario = user.idUsuario,
                estado = user.estado,
                Clave = user.Clave,
                Persona_idPersona = user.Persona_idPersona
            }) : NotFound(new Error("No se encuentra el usuario", 404));
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<DTO.Usuario.GET>> Post([FromBody] DTO.Usuario.POST dto)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var user = new Usuario
            {
                idUsuario = dto.idUsuario,
                estado = dto.estado,
                Clave = dto.Clave,
                Persona_idPersona = dto.Persona_idPersona

            };
            try
            {
                await _context.Usuario.AddAsync(user);
                return CreatedAtAction(nameof(Get), new { userName = user.idUsuario }, user);
            }
            catch(DbUpdateException ex)
            {
                return Conflict(new Error(ex.Message, 409));
            }
            catch(Exception ex)
            {
                return StatusCode(500, new Error("Error Inesperado", 500, ex.StackTrace ?? ""));
            }

        }

         // PUT api/<UsersController>/5
        [HttpPut("{userName}")]
        public async Task<ActionResult<DTO.Usuario.GET>> Put(String userName, [FromBody] DTO.Usuario.PUT dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _context.Usuario.FindAsync(userName);
            if (user == null)
            {
                return NotFound(new
                {
                    message = "Usuario No encontrado",
                    status = 404
                });
            }

            user.estado = dto.estado;
            user.Clave = dto.Clave;
            user.Persona_idPersona = dto.Persona_idPersona;
            _context.Usuario.Update(user);


            try
            {
                await _context.SaveChangesAsync();
                return Ok(new DTO.Usuario.GET
                {
                    idUsuario = user.idUsuario,
                    estado = user.estado,
                    Clave = user.Clave,
                    Persona_idPersona = user.Persona_idPersona
                });
            }
            catch (DbUpdateException ex)
            {
                return Conflict(new Error(ex.Message, 409));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Error("Error Inesperado", 500, ex.StackTrace ?? ""));
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{userName}")]
        public async Task<IActionResult> Delete(string userName)
        {
            await _context.Usuario.Where(u => u.idUsuario.Equals(userName)).ExecuteDeleteAsync();
            return Ok(new
            {
                status = "success",
                message = $"Usuario {userName} eliminado"
            });
        }

        // PATCH api/<UsersController>/5
        [HttpPatch("{userName}")]
        public async Task<ActionResult<DTO.Usuario.GET>> Patch(string userName, [FromBody] DTO.Usuario.PATCH dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _context.Usuario.FindAsync(userName);
            if (user == null)
            {
                return NotFound(new
                {
                    message = "Usuario No encontrado",
                    status = 404
                });
            }

            if(dto.Clave != null) user.Clave = dto.Clave;
            if(dto.estado != null) user.estado = dto.estado.Value;
            if(dto.Persona_idPersona != null) user.Persona_idPersona = dto.Persona_idPersona.Value;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new DTO.Usuario.GET
                {
                    idUsuario = user.idUsuario,
                    estado = user.estado,
                    Clave = user.Clave,
                    Persona_idPersona = user.Persona_idPersona
                });
            }
            catch (DbUpdateException ex)
            {
                return Conflict(new Error(ex.Message, 409));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Error("Error Inesperado", 500, ex.StackTrace ?? ""));
            }
        }
    }
}
