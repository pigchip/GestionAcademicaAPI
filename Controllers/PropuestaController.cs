using GestionAcademicaAPI.Dtos;
using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GestionAcademicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropuestaController : ControllerBase
    {
        private readonly IPropuestaService _propuestaService;
        private readonly IEstudianteService _estudianteService;

        public PropuestaController(IPropuestaService propuestaService, IEstudianteService estudianteService)
        {
            _propuestaService = propuestaService;
            _estudianteService = estudianteService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Propuesta>> AddAsync(PropuestaDTO propuestaDto)
        {

            var estudiante = await _estudianteService.GetByCredentials(propuestaDto.Username, propuestaDto.Password);

            var solicitud = new Solicitud
            {
                Id = propuestaDto.IdSolicitud,
                IdEstudiante = estudiante.Id,
                Status = propuestaDto.Status,
                Fecha = propuestaDto.Fecha,
                Estudiante = estudiante,
            };

            var propuesta = new Propuesta
            {
                IdSolicitud = propuestaDto.IdSolicitud,
                IdEscuela = propuestaDto.IdEscuela,
                Escuela = propuestaDto.Escuela,
                Status = propuestaDto.Status,
                Fecha = propuestaDto.Fecha,
                Solicitud = solicitud
            };

            var result = await _propuestaService.AddAsync(propuesta);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Propuesta>>> GetAllAsync()
        {
            var result = await _propuestaService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Propuesta?>> GetByIdAsync(int id)
        {
            var result = await _propuestaService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("solicitud/{idSolicitud}")]
        public async Task<ActionResult<IEnumerable<Propuesta>>> GetBySolicitudIdAsync(int idSolicitud)
        {
            var result = await _propuestaService.GetBySolicitudIdAsync(idSolicitud);
            return Ok(result);
        }

        [HttpGet("escuela/{idEscuela}")]
        public async Task<ActionResult<IEnumerable<Propuesta>>> GetByEscuelaIdAsync(int idEscuela)
        {
            var result = await _propuestaService.GetByEscuelaIdAsync(idEscuela);
            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Propuesta>>> GetByStatusAsync(string status)
        {
            var result = await _propuestaService.GetByStatusAsync(status);
            return Ok(result);
        }

        [HttpGet("fecha")]
        public async Task<ActionResult<IEnumerable<Propuesta>>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var result = await _propuestaService.GetByDateRangeAsync(fechaInicio, fechaFin);
            return Ok(result);
        }

        [HttpGet("count/solicitud/{idSolicitud}")]
        public async Task<ActionResult<int>> CountBySolicitudAsync(int idSolicitud)
        {
            var result = await _propuestaService.CountBySolicitudAsync(idSolicitud);
            return Ok(result);
        }

        [HttpGet("count/escuela/{idEscuela}")]
        public async Task<ActionResult<int>> CountByEscuelaAsync(int idEscuela)
        {
            var result = await _propuestaService.CountByEscuelaAsync(idEscuela);
            return Ok(result);
        }

        [HttpGet("count/status/{status}")]
        public async Task<ActionResult<int>> CountByStatusAsync(string status)
        {
            var result = await _propuestaService.CountByStatusAsync(status);
            return Ok(result);
        }

        [HttpGet("last/solicitud/{idSolicitud}")]
        public async Task<ActionResult<Propuesta?>> GetLastPropuestaBySolicitudAsync(int idSolicitud)
        {
            var result = await _propuestaService.GetLastPropuestaBySolicitudAsync(idSolicitud);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Propuesta propuesta)
        {
            if (id != propuesta.Id)
            {
                return BadRequest();
            }

            await _propuestaService.UpdateAsync(propuesta);
            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _propuestaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
