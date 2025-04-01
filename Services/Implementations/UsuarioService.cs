using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionAcademicaAPI.DTOs;
using GestionAcademicaAPI.Helpers;
using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.Services.Implementations
{
    /// <summary>
    /// Implementación del servicio de gestión de usuarios.
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioService> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UsuarioService"/>.
        /// </summary>
        /// <param name="usuarioRepository">Repositorio de usuarios.</param>
        /// <param name="logger">Logger para el servicio.</param>
        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            ILogger<UsuarioService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<Usuario> RegistrarUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            _logger.LogInformation("Iniciando registro de usuario: {Username}", usuarioDTO.Username);

            // Mapeo de DTO a entidad
            var nuevoUsuario = new Usuario
            {
                Username = usuarioDTO.Username,
                EmailPersonal = usuarioDTO.EmailPersonal,
                Password = Helpers.Helpers.HashPassword(usuarioDTO.Password)
            };

            // Crear usuario a través del repositorio
            var usuarioCreado = await _usuarioRepository.CrearAsync(nuevoUsuario);

            _logger.LogInformation("Usuario registrado exitosamente: {Id}, {Username}", usuarioCreado.Id, usuarioCreado.Username);

            return usuarioCreado;
        }

        /// <inheritdoc/>
        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
        {
            return await _usuarioRepository.ObtenerPorIdAsync(id);
        }

        /// <inheritdoc/>
        public async Task<Usuario?> ObtenerUsuarioPorNombreUsuarioAsync(string nombreUsuario)
        {
            return await _usuarioRepository.ObtenerPorNombreUsuarioAsync(nombreUsuario);
        }

        /// <inheritdoc/>
        public async Task<Usuario?> ObtenerUsuarioPorCorreoElectronicoAsync(string correoElectronico)
        {
            return await _usuarioRepository.ObtenerPorCorreoElectronicoAsync(correoElectronico);
        }

        /// <inheritdoc/>
        public async Task<Usuario> ActualizarUsuarioAsync(int id, UsuarioDTO usuarioDTO)
        {
            _logger.LogInformation("Iniciando actualización de usuario: {Id}", id);

            // Validar el DTO
            var validationContext = new ValidationContext(usuarioDTO);
            Validator.ValidateObject(usuarioDTO, validationContext, validateAllProperties: true);

            var usuarioExistente = await _usuarioRepository.ObtenerPorIdAsync(id);
            if (usuarioExistente == null)
            {
                _logger.LogWarning("Intento de actualizar usuario inexistente: {Id}", id);
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado");
            }

            // Validar si el nuevo nombre de usuario ya existe (excluyendo al usuario actual)
            if (await _usuarioRepository.ExisteNombreUsuarioAsync(usuarioDTO.Username, id))
            {
                _logger.LogWarning("Intento de actualizar a un nombre de usuario ya existente: {Username}", usuarioDTO.Username);
                throw new InvalidOperationException($"El nombre de usuario '{usuarioDTO.Username}' ya está en uso");
            }

            // Validar si el nuevo correo ya existe (excluyendo al usuario actual)
            if (await _usuarioRepository.ExisteCorreoElectronicoAsync(usuarioDTO.EmailPersonal, id))
            {
                _logger.LogWarning("Intento de actualizar a un correo ya existente: {Email}", usuarioDTO.EmailPersonal);
                throw new InvalidOperationException($"El correo electrónico '{usuarioDTO.EmailPersonal}' ya está registrado");
            }

            // Actualizar propiedades
            usuarioExistente.Username = usuarioDTO.Username;
            usuarioExistente.EmailPersonal = usuarioDTO.EmailPersonal;

            // Actualizar contraseña solo si se proporcionó una nueva
            if (!string.IsNullOrEmpty(usuarioDTO.Password))
            {
                if (usuarioDTO.Password.Length < 5)
                {
                    throw new ArgumentException("La nueva contraseña debe tener al menos 5 caracteres");
                }
                usuarioExistente.Password = Helpers.Helpers.HashPassword(usuarioDTO.Password);
            }

            var usuarioActualizado = await _usuarioRepository.ActualizarAsync(usuarioExistente);

            _logger.LogInformation("Usuario actualizado exitosamente: {Id}", id);

            return usuarioActualizado;
        }

        /// <inheritdoc/>
        public async Task<bool> EliminarUsuarioAsync(int id)
        {
            _logger.LogInformation("Iniciando eliminación de usuario: {Id}", id);

            var resultado = await _usuarioRepository.EliminarAsync(id);

            if (resultado)
            {
                _logger.LogInformation("Usuario eliminado exitosamente: {Id}", id);
            }
            else
            {
                _logger.LogWarning("Intento de eliminar usuario inexistente: {Id}", id);
            }

            return resultado;
        }

        /// <inheritdoc/>
        public async Task<Usuario?> AutenticarUsuarioAsync(string nombreUsuarioOCorreo, string contraseña)
        {
            _logger.LogInformation("Intento de autenticación para: {Username}", nombreUsuarioOCorreo);

            var usuario = await _usuarioRepository.AutenticarAsync(nombreUsuarioOCorreo, contraseña);

            if (usuario != null)
            {
                _logger.LogInformation("Autenticación exitosa para usuario: {Id}, {Username}", usuario.Id, usuario.Username);
            }
            else
            {
                _logger.LogWarning("Autenticación fallida para: {Username}", nombreUsuarioOCorreo);
            }

            return usuario;
        }

        /// <inheritdoc/>
        public async Task<bool> IniciarRestablecimientoContraseñaAsync(string correoElectronico)
        {
            _logger.LogInformation("Iniciando proceso de restablecimiento de contraseña para: {Email}", correoElectronico);

            var resultado = await _usuarioRepository.IniciarProcesoRestablecimientoContraseñaAsync(correoElectronico);

            if (resultado)
            {
                _logger.LogInformation("Proceso de restablecimiento iniciado exitosamente para: {Email}", correoElectronico);
            }
            else
            {
                _logger.LogWarning("Fallo al iniciar proceso de restablecimiento para: {Email}", correoElectronico);
            }

            return resultado;
        }

        /// <inheritdoc/>
        public async Task<bool> RestablecerContraseñaAsync(string correoElectronico, string token, string nuevaContraseña)
        {
            _logger.LogInformation("Restableciendo contraseña para: {Email}", correoElectronico);

            // Validación de seguridad para la nueva contraseña
            if (string.IsNullOrEmpty(nuevaContraseña) || nuevaContraseña.Length < 5)
            {
                _logger.LogWarning("Intento de restablecer contraseña con una contraseña débil");
                throw new ArgumentException("La nueva contraseña debe tener al menos 5 caracteres");
            }

            var resultado = await _usuarioRepository.RestablecerContraseñaAsync(correoElectronico, token, nuevaContraseña);

            if (resultado)
            {
                _logger.LogInformation("Contraseña restablecida exitosamente para: {Email}", correoElectronico);

                // Notificar cambio de contraseña
                var usuario = await ObtenerUsuarioPorCorreoElectronicoAsync(correoElectronico);
                if (usuario != null)
                {
                    _ = EnviarNotificacionActividadAsync(usuario.Id, "Cambio de contraseña");
                }
            }
            else
            {
                _logger.LogWarning("Fallo al restablecer contraseña para: {Email}", correoElectronico);
            }

            return resultado;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Usuario>> ObtenerTodosUsuariosAsync()
        {
            return await _usuarioRepository.ObtenerTodosAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario, int? idExcluir = null)
        {
            return await _usuarioRepository.ExisteNombreUsuarioAsync(nombreUsuario, idExcluir);
        }
        
        /// <inheritdoc/>
        public async Task<bool> ExisteCorreoElectronicoAsync(string correoElectronico, int? idExcluir = null)
        {
            return await _usuarioRepository.ExisteCorreoElectronicoAsync(correoElectronico, idExcluir);
        }

        /// <inheritdoc/>
        public async Task<bool> EnviarCorreoBienvenidaAsync(int usuarioId)
        {
            _logger.LogInformation("Enviando correo de bienvenida a usuario: {Id}", usuarioId);

            var usuario = await _usuarioRepository.ObtenerPorIdAsync(usuarioId);
            if (usuario == null)
            {
                _logger.LogWarning("No se puede enviar correo de bienvenida. Usuario no encontrado: {Id}", usuarioId);
                return false;
            }

            var resultado = await _usuarioRepository.EnviarCorreoAutomatizadoAsync(
                usuario,
                TipoCorreoElectronico.Bienvenida);

            if (resultado)
            {
                _logger.LogInformation("Correo de bienvenida enviado exitosamente a: {Email}", usuario.EmailPersonal);
            }
            else
            {
                _logger.LogWarning("Fallo al enviar correo de bienvenida a: {Email}", usuario.EmailPersonal);
            }

            return resultado;
        }

        /// <inheritdoc/>
        public async Task<bool> EnviarNotificacionActividadAsync(int usuarioId, string detallesActividad)
        {
            _logger.LogInformation("Enviando notificación de actividad a usuario: {Id}, Actividad: {Actividad}",
                usuarioId, detallesActividad);

            var usuario = await _usuarioRepository.ObtenerPorIdAsync(usuarioId);
            if (usuario == null)
            {
                _logger.LogWarning("No se puede enviar notificación. Usuario no encontrado: {Id}", usuarioId);
                return false;
            }

            var parametros = new Dictionary<string, string>
            {
                { "actividad", detallesActividad },
                { "fecha", DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") }
            };

            var resultado = await _usuarioRepository.EnviarCorreoAutomatizadoAsync(
                usuario,
                TipoCorreoElectronico.NotificacionActividad,
                parametros);

            if (resultado)
            {
                _logger.LogInformation("Notificación de actividad enviada exitosamente a: {Email}", usuario.EmailPersonal);
            }
            else
            {
                _logger.LogWarning("Fallo al enviar notificación de actividad a: {Email}", usuario.EmailPersonal);
            }

            return resultado;
        }
    }
}