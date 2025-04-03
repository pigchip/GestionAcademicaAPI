using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscuelaController : ControllerBase
    {
        private readonly IEscuelaService _escuelaService;

        public EscuelaController(IEscuelaService escuelaService)
        {
            _escuelaService = escuelaService ?? throw new ArgumentNullException(nameof(escuelaService));
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Escuela>> Add(Escuela escuela)
        {
            if (escuela == null)
            {
                return BadRequest("La escuela no puede ser nula");
            }

            var addedEscuela = await _escuelaService.AddAsync(escuela);
            return CreatedAtAction(nameof(GetById), new { id = addedEscuela.Id }, addedEscuela);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Escuela>>> GetAll()
        {
            var escuelas = await _escuelaService.GetAllAsync();
            return Ok(escuelas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Escuela>> GetById(int id)
        {
            var escuela = await _escuelaService.GetByIdAsync(id);
            if (escuela == null)
            {
                return NotFound();
            }
            return Ok(escuela);
        }

        [HttpGet("nombre/{nombre}")]
        public async Task<ActionResult<Escuela>> GetByNombre(string nombre)
        {
            var escuela = await _escuelaService.GetByNombreAsync(nombre);
            if (escuela == null)
            {
                return NotFound();
            }
            return Ok(escuela);
        }

        [HttpGet("exists/{nombre}")]
        public async Task<ActionResult<bool>> ExistsByNombre(string nombre)
        {
            var exists = await _escuelaService.ExistsByNombreAsync(nombre);
            return Ok(exists);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> Count()
        {
            var count = await _escuelaService.CountAsync();
            return Ok(count);
        }

        [HttpGet("with-propostas")]
        public async Task<ActionResult<IEnumerable<Escuela>>> GetEscuelasWithPropostas()
        {
            var escuelas = await _escuelaService.GetEscuelasWithPropostasAsync();
            return Ok(escuelas);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Escuela escuela)
        {
            if (id != escuela.Id)
            {
                return BadRequest("El ID de la escuela no coincide");
            }

            await _escuelaService.UpdateAsync(escuela);
            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var escuela = await _escuelaService.GetByIdAsync(id);
            if (escuela == null)
            {
                return NotFound();
            }

            await _escuelaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
