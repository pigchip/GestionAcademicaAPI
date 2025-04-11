using GestionAcademicaAPI.Dtos;
using GestionAcademicaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GestionAcademicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropuestaController : ControllerBase
    {
        private readonly IPropuestaService _propuestaService;

        public PropuestaController(IPropuestaService propuestaService)
        {
            _propuestaService = propuestaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropuestaInfoDto>>> GetAllAsync()
        {
            var result = await _propuestaService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropuestaInfoDto>> GetByIdAsync(int id)
        {
            var result = await _propuestaService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("solicitud/{idSolicitud}")]
        public async Task<ActionResult<IEnumerable<PropuestaInfoDto>>> GetBySolicitudIdAsync(int idSolicitud)
        {
            var result = await _propuestaService.GetBySolicitudIdAsync(idSolicitud);
            return Ok(result);
        }

        [HttpGet("escuela/{idEscuela}")]
        public async Task<ActionResult<IEnumerable<PropuestaInfoDto>>> GetByEscuelaIdAsync(int idEscuela)
        {
            var result = await _propuestaService.GetByEscuelaIdAsync(idEscuela);
            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<PropuestaInfoDto>>> GetByStatusAsync(string status)
        {
            var result = await _propuestaService.GetByStatusAsync(status);
            return Ok(result);
        }

        [HttpGet("fecha")]
        public async Task<ActionResult<IEnumerable<PropuestaInfoDto>>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
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
        public async Task<ActionResult<PropuestaInfoDto>> GetLastPropuestaBySolicitudAsync(int idSolicitud)
        {
            var result = await _propuestaService.GetLastPropuestaBySolicitudAsync(idSolicitud);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<PropuestaInfoDto>> UpdateAsync([FromBody] PropuestaUpdateDto propuestaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _propuestaService.UpdateAsync(propuestaDto);
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
                return StatusCode(500, new { Message = "Error al actualizar la propuesta.", Details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _propuestaService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al eliminar la propuesta.", Details = ex.Message });
            }
        }
    }
}