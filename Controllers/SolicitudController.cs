using GestionAcademicaAPI.Dtos;
using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GestionAcademicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly ISolicitudService _solicitudService;

        public SolicitudController(ISolicitudService solicitudService)
        {
            _solicitudService = solicitudService;
        }

        [HttpPost]
        public async Task<ActionResult<SolicitudResponseDto>> AddAsync([FromBody] SolicitudRequestDto solicitudDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _solicitudService.AddAsyncSolicitud(solicitudDto);
                if (result == null || result.Id == 0)
                {
                    return StatusCode(500, new { Message = "No se pudo crear la solicitud correctamente." });
                }

                return Created($"api/Solicitud/{result.Id}", result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error inesperado en el servidor.", Details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudResponseDto>>> GetAllAsync()
        {
            var result = await _solicitudService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudResponseDto>> GetByIdAsync(int id)
        {
            var result = await _solicitudService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("estudiante/{idEstudiante}")]
        public async Task<ActionResult<IEnumerable<SolicitudResponseDto>>> GetByEstudianteIdAsync(int idEstudiante)
        {
            var result = await _solicitudService.GetByEstudianteIdAsync(idEstudiante);
            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<SolicitudResponseDto>>> GetByStatusAsync(string status)
        {
            var result = await _solicitudService.GetByStatusAsync(status);
            return Ok(result);
        }

        [HttpGet("fecha")]
        public async Task<ActionResult<IEnumerable<SolicitudResponseDto>>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
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
        public async Task<ActionResult<SolicitudResponseDto>> GetLastSolicitudByEstudianteAsync(int idEstudiante)
        {
            var result = await _solicitudService.GetLastSolicitudByEstudianteAsync(idEstudiante);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<SolicitudResponseDto>> UpdateAsync([FromBody] SolicitudUpdateDto solicitudDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _solicitudService.UpdateAsync(solicitudDto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al actualizar la solicitud.", Details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _solicitudService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al eliminar la solicitud.", Details = ex.Message });
            }
        }
    }
}