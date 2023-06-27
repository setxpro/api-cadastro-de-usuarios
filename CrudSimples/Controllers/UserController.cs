using CrudSimples.Connection;
using CrudSimples.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudSimples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Usar a class que faz a conexão com o banco

        private ConnectionDatabase _context;
             
        public UserController(ConnectionDatabase connectionDb)
        {
            _context = connectionDb;
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user) 
        {
            // Users -> passado por parametro pelo construtor da class User
            this._context.Users.Add(user);
            await this._context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if(this._context == null)
            {
                return NotFound("Não há usuários cadastrados!");
            }
            return await this._context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            // Vai pegar o id pelo parametro
            var user = await _context.Users.FindAsync(id);

            // Verifica se existe esse usuário
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = this._context.Users.Where(c => c.Id == id).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            // Method to remove user
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Verify if user exists
        private bool UserExists(Guid id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
