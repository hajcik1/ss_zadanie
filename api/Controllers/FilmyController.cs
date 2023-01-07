using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojeFilmyAPI.Models;

namespace MojeFilmyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmyController : ControllerBase
    {
        private readonly MojeFilmyContext _context;

        public FilmyController(MojeFilmyContext context)
        {
            _context= context;
        }

        [HttpGet]

        public async Task<IActionResult> GetAsync()
        {
            var films = await _context.Filmies.ToListAsync();
            return Ok(films);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFilmByIdAsync(int id)
        {
            var film = await _context.Filmies.FindAsync(id);
            return Ok(film);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Filmy film)
        {
            _context.Filmies.Add(film);
            await _context.SaveChangesAsync();
            return Ok(film);
        }

        [HttpPut]

        public async Task<IActionResult> PutAsync(Filmy filmToUpdate)
        {
            _context.Filmies.Update(filmToUpdate);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var filmToDelete = await _context.Filmies.FindAsync(id);
            if (filmToDelete == null)
            {
                return NotFound();
            }
            _context.Filmies.Remove(filmToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
