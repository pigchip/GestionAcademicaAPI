using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionAcademicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaService _materiaService;

        public MateriaController(IMateriaService materiaService)
        {
            _materiaService = materiaService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Materia>> Add(Materia materia)
        {
            var addedMateria = await _materiaService.AddAsync(materia);
            return CreatedAtAction(nameof(GetById), new { id = addedMateria.Id }, addedMateria);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materia>>> GetAll()
        {
            var materias = await _materiaService.GetAllAsync();
            return Ok(materias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Materia>> GetById(int id)
        {
            var materia = await _materiaService.GetByIdAsync(id);
            if (materia == null)
            {
                return NotFound();
            }
            return Ok(materia);
        }

        [HttpGet("estudiante/{idEstudiante}")]
        public async Task<ActionResult<IEnumerable<Materia>>> GetByEstudianteId(int idEstudiante)
        {
            var materias = await _materiaService.GetByEstudianteIdAsync(idEstudiante);
            return Ok(materias);
        }

        [HttpGet("nombreEscom/{nombreMateriaEscom}")]
        public async Task<ActionResult<IEnumerable<Materia>>> GetByNombreMateriaEscom(string nombreMateriaEscom)
        {
            var materias = await _materiaService.GetByNombreMateriaEscomAsync(nombreMateriaEscom);
            return Ok(materias);
        }

        [HttpGet("nombreForanea/{nombreMateriaForanea}")]
        public async Task<ActionResult<IEnumerable<Materia>>> GetByNombreMateriaForanea(string nombreMateriaForanea)
        {
            var materias = await _materiaService.GetByNombreMateriaForaneaAsync(nombreMateriaForanea);
            return Ok(materias);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Materia>>> GetByStatus(string status)
        {
            var materias = await _materiaService.GetByStatusAsync(status);
            return Ok(materias);
        }

        [HttpGet("count/estudiante/{idEstudiante}")]
        public async Task<ActionResult<int>> CountByEstudiante(int idEstudiante)
        {
            var count = await _materiaService.CountByEstudianteAsync(idEstudiante);
            return Ok(count);
        }

        [HttpGet("count/status/{status}")]
        public async Task<ActionResult<int>> CountByStatus(string status)
        {
            var count = await _materiaService.CountByStatusAsync(status);
            return Ok(count);
        }

        [HttpGet("exists/nombreEscom/{nombreMateriaEscom}")]
        public async Task<ActionResult<bool>> ExistsByNombreMateriaEscom(string nombreMateriaEscom)
        {
            var exists = await _materiaService.ExistsByNombreMateriaEscomAsync(nombreMateriaEscom);
            return Ok(exists);
        }

        [HttpGet("exists/nombreForanea/{nombreMateriaForanea}")]
        public async Task<ActionResult<bool>> ExistsByNombreMateriaForanea(string nombreMateriaForanea)
        {
            var exists = await _materiaService.ExistsByNombreMateriaForaneaAsync(nombreMateriaForanea);
            return Ok(exists);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Materia materia)
        {
            if (id != materia.Id)
            {
                return BadRequest();
            }

            await _materiaService.UpdateAsync(materia);
            return NoContent();
        }

        // Nuevo endpoint para actualizar el temario (ID en el cuerpo JSON)
        [HttpPatch("temario")]
        public async Task<IActionResult> UpdateTemario([FromBody] UpdateTemarioDto temarioDto)
        {
            if (temarioDto == null || temarioDto.Id <= 0 || string.IsNullOrWhiteSpace(temarioDto.TemarioMateriaForaneaUrl))
            {
                return BadRequest("El cuerpo de la solicitud debe incluir un ID válido y una URL del temario.");
            }

            try
            {
                await _materiaService.UpdateTemarioAsync(temarioDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Materia con ID {temarioDto.Id} no encontrada.");
            }
        }


        // Nuevo endpoint para actualizar el temario (ID en el cuerpo JSON)
        [HttpPatch("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDto temarioDto)
        {
            if (temarioDto == null || temarioDto.Id <= 0 || string.IsNullOrWhiteSpace(temarioDto.Status))
            {
                return BadRequest("El cuerpo de la solicitud debe incluir un ID válido y un status.");
            }

            try
            {
                await _materiaService.UpdateStatusAsync(temarioDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Materia con ID {temarioDto.Id} no encontrada.");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _materiaService.DeleteAsync(id);
            return NoContent();
        }
    }
}