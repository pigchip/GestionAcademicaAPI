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
                return Ok(administrador);
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
        /// Actualiza la contraseña de un administrador
        /// </summary>
        [HttpPut("update-password")]
        public async Task<ActionResult> UpdatePassword([FromBody] ActualizarPasswordAdminRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud es nula.");
            }

            try
            {
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

                var usuarioDTO = new UsuarioDTO
                {
                    Username = request.Username,
                    Password = request.NuevaPassword,
                    EmailPersonal = usuario.EmailPersonal
                };

                await _usuarioService.ActualizarUsuarioAsync(usuario.Id, usuarioDTO);

                // Notificar cambios al usuario
                await _usuarioService.EnviarNotificacionActividadAsync(
                    usuario.Id,
                    "Se ha actualizado la contraseña de su cuenta de administrador."
                );

                return Ok("Contraseña actualizada correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la contraseña");
                return BadRequest($"Error al actualizar la contraseña: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza el nombre de usuario de un administrador
        /// </summary>
        [HttpPut("update-username")]
        public async Task<ActionResult> UpdateUsername([FromBody] ActualizarUsernameAdminRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud es nula.");
            }

            try
            {
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

                if (await _usuarioService.ExisteNombreUsuarioAsync(request.NuevoUsername, usuario.Id))
                {
                    return Conflict($"El nombre de usuario '{request.NuevoUsername}' ya está en uso.");
                }

                var usuarioDTO = new UsuarioDTO
                {
                    Username = request.NuevoUsername,
                    Password = request.Password,
                    EmailPersonal = usuario.EmailPersonal
                };

                await _usuarioService.ActualizarUsuarioAsync(usuario.Id, usuarioDTO);

                // Notificar cambios al usuario
                await _usuarioService.EnviarNotificacionActividadAsync(
                    usuario.Id,
                    "Se ha actualizado el nombre de usuario de su cuenta de administrador."
                );

                return Ok("Nombre de usuario actualizado correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el nombre de usuario");
                return BadRequest($"Error al actualizar el nombre de usuario: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza los detalles de un administrador
        /// </summary>
        [HttpPut("update-details")]
        public async Task<ActionResult<AdministradorDTO>> UpdateDetails([FromBody] ActualizarAdminDetallesRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud es nula.");
            }

            try
            {
                // 1. Autenticar al usuario (responsabilidad de autenticación)
                var usuario = await _usuarioService.AutenticarUsuarioAsync(request.Username, request.Password);
                if (usuario == null)
                {
                    return Unauthorized("Nombre de usuario o contraseña incorrectos.");
                }

                // 2. Verificar si existe un administrador asociado al usuario (responsabilidad de validación)
                var administrador = await _administradorService.GetByUserIdAsync(usuario.Id);
                if (administrador == null)
                {
                    return NotFound("Administrador no encontrado");
                }

                // 3. Verificar si el nuevo correo electrónico ya existe (si se está cambiando)
                if (request.NuevoEmailPersonal != null &&
                    request.NuevoEmailPersonal != usuario.EmailPersonal &&
                    await _usuarioService.ExisteCorreoElectronicoAsync(request.NuevoEmailPersonal, usuario.Id))
                {
                    return Conflict($"El correo electrónico '{request.NuevoEmailPersonal}' ya está en uso.");
                }

                // 4. Actualizar los datos del usuario
                var usuarioDto = new UsuarioDTO
                {
                    Username = request.Username,
                    EmailPersonal = request.NuevoEmailPersonal ?? usuario.EmailPersonal,
                    Password = request.Password
                };

                await _usuarioService.ActualizarUsuarioAsync(usuario.Id, usuarioDto);

                // 6. Notificar cambios al usuario
                await _usuarioService.EnviarNotificacionActividadAsync(
                    usuario.Id,
                    "Se han actualizado los detalles de su cuenta de administrador."
                );

                return Ok(usuarioDto);
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
                _logger.LogError(ex, "Error al actualizar los detalles del administrador");
                return BadRequest($"Error al actualizar los detalles del administrador: {ex.Message}");
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

                await _usuarioService.EliminarUsuarioAsync(administrador.IdUsuario);

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