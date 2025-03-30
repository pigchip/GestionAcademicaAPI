using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GestionAcademicaAPI.Models.Entities;
using GestionAcademicaAPI.Helpers;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MyProject.Data;
using MyProject.Models;
using MyProject.Repositories.Interfaces;

namespace GestionAcademicaAPI.Data
{
    /// <summary>
    /// Repositorio para gestionar entidades de tipo Usuario.
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<IUsuarioRepository> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UsuarioRepository"/>.
        /// </summary>
        /// <param name="context">El contexto de la base de datos.</param>
        /// <param name="configuration">La configuración de la aplicación.</param>
        /// <param name="logger">El registrador de eventos.</param>
        public UsuarioRepository(
            AppDbContext context,
            IConfiguration configuration,
            ILogger<IUsuarioRepository> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene un Usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del Usuario.</param>
        /// <returns>La entidad Usuario si se encuentra; de lo contrario, null.</returns>
        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            return await _context.Set<Usuario>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Obtiene un Usuario por su nombre de usuario.
        /// </summary>
        /// <param name="nombreUsuario">El nombre de usuario del Usuario.</param>
        /// <returns>La entidad Usuario si se encuentra; de lo contrario, null.</returns>
        public async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
        {
            return await _context.Set<Usuario>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == nombreUsuario);
        }

