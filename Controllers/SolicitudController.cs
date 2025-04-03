using GestionAcademicaAPI.Dtos;
using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionAcademicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly ISolicitudService _solicitudService;
        private readonly IEstudianteService _estudianteService;

        public SolicitudController(ISolicitudService solicitudService, IEstudianteService estudianteService)
        {
            _solicitudService = solicitudService;
            _estudianteService = estudianteService;
        }

        // Inside the AddAsync method, update the code as follows:

        [HttpPost]
        public async Task<ActionResult<Solicitud>> AddAsync(SolicitudDTO solicitudDto)
        {
            var getEstudiante = await _estudianteService.GetByIdAsync(solicitudDto.IdEstudiante);

            var solicitud = new Solicitud
            {
                IdEstudiante = solicitudDto.IdEstudiante,
                Status = solicitudDto.Status,
                Fecha = solicitudDto.Fecha,
                Estudiante = getEstudiante,
                Propuestas = solicitudDto.Propuestas.Select(p => new Propuesta
                {
                    IdEscuela = p.IdEscuela,
                    Escuela = p.Escuela,
                    Status = p.Status,
                    Fecha = p.Fecha,
                    Solicitud = null // Temporarily set to null
                }).ToList()
            };

            // Update the Solicitud property of each Propuesta
            foreach (var propuesta in solicitud.Propuestas)
            {
                propuesta.Solicitud = solicitud;
            }

            var result = await _solicitudService.AddAsync(solicitud);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitud>>> GetAllAsync()
        {
            var result = await _solicitudService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitud?>> GetByIdAsync(int id)
        {
            var result = await _solicitudService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("estudiante/{idEstudiante}")]
        public async Task<ActionResult<IEnumerable<Solicitud>>> GetByEstudianteIdAsync(int idEstudiante)
        {
            var result = await _solicitudService.GetByEstudianteIdAsync(idEstudiante);
            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Solicitud>>> GetByStatusAsync(string status)
        {
            var result = await _solicitudService.GetByStatusAsync(status);
            return Ok(result);
        }

        [HttpGet("fecha")]
        public async Task<ActionResult<IEnumerable<Solicitud>>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var result = await _solicitudService.GetByDateRangeAsync(fechaInicio, fechaFin);
            return Ok(result);
        }

        [HttpGet("count/estudiante/{idEstudiante}")]
        public async Task<ActionResult<int>> CountByEstudianteAsync(int idEstudiante)
        {
            var result = await _solicitudService.CountByEstudianteAsync(idEstudiante);
            return Ok(result);
        }

        [HttpGet("count/status/{status}")]
        public async Task<ActionResult<int>> CountByStatusAsync(string status)
        {
            var result = await _solicitudService.CountByStatusAsync(status);
            return Ok(result);
        }

        [HttpGet("last/estudiante/{idEstudiante}")]
        public async Task<ActionResult<Solicitud?>> GetLastSolicitudByEstudianteAsync(int idEstudiante)
        {
            var result = await _solicitudService.GetLastSolicitudByEstudianteAsync(idEstudiante);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Solicitud solicitud)
        {
            if (id != solicitud.Id)
            {
                return BadRequest();
            }

            await _solicitudService.UpdateAsync(solicitud);
            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _solicitudService.DeleteAsync(id);
            return NoContent();
        }
    }
}
