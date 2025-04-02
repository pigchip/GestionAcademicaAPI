using Microsoft.AspNetCore.Mvc;
using GestionAcademicaAPI.Helpers;
using GestionAcademicaAPI.DTOs;
using GestionAcademicaAPI.Services.Interfaces;
using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Dtos;

namespace GestionAcademicaAPI.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones relacionadas con los estudiantes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<EstudianteController> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EstudianteController"/>
        /// </summary>
        /// <param name="estudianteService">El servicio de estudiantes</param>
        /// <param name="usuarioService">El servicio de usuarios</param>
        /// <param name="logger">El registrador de eventos</param>
        public EstudianteController(
            IEstudianteService estudianteService,
            IUsuarioService usuarioService,
            ILogger<EstudianteController> logger)
        {
            _estudianteService = estudianteService;
            _usuarioService = usuarioService;
            _logger = logger;
        }

        /// <summary>
        /// Agrega un nuevo estudiante
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<EstudianteDTO>> Create([FromBody] CrearEstudianteRequest request)
        {
            try
            {
                // Verificar si el nombre de usuario ya existe
                if (await _usuarioService.ExisteNombreUsuarioAsync(request.Username))
                {
                    return Conflict($"El nombre de usuario '{request.Username}' ya está en uso.");
                }

                // Verificar si el correo electrónico ya existe
                if (await _usuarioService.ExisteCorreoElectronicoAsync(request.EmailPersonal))
                {
                    return Conflict($"El correo electrónico '{request.EmailPersonal}' ya está en uso.");
                }

                // Crear un DTO para agregar el usuario
                var usuarioDto = new EstudianteDTO
                {
                    Nombre = request.Nombre,
                    ApellidoMat = request.ApellidoMaterno,
                    ApellidoPat = request.ApellidoPaterno,
                    EmailEscolar = request.EmailEscolar,
                    Boleta = request.Boleta,
                    Carrera = request.Carrera,
                    Username = request.Username,
                    EmailPersonal = request.EmailPersonal,
                    Password = request.Password
                };

                // Agregar el estudiante
                var nuevoEstudiante = await _estudianteService.AddAsync(usuarioDto);

                var nuevoUser = await _usuarioService.ObtenerUsuarioPorIdAsync(nuevoEstudiante.IdUsuario);

                // Enviar correo de bienvenida
                if (nuevoUser == null)
                {
                    _logger.LogError("Usuario no encontrado después de crear el estudiante con ID {IdUsuario}", nuevoEstudiante.IdUsuario);
                    return BadRequest("Error al crear el estudiante: Usuario no encontrado.");
                }

                // Enviar correo de bienvenida
                await _usuarioService.EnviarCorreoBienvenidaAsync(nuevoUser.Id);
                return CreatedAtAction(nameof(GetById), new { id = nuevoEstudiante.IdUsuario }, nuevoEstudiante);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear estudiante");
                return BadRequest($"Error al crear el estudiante: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un estudiante por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetById(int id)
        {
            try
            {
                var estudiante = await _estudianteService.GetByIdAsync(id);
                return Ok(estudiante);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estudiante con ID {Id}", id);
                return BadRequest($"Error al obtener el estudiante: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un estudiante por el ID de usuario
        /// </summary>
        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<EstudianteDTO>> GetByUserId(int idUsuario)
        {
            try
            {
                var estudiante = await _estudianteService.GetByUserIdAsync(idUsuario);
                return Ok(estudiante);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estudiante con ID de usuario {IdUsuario}", idUsuario);
                return BadRequest($"Error al obtener el estudiante: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene todos los estudiantes
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetAll()
        {
            try
            {
                var estudiantes = await _estudianteService.GetAllAsync();
                return Ok(estudiantes);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los estudiantes");
                return BadRequest($"Error al obtener los estudiantes: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un estudiante existente
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<EstudianteDTO>> Update([FromBody] ActualizarEstudianteRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud es nula.");
            }

            try
            {
                // Autenticar al usuario
                var usuario = await _usuarioService.AutenticarUsuarioAsync(request.Username, request.Password);
                if (usuario == null)
                {
                    return Unauthorized("Nombre de usuario o contraseña incorrectos.");
                }

                // Verificar si el nuevo nombre de usuario ya existe (si se está cambiando)
                if (request.NuevoUsername != null &&
                    request.NuevoUsername != usuario.Username &&
                    await _usuarioService.ExisteNombreUsuarioAsync(request.NuevoUsername, usuario.Id))
                {
                    return Conflict($"El nombre de usuario '{request.NuevoUsername}' ya está en uso.");
                }

                // Verificar si el nuevo correo electrónico ya existe (si se está cambiando)
                if (request.NuevoEmailPersonal != null &&
                    request.NuevoEmailPersonal != usuario.EmailPersonal &&
                    await _usuarioService.ExisteCorreoElectronicoAsync(request.NuevoEmailPersonal, usuario.Id))
                {
                    return Conflict($"El correo electrónico '{request.NuevoEmailPersonal}' ya está en uso.");
                }

                // Verificar si el nuevo correo electrónico ya existe (si se está cambiando)
                if (request.NuevoEmailEscolar != null &&
                    request.NuevoEmailEscolar != usuario.EmailPersonal &&
                    await _usuarioService.ExisteCorreoElectronicoAsync(request.NuevoEmailEscolar, usuario.Id))
                {
                    return Conflict($"El correo electrónico '{request.NuevoEmailEscolar}' ya está en uso.");
                }

                var estudianteId = await _estudianteService.GetByUserIdAsync(usuario.Id);

                if(estudianteId == null)
                {
                    return NotFound("Estudiante no encontrado");
                }

                // Actualizar los datos del usuario
                var estudianteDTO = new EstudianteDTO
                {
                    IdUsuario = usuario.Id,
                    Username = request.NuevoUsername,
                    Password = request.NuevaPassword,
                    Nombre = request.NuevoNombre,
                    ApellidoPat = request.NuevoApellidoPaterno,
                    ApellidoMat = request.NuevoApellidoMaterno,
                    EmailPersonal = request.NuevoEmailPersonal,
                    EmailEscolar = request.NuevoEmailEscolar,
                    Boleta = request.NuevoBoleta,
                    Carrera = request.NuevaCarrera
                };

                var estudiante = await _estudianteService.UpdateAsync(estudianteDTO);

                return Ok(estudiante);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar estudiante");
                return BadRequest($"Error al actualizar el estudiante: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un estudiante
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Obtener el estudiante para conocer el ID de usuario asociado
                var estudiante = await _estudianteService.GetByIdAsync(id);
                if (estudiante == null)
                {
                    return NotFound($"Estudiante con ID {id} no encontrado");
                }

                // Eliminar el estudiante
                await _estudianteService.DeleteAsync(id);

                // También podríamos eliminar al usuario si es necesario
                // await _usuarioService.EliminarUsuarioAsync(estudiante.IdUsuario);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar estudiante con ID {Id}", id);
                return BadRequest($"Error al eliminar el estudiante: {ex.Message}");
            }
        }

        /// <summary>
        /// Verifica si existe un estudiante para un usuario específico
        /// </summary>
        [HttpGet("existe/usuario/{idUsuario}")]
        public async Task<ActionResult<bool>> ExistsForUser(int idUsuario)
        {
            try
            {
                // Verificar si el usuario existe
                var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(idUsuario);
                if (usuario == null)
                {
                    return NotFound($"Usuario con ID {idUsuario} no encontrado");
                }

                // Verificar si existe un estudiante para el usuario
                try
                {
                    var estudiante = await _estudianteService.GetByUserIdAsync(idUsuario);
                    // Si llegamos aquí sin excepción, el estudiante existe
                    return Ok(true);
                }
                catch (KeyNotFoundException)
                {
                    // Si no se encuentra, no existe un estudiante para este usuario
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar existencia de estudiante para usuario con ID {IdUsuario}", idUsuario);
                return BadRequest($"Error al verificar la existencia del estudiante: {ex.Message}");
            }
        }
    }
}
