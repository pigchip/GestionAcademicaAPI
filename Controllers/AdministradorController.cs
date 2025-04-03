using Microsoft.AspNetCore.Mvc;
using GestionAcademicaAPI.Helpers;
using GestionAcademicaAPI.DTOs;
using GestionAcademicaAPI.Services.Interfaces;
using GestionAcademicaAPI.Models;

namespace GestionAcademicaAPI.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones relacionadas con los administradores.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly IAdministradorService _administradorService;
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<AdministradorController> _logger;

        /// <summary>
        /// Inicializa una nueva instacia de la clase <see cref="AdministradorController"/>
        /// </summary>
        /// <param name="administradorService">El servicio de administradores</param>
        /// <param name="usuarioService">El servicio de usuarios</param>
        /// <param name="logger">El registrador de eventos</param>
        public AdministradorController(
            IAdministradorService administradorService,
            IUsuarioService usuarioService,
            ILogger<AdministradorController> logger)
        {
            _administradorService = administradorService;
            _usuarioService = usuarioService;
            _logger = logger;
        }

        /// <summary>
        /// Agrega un nuevo administrador
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<AdministradorDTO>> Create([FromBody] CrearUsuarioRequest request)
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
                var usuarioDto = new UsuarioDTO
                {
                    Username = request.Username,
                    EmailPersonal = request.EmailPersonal,
                    Password = request.Password
                };

                // Agregar el administrador
                var nuevoAdministrador = await _administradorService.AddAsync(usuarioDto);


                var nuevoUser = await _usuarioService.ObtenerUsuarioPorIdAsync(nuevoAdministrador.IdUsuario);

                // Enviar correo de bienvenida
                if (nuevoUser == null)
                {
                    _logger.LogError("Usuario no encontrado después de crear el administrador con ID {IdUsuario}", nuevoAdministrador.IdUsuario);
                    return BadRequest("Error al crear el administrador: Usuario no encontrado.");
                }

                // Enviar correo de bienvenida
                await _usuarioService.EnviarCorreoBienvenidaAsync(nuevoUser.Id);
                return CreatedAtAction(nameof(GetById), new { id = nuevoAdministrador.IdUsuario }, nuevoAdministrador);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear administrador");
                return BadRequest($"Error al crear el administrador: {ex.Message}");
            }
        }

        /// <summary>
        /// Inicia sesión como administrador
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<bool>> LoginAdmin(LoginRequest request)
        {
            try
            {
                // Autenticar al usuario
                var usuario = await _usuarioService.AutenticarUsuarioAsync(request.Username, request.Password);
                if (usuario == null)
                {
                    return Unauthorized("Nombre de usuario o contraseña incorrectos.");
                }
                // Verificar si el usuario es un administrador
                var administrador = await _administradorService.GetByUserIdAsync(usuario.Id);
                if (administrador == null)
                {
                    return Unauthorized("El usuario no es un administrador.");
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al autenticar administrador");
                return BadRequest($"Error al autenticar el administrador: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un administrador por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrador>> GetById(int id)
        {
            try
            {
                var administrador = await _administradorService.GetByIdAsync(id);
                return Ok(administrador);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener administrador con ID {Id}", id);
                return BadRequest($"Error al obtener el administrador: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un administrador por el ID de usuario
        /// </summary>
        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<AdministradorDTO>> GetByUserId(int idUsuario)
        {
            try
            {
                var administrador = await _administradorService.GetByUserIdAsync(idUsuario);
                return Ok(administrador);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener administrador con ID de usuario {IdUsuario}", idUsuario);
                return BadRequest($"Error al obtener el administrador: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene todos los administradores
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrador>>> GetAll()
        {
            try
            {
                var administradores = await _administradorService.GetAllAsync();
                return Ok(administradores);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los administradores");
                return BadRequest($"Error al obtener los administradores: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un administrador existente
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<Administrador>> Update([FromBody] ActualizarAdminRequest request)
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

                // Actualizar los datos del usuario
                var usuarioDto = new UsuarioDTO
                {
                    Username = request.NuevoUsername ?? usuario.Username,
                    EmailPersonal = request.NuevoEmailPersonal ?? usuario.EmailPersonal,
                    Password = request.NuevaPassword ?? usuario.Password
                };

                // Actualizar usuario
                await _usuarioService.ActualizarUsuarioAsync(usuario.Id, usuarioDto);

                // Notificar cambios al usuario
                await _usuarioService.EnviarNotificacionActividadAsync(
                    usuario.Id,
                    "Se ha actualizado la información de su cuenta de administrador."
                );

                var admin = await _administradorService.GetByUserIdAsync(usuario.Id);

                return Ok(admin);
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
                _logger.LogError(ex, "Error al actualizar administrador");
                return BadRequest($"Error al actualizar el administrador: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un administrador
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Obtener el administrador para conocer el ID de usuario asociado
                var administrador = await _administradorService.GetByIdAsync(id);
                if (administrador == null)
                {
                    return NotFound($"Administrador con ID {id} no encontrado");
                }

                // Eliminar el administrador
                await _administradorService.DeleteAsync(id);

                // También podríamos eliminar al usuario si es necesario
                // await _usuarioService.EliminarUsuarioAsync(administrador.IdUsuario);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar administrador con ID {Id}", id);
                return BadRequest($"Error al eliminar el administrador: {ex.Message}");
            }
        }

        /// <summary>
        /// Verifica si existe un administrador para un usuario específico
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

                // Verificar si existe un administrador para el usuario
                try
                {
                    var administrador = await _administradorService.GetByUserIdAsync(idUsuario);
                    // Si llegamos aquí sin excepción, el administrador existe
                    return Ok(true);
                }
                catch (KeyNotFoundException)
                {
                    // Si no se encuentra, no existe un administrador para este usuario
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar existencia de administrador para usuario con ID {IdUsuario}", idUsuario);
                return BadRequest($"Error al verificar la existencia del administrador: {ex.Message}");
            }
        }
    }
}