        /// <summary>
        /// Obtiene un Usuario por su correo electrónico.
        /// </summary>
        /// <param name="correoElectronico">El correo electrónico del Usuario.</param>
        /// <returns>La entidad Usuario si se encuentra; de lo contrario, null.</returns>
        public async Task<Usuario?> ObtenerPorCorreoElectronicoAsync(string correoElectronico)
        {
            return await _context.Set<Usuario>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.EmailPersonal == correoElectronico);
        }

        /// <summary>
        /// Crea un nuevo Usuario.
        /// </summary>
        /// <param name="usuario">La entidad Usuario a crear.</param>
        /// <returns>La entidad Usuario creada.</returns>
        public async Task<Usuario> CrearAsync(Usuario usuario)
        {
            usuario.Password = FunctionsHelper.HashPassword(usuario.Password);
            _context.Set<Usuario>().Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        /// <summary>
        /// Actualiza un Usuario existente.
        /// </summary>
        /// <param name="usuario">La entidad Usuario a actualizar.</param>
        /// <returns>La entidad Usuario actualizada.</returns>
        public async Task<Usuario> ActualizarAsync(Usuario usuario)
        {
            var usuarioExistente = await _context.Set<Usuario>().FindAsync(usuario.Id);

            if (usuarioExistente == null)
            {
                throw new KeyNotFoundException($"Usuario con ID {usuario.Id} no encontrado");
            }

            if (!string.IsNullOrEmpty(usuario.Password) && usuario.Password != usuarioExistente.Password)
            {
                usuarioExistente.Password = FunctionsHelper.HashPassword(usuario.Password);
            }

            usuarioExistente.Username = usuario.Username;
            usuarioExistente.EmailPersonal = usuario.EmailPersonal;

            await _context.SaveChangesAsync();
            return usuarioExistente;
        }

        /// <summary>
        /// Elimina un Usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del Usuario a eliminar.</param>
        /// <returns>True si el Usuario fue eliminado; de lo contrario, false.</returns>
        public async Task<bool> EliminarAsync(int id)
        {
            var usuario = await _context.Set<Usuario>().FindAsync(id);

            if (usuario == null)
            {
                return false;
            }

            _context.Set<Usuario>().Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Verifica si un nombre de usuario existe.
        /// </summary>
        /// <param name="nombreUsuario">El nombre de usuario a verificar.</param>
        /// <returns>True si el nombre de usuario existe; de lo contrario, false.</returns>
        public async Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario)
        {
            return await _context.Set<Usuario>()
                .AnyAsync(u => u.Username == nombreUsuario);
        }

        /// <summary>
        /// Verifica si un correo electrónico existe.
        /// </summary>
        /// <param name="correoElectronico">El correo electrónico a verificar.</param>
        /// <returns>True si el correo electrónico existe; de lo contrario, false.</returns>
        public async Task<bool> ExisteCorreoElectronicoAsync(string correoElectronico)
        {
            return await _context.Set<Usuario>()
                .AnyAsync(u => u.EmailPersonal == correoElectronico);
        }

        /// <summary>
        /// Autentica un Usuario por nombre de usuario o correo electrónico y contraseña.
        /// </summary>
        /// <param name="nombreUsuario">El nombre de usuario o correo electrónico del Usuario.</param>
        /// <param name="contraseña">La contraseña del Usuario.</param>
        /// <returns>La entidad Usuario autenticada si es exitoso; de lo contrario, null.</returns>
        public async Task<Usuario?> AutenticarAsync(string nombreUsuario, string contraseña)
        {
            var usuario = await _context.Set<Usuario>()
                .FirstOrDefaultAsync(u =>
                    u.Username == nombreUsuario ||
                    u.EmailPersonal == nombreUsuario);

            if (usuario == null)
            {
                return null;
            }

            if (FunctionsHelper.VerifyPassword(contraseña, usuario.Password))
            {
                return usuario;
            }

            return null;
        }

        /// <summary>
        /// Inicia el proceso de restablecimiento de contraseña para un Usuario.
        /// </summary>
        /// <param name="correoElectronico">El correo electrónico del Usuario.</param>
        /// <returns>True si el proceso fue iniciado; de lo contrario, false.</returns>
        public async Task<bool> IniciarProcesoRestablecimientoContraseñaAsync(string correoElectronico)
        {
            var usuario = await ObtenerPorCorreoElectronicoAsync(correoElectronico);

            if (usuario == null)
            {
                return false;
            }

            string token = await GenerarTokenRestablecimientoAsync(correoElectronico);

            // Adjuntar el objeto usuario al contexto
            _context.Attach(usuario);

            usuario.ResetPasswordToken = token;
            usuario.ResetPasswordTokenExpiration = DateTime.UtcNow.AddHours(24);

            await _context.SaveChangesAsync();

            var parametros = new Dictionary<string, string>
            {
                { "token", token },
                { "nombre", usuario.Username }
            };

            return await EnviarCorreoAutomatizadoAsync(usuario, TipoCorreoElectronico.RestablecimientoContraseña, parametros);
        }

        /// <summary>
        /// Restablece la contraseña de un Usuario.
        /// </summary>
        /// <param name="correoElectronico">El correo electrónico del Usuario.</param>
        /// <param name="token">El token de restablecimiento de contraseña.</param>
        /// <param name="nuevaContraseña">La nueva contraseña.</param>
        /// <returns>True si la contraseña fue restablecida; de lo contrario, false.</returns>
        public async Task<bool> RestablecerContraseñaAsync(string correoElectronico, string token, string nuevaContraseña)
        {
            var usuario = await _context.Set<Usuario>()
                .FirstOrDefaultAsync(u =>
                    u.EmailPersonal == correoElectronico &&
                    u.ResetPasswordToken == token &&
                    u.ResetPasswordTokenExpiration > DateTime.UtcNow);

            if (usuario == null)
            {
                return false;
            }

            usuario.Password = FunctionsHelper.HashPassword(nuevaContraseña);
            usuario.ResetPasswordToken = null;
            usuario.ResetPasswordTokenExpiration = null;

            await _context.SaveChangesAsync();

            await EnviarCorreoAutomatizadoAsync(usuario, TipoCorreoElectronico.CambioContraseña);

            return true;
        }

        /// <summary>
        /// Obtiene todas las entidades Usuario.
        /// </summary>
        /// <returns>Una lista de todas las entidades Usuario.</returns>
        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            return await _context.Set<Usuario>()
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Envía un correo electrónico automatizado a un Usuario.
        /// </summary>
        /// <param name="usuario">El Usuario al que se enviará el correo.</param>
        /// <param name="tipoCorreo">El tipo de correo a enviar.</param>
        /// <param name="parametrosAdicionales">Parámetros adicionales para el correo.</param>
        /// <returns>True si el correo fue enviado; de lo contrario, false.</returns>
        public async Task<bool> EnviarCorreoAutomatizadoAsync(
            Usuario usuario,
            TipoCorreoElectronico tipoCorreo,
            Dictionary<string, string>? parametrosAdicionales = null)
        {
            try
            {
                var message = new MimeMessage();

                string emailFrom = Environment.GetEnvironmentVariable("SMTP_FROM") ?? "noreply@gestionacademica.com";
                string nombreFrom = Environment.GetEnvironmentVariable("SMTP_FROM_NAME") ?? "Gestión Académica";

                message.From.Add(new MailboxAddress(nombreFrom, emailFrom));
                message.To.Add(new MailboxAddress(usuario.Username, usuario.EmailPersonal));

                ConfigurarContenidoCorreo(message, usuario, tipoCorreo, parametrosAdicionales);

                using var client = new MailKit.Net.Smtp.SmtpClient();

                string smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER") ?? "smtp.example.com";
                int smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");
                string smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME") ?? "";
                string smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? "";
                bool useSsl = bool.TryParse(Environment.GetEnvironmentVariable("SMTP_USE_SSL"), out var ssl) && ssl;

                // Asegurar que los valores son correctos
                if (string.IsNullOrWhiteSpace(smtpServer) || smtpPort == 0)
                {
                    _logger.LogError("Configuración SMTP inválida. SMTP_SERVER o SMTP_PORT no definidos.");
                    return false;
                }

                // Intentar conexión con un reintento en caso de fallo
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.Auto);
                        break; // Si la conexión es exitosa, salir del bucle
                    }
                    catch (SmtpProtocolException ex) when (i < 2)
                    {
                        _logger.LogWarning("Fallo al conectar al SMTP. Reintentando... ({Intento}/3)", i + 1);
                        await Task.Delay(2000); // Esperar 2 segundos antes de reintentar
                    }
                }

                if (!client.IsConnected)
                {
                    _logger.LogError("No se pudo conectar al servidor SMTP.");
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(smtpUsername) && !string.IsNullOrWhiteSpace(smtpPassword))
                {
                    await client.AuthenticateAsync(smtpUsername, smtpPassword);
                }

                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                await RegistrarIntentoCorreoAsync(usuario, tipoCorreo, true);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar correo electrónico de tipo {TipoCorreo} a {CorreoElectronico}",
                    tipoCorreo, usuario.EmailPersonal);

                await RegistrarIntentoCorreoAsync(usuario, tipoCorreo, false, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Registra un intento de envío de correo electrónico.
        /// </summary>
        /// <param name="usuario">El Usuario al que se envió el correo.</param>
        /// <param name="tipoCorreo">El tipo de correo enviado.</param>
        /// <param name="resultado">El resultado del intento de envío.</param>
        /// <param name="detallesError">Detalles de cualquier error ocurrido.</param>
        /// <returns>True si el intento fue registrado; de lo contrario, false.</returns>
        public async Task<bool> RegistrarIntentoCorreoAsync(
            Usuario usuario,
            TipoCorreoElectronico tipoCorreo,
            bool resultado,
            string? detallesError = null)
        {
            try
            {
                if (_context.Entry(usuario).State == EntityState.Detached)
                {
                    _context.Attach(usuario);
                }

                var registroCorreo = new RegistroEnvioCorreo
                {
                    TipoCorreo = tipoCorreo.ToString(),
                    FechaEnvio = DateTime.UtcNow,
                    Resultado = resultado,
                    DetallesError = detallesError,
                    Usuario = usuario
                };

                _context.Set<RegistroEnvioCorreo>().Add(registroCorreo);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar intento de envío de correo");
                return false;
            }
        }

        /// <summary>
        /// Genera un token de restablecimiento de contraseña para un Usuario.
        /// </summary>
        /// <param name="correoElectronico">El correo electrónico del Usuario.</param>
        /// <returns>El token generado.</returns>
        public async Task<string> GenerarTokenRestablecimientoAsync(string correoElectronico)
        {
            var usuario = await ObtenerPorCorreoElectronicoAsync(correoElectronico);

            if (usuario == null)
            {
                throw new KeyNotFoundException($"Usuario con correo {correoElectronico} no encontrado");
            }

            byte[] tokenBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenBytes);
            }

            string token = Convert.ToBase64String(tokenBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");

            return token;
        }

        #region Métodos Privados

        private void ConfigurarContenidoCorreo(
            MimeMessage message,
            Usuario usuario,
            TipoCorreoElectronico tipoCorreo,
            Dictionary<string, string>? parametrosAdicionales)
        {
            string asunto = string.Empty;
            string cuerpo = string.Empty;
            string plantillaPath;

            switch (tipoCorreo)
            {
                case TipoCorreoElectronico.Bienvenida:
                    asunto = "Bienvenido a Gestión Académica";
                    plantillaPath = "Plantillas/Correo/Bienvenida.html";
                    break;

                case TipoCorreoElectronico.RestablecimientoContraseña:
                    asunto = "Restablecer contraseña";
                    plantillaPath = "Plantillas/Correo/RestablecimientoContrasena.html";
                    break;

                case TipoCorreoElectronico.ConfirmacionRegistro:
                    asunto = "Confirma tu registro";
                    plantillaPath = "Plantillas/Correo/ConfirmacionRegistro.html";
                    break;

                case TipoCorreoElectronico.CambioContraseña:
                    asunto = "Cambio de contraseña";
                    plantillaPath = "Plantillas/Correo/CambioContrasena.html";
                    break;

                case TipoCorreoElectronico.NotificacionActividad:
                    asunto = "Actividad reciente en tu cuenta";
                    plantillaPath = "Plantillas/Correo/NotificacionActividad.html";
                    break;

                default:
                    throw new ArgumentException($"Tipo de correo no soportado: {tipoCorreo}");
            }

            string plantillaHtml = ObtenerPlantillaCorreo(plantillaPath);

            cuerpo = ReemplazarVariablesPlantilla(plantillaHtml, usuario, parametrosAdicionales);

            message.Subject = asunto;

            var builder = new BodyBuilder
            {
                HtmlBody = cuerpo
            };

            message.Body = builder.ToMessageBody();
        }

        private string ObtenerPlantillaCorreo(string plantillaPath)
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string fullPath = Path.Combine(baseDir, plantillaPath);

                Console.WriteLine($"Base Directory: {baseDir}");
                Console.WriteLine($"Full Path: {fullPath}");

                if (File.Exists(fullPath))
                {
                    return File.ReadAllText(fullPath);
                }

                _logger.LogWarning("Plantilla no encontrada: {PlantillaPath}. Usando plantilla por defecto.", plantillaPath);
                return "<html><body><h1>{{titulo}}</h1><p>{{mensaje}}</p></body></html>";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al leer la plantilla de correo: {PlantillaPath}", plantillaPath);
                return "<html><body><h1>{{titulo}}</h1><p>{{mensaje}}</p></body></html>";
            }
        }

        private string ReemplazarVariablesPlantilla(
            string plantilla,
            Usuario usuario,
            Dictionary<string, string>? parametrosAdicionales)
        {
            string resultado = plantilla
                .Replace("{{nombreUsuario}}", usuario.Username)
                .Replace("{{nombre}}", usuario.Username)
                .Replace("{{correoElectronico}}", usuario.EmailPersonal)
                .Replace("{{fechaActual}}", DateTime.Now.ToString("dd/MM/yyyy"));

            string baseUrl = _configuration["Application:BaseUrl"] ?? "https://gestionacademica.com";
            resultado = resultado.Replace("{{baseUrl}}", baseUrl);

            if (parametrosAdicionales != null)
            {
                foreach (var param in parametrosAdicionales)
                {
                    resultado = resultado.Replace($"{{{{{param.Key}}}}}", param.Value);
                }
            }

            return resultado;
        }

        #endregion
    }
}