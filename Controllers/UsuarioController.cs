using GestionAcademicaAPI.DTOs;
using GestionAcademicaAPI.Helpers;
using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones relacionadas con los usuarios.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UsuarioController"/>.
        /// </summary>
        /// <param name="usuarioService">El servicio de usuarios.</param>
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="request">La solicitud de creación de usuario.</param>
        /// <returns>El usuario creado.</returns>
        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUser([FromBody] CrearUsuarioRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud es nula.");
            }

            var usuarioDTO = new UsuarioDTO
            {
                Username = request.Username,
                EmailPersonal = request.EmailPersonal,
                Password = request.Password
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(usuarioDTO);
            if (!Validator.TryValidateObject(usuarioDTO, validationContext, validationResults, true))
            {
                return BadRequest(validationResults);
            }

            try
            {
                var nuevoUsuario = await _usuarioService.RegistrarUsuarioAsync(usuarioDTO);
                return CreatedAtAction(nameof(GetUserById), new { id = nuevoUsuario.Id }, nuevoUsuario);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario.</param>
        /// <returns>El usuario con el ID especificado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUserById(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado.");
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Obtiene un usuario por su nombre de usuario.
        /// </summary>
        /// <param name="nombreUsuario">El nombre de usuario.</param>
        /// <returns>El usuario con el nombre de usuario especificado.</returns>
        [HttpGet("nombreUsuario/{nombreUsuario}")]
        public async Task<ActionResult<Usuario>> GetUserByUsername(string nombreUsuario)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorNombreUsuarioAsync(nombreUsuario);
            if (usuario == null)
            {
                return NotFound($"Usuario con nombre de usuario {nombreUsuario} no encontrado.");
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Obtiene todos los usuarios.
        /// </summary>
        /// <returns>Una lista de todos los usuarios.</returns>
        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAllUsers()
        {
            var usuarios = await _usuarioService.ObtenerTodosUsuariosAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario a eliminar.</param>
        /// <returns>Resultado de la operación de eliminación.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var resultado = await _usuarioService.EliminarUsuarioAsync(id);
            if (!resultado)
            {
                return NotFound($"Usuario con ID {id} no encontrado.");
            }

            return NoContent();
        }

        /// <summary>
        /// Autentica un usuario.
        /// </summary>
        /// <param name="request">La solicitud de autenticación.</param>
        /// <returns>El usuario autenticado.</returns>
        [HttpPost("autenticar")]
        public async Task<ActionResult<Usuario>> Authenticate([FromBody] LoginRequest request)
        {
            var usuario = await _usuarioService.AutenticarUsuarioAsync(request.Username, request.Password);
            if (usuario == null)
            {
                return Unauthorized("Nombre de usuario o contraseña incorrectos.");
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Restablece la contraseña de un usuario.
        /// </summary>
        /// <param name="request">La solicitud de restablecimiento de contraseña.</param>
        /// <returns>Resultado de la operación de restablecimiento.</returns>
        [HttpPost("restablecerContraseña")]
        public async Task<ActionResult> ResetPassword([FromBody] RestablecimientoContraseñaRequest request)
        {
            var exito = await _usuarioService.RestablecerContraseñaAsync(request.CorreoElectronico, request.Token, request.NuevaContraseña);
            if (!exito)
            {
                return BadRequest("Token inválido o ha expirado.");
            }

            return Ok("Contraseña restablecida correctamente.");
        }

        /// <summary>
        /// Inicia el proceso de restablecimiento de contraseña.
        /// </summary>
        /// <param name="correoElectronico">El correo electrónico del usuario.</param>
        /// <returns>Resultado de la operación de inicio de restablecimiento.</returns>
        [HttpPost("iniciarRestablecimientoContraseña")]
        public async Task<ActionResult> StartPasswordReset(string correoElectronico)
        {
            var exito = await _usuarioService.IniciarRestablecimientoContraseñaAsync(correoElectronico);
            if (!exito)
            {
                return NotFound("Correo electrónico no registrado.");
            }

            return Ok("Se ha enviado un correo de restablecimiento a tu correo electrónico.");
        }

        /// <summary>
        /// Modifica los datos personales de un usuario autenticándolo primero.
        /// </summary>
        /// <param name="request">La solicitud de actualización de datos personales que incluye el nombre de usuario, contraseña y los nuevos datos personales.</param>
        /// <returns>El usuario actualizado.</returns>
        [HttpPut("cambiarDatosPersonales")]
        public async Task<ActionResult<Usuario>> UpdatePersonalData([FromBody] CambiarDatosPersonalesRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud es nula.");
            }

            // Autenticar al usuario
            var usuario = await _usuarioService.AutenticarUsuarioAsync(request.Username, request.Password);
            if (usuario == null)
            {
                return Unauthorized("Nombre de usuario o contraseña incorrectos.");
            }

            var usuarioDTO = new UsuarioDTO
            {
                Username = request.Username,
                EmailPersonal = request.EmailPersonal,
                Password = request.Password
            };

            var usuarioActualizado = await _usuarioService.ActualizarUsuarioAsync(usuario.Id, usuarioDTO);
            return Ok(usuarioActualizado);
        }

        /// <summary>
        /// Modifica la contraseña de un usuario autenticándolo primero.
        /// </summary>
        /// <param name="request">La solicitud de cambio de contraseña que incluye el nombre de usuario, contraseña actual y la nueva contraseña.</param>
        /// <returns>Resultado de la operación de cambio de contraseña.</returns>
        [HttpPut("cambiarContraseña")]
        public async Task<ActionResult> ChangePassword([FromBody] CambiarContraseñaRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud es nula.");
            }

            // Autenticar al usuario
            var usuario = await _usuarioService.AutenticarUsuarioAsync(request.Username, request.Password);
            if (usuario == null)
            {
                return Unauthorized("Nombre de usuario o contraseña incorrectos.");
            }

            var usuarioDTO = new UsuarioDTO
            {
                Username = request.Username,
                EmailPersonal = usuario.EmailPersonal,
                Password = request.NuevaContraseña
            };

            await _usuarioService.ActualizarUsuarioAsync(usuario.Id, usuarioDTO);
            return Ok("Contraseña cambiada correctamente.");
        }
    }

    /// <summary>
    /// Representa una solicitud de restablecimiento de contraseña.
    /// </summary>
    public class RestablecimientoContraseñaRequest
    {
        /// <summary>
        /// El correo electrónico del usuario.
        /// </summary>
        public string CorreoElectronico { get; set; } = string.Empty;
        /// <summary>
        /// El token de restablecimiento.
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// La nueva contraseña.
        /// </summary>
        public string NuevaContraseña { get; set; } = string.Empty;
    }

    /// <summary>
    /// Representa una solicitud de cambio de datos personales.
    /// </summary>
    public class CambiarDatosPersonalesRequest
    {
        /// <summary>
        /// El nombre de usuario.
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// La contraseña actual.
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// El nuevo correo electrónico personal.
        /// </summary>
        public string EmailPersonal { get; set; } = string.Empty;
    }

    /// <summary>
    /// Representa una solicitud de cambio de contraseña.
    /// </summary>
    public class CambiarContraseñaRequest
    {
        /// <summary>
        /// El nombre de usuario.
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// La contraseña actual.
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// La nueva contraseña.
        /// </summary>
        public string NuevaContraseña { get; set; } = string.Empty;
    }
}
