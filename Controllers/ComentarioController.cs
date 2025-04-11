using GestionAcademicaAPI.Dtos; // Add this for ComentarioDTO
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
        public async Task<IActionResult> Add([FromBody] ComentarioDTO comentarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to entity
            var comentario = new Comentario
            {
                Contenido = comentarioDto.Contenido,
                IdSolicitud = comentarioDto.IdSolicitud,
                IdUsuario = comentarioDto.IdUsuario,
                Fecha = comentarioDto.Fecha
            };

            var createdComentario = await _comentarioService.AddAsync(comentario);

            // Map entity back to DTO for response
            var createdComentarioDto = new ComentarioDTO
            {
                Contenido = createdComentario.Contenido,
                IdSolicitud = createdComentario.IdSolicitud,
                IdUsuario = createdComentario.IdUsuario,
                Fecha = createdComentario.Fecha
            };

            return CreatedAtAction(nameof(GetById), new { id = createdComentario.Id }, createdComentarioDto);
        }

        // Read
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comentarios = await _comentarioService.GetAllAsync();
            var comentarioDtos = comentarios.Select(c => new ComentarioDTO
            {
                Contenido = c.Contenido,
                IdSolicitud = c.IdSolicitud,
                IdUsuario = c.IdUsuario,
                Fecha = c.Fecha
            });
            return Ok(comentarioDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comentario = await _comentarioService.GetByIdAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }

            var comentarioDto = new ComentarioDTO
            {
                Contenido = comentario.Contenido,
                IdSolicitud = comentario.IdSolicitud,
                IdUsuario = comentario.IdUsuario,
                Fecha = comentario.Fecha
            };
            return Ok(comentarioDto);
        }

        [HttpGet("solicitud/{idSolicitud}")]
        public async Task<IActionResult> GetBySolicitudId(int idSolicitud)
        {
            var comentarios = await _comentarioService.GetBySolicitudIdAsync(idSolicitud);
            var comentarioDtos = comentarios.Select(c => new ComentarioDTO
            {
                Contenido = c.Contenido,
                IdSolicitud = c.IdSolicitud,
                IdUsuario = c.IdUsuario,
                Fecha = c.Fecha
            });
            return Ok(comentarioDtos);
        }

        [HttpGet("user/{idUsuario}")]
        public async Task<IActionResult> GetByUserId(int idUsuario)
        {
            var comentarios = await _comentarioService.GetByUserIdAsync(idUsuario);
            var comentarioDtos = comentarios.Select(c => new ComentarioDTO
            {
                Contenido = c.Contenido,
                IdSolicitud = c.IdSolicitud,
                IdUsuario = c.IdUsuario,
                Fecha = c.Fecha
            });
            return Ok(comentarioDtos);
        }

        [HttpGet("date-range")]
        public async Task<IActionResult> GetByDateRange(DateTime fechaInicio, DateTime fechaFin)
        {
            var comentarios = await _comentarioService.GetByDateRangeAsync(fechaInicio, fechaFin);
            var comentarioDtos = comentarios.Select(c => new ComentarioDTO
            {
                Contenido = c.Contenido,
                IdSolicitud = c.IdSolicitud,
                IdUsuario = c.IdUsuario,
                Fecha = c.Fecha
            });
            return Ok(comentarioDtos);
        }

        [HttpGet("count/solicitud/{idSolicitud}")]
        public async Task<IActionResult> CountBySolicitud(int idSolicitud)
        {
            var count = await _comentarioService.CountBySolicitudAsync(idSolicitud);
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
        public async Task<IActionResult> Update(int id, [FromBody] ComentarioDTO comentarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingComentario = await _comentarioService.GetByIdAsync(id);
            if (existingComentario == null)
            {
                return NotFound();
            }

            // Map DTO to existing entity
            existingComentario.Contenido = comentarioDto.Contenido;
            existingComentario.IdSolicitud = comentarioDto.IdSolicitud;
            existingComentario.IdUsuario = comentarioDto.IdUsuario;
            existingComentario.Fecha = comentarioDto.Fecha;

            await _comentarioService.UpdateAsync(existingComentario);
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