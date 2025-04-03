using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionAcademicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioService _comentarioService;

        public ComentarioController(IComentarioService comentarioService)
        {
            _comentarioService = comentarioService;
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Comentario comentario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdComentario = await _comentarioService.AddAsync(comentario);
            return CreatedAtAction(nameof(GetById), new { id = createdComentario.Id }, createdComentario);
        }

        // Read
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comentarios = await _comentarioService.GetAllAsync();
            return Ok(comentarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comentario = await _comentarioService.GetByIdAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }
            return Ok(comentario);
        }

        [HttpGet("user/{idUsuario}")]
        public async Task<IActionResult> GetByUserId(int idUsuario)
        {
            var comentarios = await _comentarioService.GetByUserIdAsync(idUsuario);
            return Ok(comentarios);
        }

        [HttpGet("materia/{idMateria}")]
        public async Task<IActionResult> GetByMateriaId(int idMateria)
        {
            var comentarios = await _comentarioService.GetByMateriaIdAsync(idMateria);
            return Ok(comentarios);
        }

        [HttpGet("date-range")]
        public async Task<IActionResult> GetByDateRange(DateTime fechaInicio, DateTime fechaFin)
        {
            var comentarios = await _comentarioService.GetByDateRangeAsync(fechaInicio, fechaFin);
            return Ok(comentarios);
        }

        [HttpGet("count/materia/{idMateria}")]
        public async Task<IActionResult> CountByMateria(int idMateria)
        {
            var count = await _comentarioService.CountByMateriaAsync(idMateria);
            return Ok(count);
        }

        [HttpGet("count/user/{idUsuario}")]
        public async Task<IActionResult> CountByUser(int idUsuario)
        {
            var count = await _comentarioService.CountByUserAsync(idUsuario);
            return Ok(count);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Comentario comentario)
        {
            if (id != comentario.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _comentarioService.UpdateAsync(comentario);
            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comentario = await _comentarioService.GetByIdAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }

            await _comentarioService.DeleteAsync(id);
            return NoContent();
        }
    }
}
