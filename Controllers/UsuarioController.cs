using GestionAcademicaAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionAcademicaAPI.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones relacionadas con los usuarios.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class 
        UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UsuarioController"/>.
        /// </summary>
        /// <param name="usuarioRepository">El repositorio de usuarios.</param>
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario.</param>
        /// <returns>El usuario con el ID especificado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(id);
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
        public async Task<ActionResult<Usuario>> GetUsuarioPorNombreUsuario(string nombreUsuario)
        {
            var usuario = await _usuarioRepository.ObtenerPorNombreUsuarioAsync(nombreUsuario);
            if (usuario == null)
            {
                return NotFound($"Usuario con nombre de usuario {nombreUsuario} no encontrado.");
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="request">La solicitud de creación de usuario.</param>
        /// <returns>El usuario creado.</returns>
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario([FromBody] CrearUsuarioRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud es nula.");
            }

            if (string.IsNullOrWhiteSpace(request.Username))
            {
                return BadRequest("El nombre de usuario es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(request.EmailPersonal))
            {
                return BadRequest("El correo electrónico personal es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("La contraseña es obligatoria.");
            }

            if (await _usuarioRepository.ExisteNombreUsuarioAsync(request.Username))
            {
                return Conflict($"El nombre de usuario {request.Username} ya está en uso.");
            }

            if (await _usuarioRepository.ExisteCorreoElectronicoAsync(request.EmailPersonal))
            {
                return Conflict($"El correo electrónico {request.EmailPersonal} ya está registrado.");
            }

            var usuario = new Usuario
            {
                Username = request.Username,
                EmailPersonal = request.EmailPersonal,
                Password = request.Password
            };

            var nuevoUsuario = await _usuarioRepository.CrearAsync(usuario);

            return CreatedAtAction(nameof(GetUsuario), new { id = nuevoUsuario.Id }, nuevoUsuario);
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario a eliminar.</param>
        /// <returns>Resultado de la operación de eliminación.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var resultado = await _usuarioRepository.EliminarAsync(id);
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
        public async Task<ActionResult<Usuario>> Autenticar([FromBody] LoginRequest request)
        {
            var usuario = await _usuarioRepository.AutenticarAsync(request.Username, request.Password);
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
        public async Task<ActionResult> RestablecerContraseña([FromBody] RestablecimientoContraseñaRequest request)
        {
            var exito = await _usuarioRepository.RestablecerContraseñaAsync(request.CorreoElectronico, request.Token, request.NuevaContraseña);
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
        public async Task<ActionResult> IniciarRestablecimientoContraseña(string correoElectronico)
        {
            var exito = await _usuarioRepository.IniciarProcesoRestablecimientoContraseñaAsync(correoElectronico);
            if (!exito)
            {
                return NotFound("Correo electrónico no registrado.");
            }

            return Ok("Se ha enviado un correo de restablecimiento a tu correo electrónico.");
        }

        /// <summary>
        /// Obtiene todos los usuarios.
        /// </summary>
        /// <returns>Una lista de todos los usuarios.</returns>
        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.ObtenerTodosAsync();
            return Ok(usuarios);
        }
        /// <summary>
        /// Modifica los datos personales de un usuario autenticándolo primero.
        /// </summary>
        /// <param name="request">La solicitud de actualización de datos personales que incluye el nombre de usuario, contraseña y los nuevos datos personales.</param>
        /// <returns>El usuario actualizado.</returns>
        [HttpPut("cambiarDatosPersonales")]
        public async Task<ActionResult<Usuario>> CambiarDatosPersonales([FromBody] CambiarDatosPersonalesRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud es nula.");
            }

            // Autenticar al usuario
            var usuario = await _usuarioRepository.AutenticarAsync(request.Username, request.Password);
            if (usuario == null)
            {
                return Unauthorized("Nombre de usuario o contraseña incorrectos.");
            }

            // Cambiar los datos personales del usuario autenticado
            usuario.EmailPersonal = request.EmailPersonal;

            var usuarioActualizado = await _usuarioRepository.ActualizarAsync(usuario);
            return Ok(usuarioActualizado);
        }

        /// <summary>
        /// Modifica la contraseña de un usuario autenticándolo primero.
        /// </summary>
        /// <param name="request">La solicitud de cambio de contraseña que incluye el nombre de usuario, contraseña actual y la nueva contraseña.</param>
        /// <returns>Resultado de la operación de cambio de contraseña.</returns>
        [HttpPut("cambiarContraseña")]
        public async Task<ActionResult> CambiarContraseña([FromBody] CambiarContraseñaRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud es nula.");
            }

            // Autenticar al usuario
            var usuario = await _usuarioRepository.AutenticarAsync(request.Username, request.Password);
            if (usuario == null)
            {
                return Unauthorized("Nombre de usuario o contraseña incorrectos.");
            }

            // Cambiar la contraseña del usuario autenticado
            usuario.Password = FunctionsHelper.HashPassword(request.NuevaContraseña);

            await _usuarioRepository.ActualizarAsync(usuario);
            return Ok("Contraseña cambiada correctamente.");
        }
    }

    /// <summary>
    /// Representa una solicitud de inicio de sesión.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// El nombre de usuario.
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// La contraseña.
        /// </summary>
        public string Password { get; set; } = string.Empty;
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

    /// <summary>
    /// Representa una solicitud para crear un nuevo usuario.
    /// </summary>
    public class CrearUsuarioRequest
    {
        /// <summary>
        /// El nombre de usuario.
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// El correo electrónico personal.
        /// </summary>
        public string EmailPersonal { get; set; } = string.Empty;
        /// <summary>
        /// La contraseña.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
