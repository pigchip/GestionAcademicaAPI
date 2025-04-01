using GestionAcademicaAPI.DTOs;
using GestionAcademicaAPI.Helpers;
using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.Services.Implementations
{
    public class AdministradorService : IAdministradorService
    {
        private readonly IAdministradorRepository _administradorRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<AdministradorService> _logger;

        public AdministradorService(
            IAdministradorRepository administradorRepository,
            IUsuarioRepository usuarioRepository,
            ILogger<AdministradorService> logger)
        {
            _administradorRepository = administradorRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        public async Task<AdministradorDTO> AddAsync(UsuarioDTO dto)
        {
            // Validate the DTO
            var validationContext = new ValidationContext(dto);
            Validator.ValidateObject(dto, validationContext, validateAllProperties: true);

            // Map DTO to Usuario entity
            var usuarioEntity = new Usuario
            {
                Username = dto.Username,
                EmailPersonal = dto.EmailPersonal,
                Password = Helpers.Helpers.HashPassword(dto.Password)
            };

            // Save the Usuario entity
            var savedUsuario = await _usuarioRepository.CrearAsync(usuarioEntity);

            // Map Usuario entity to Administrador entity
            var administradorEntity = new Administrador
            {
                IdUsuario = savedUsuario.Id,
                Usuario = savedUsuario
            };

            // Save the Administrador entity
            var savedAdministrador = await _administradorRepository.AddAsync(administradorEntity);

            // Create a response DTO from the saved entity
            var response = new AdministradorDTO
            {
                IdUsuario = savedAdministrador.IdUsuario,
                Usuario = new UsuarioDTO
                {
                    Username = savedAdministrador.Usuario.Username,
                    EmailPersonal = savedAdministrador.Usuario.EmailPersonal,
                    Password = savedAdministrador.Usuario.Password
                }
            };

            // Return the fully populated DTO
            return response;
        }

        public async Task<Administrador?> GetByIdAsync(int id)
        {
            var entity = await _administradorRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Administrador with ID {id} not found.");
            }

            var usuario = await _usuarioRepository.ObtenerPorIdAsync(entity.IdUsuario);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"User with ID {entity.IdUsuario} not found.");
            }

            return new Administrador
            {
                Id = entity.Id,
                IdUsuario = entity.IdUsuario,
                Usuario = new Usuario
                {
                    Username = usuario.Username,
                    EmailPersonal = usuario.EmailPersonal,
                    Password = usuario.Password
                }
            };
        }

        public async Task<IEnumerable<Administrador>> GetAllAsync()
        {
            var entities = await _administradorRepository.GetAllAsync();
            if (!entities.Any())
            {
                throw new KeyNotFoundException("No administrators found.");
            }

            var administradorDTOs = new List<Administrador>();

            foreach (var entity in entities)
            {
                var usuario = await _usuarioRepository.ObtenerPorIdAsync(entity.IdUsuario);
                if (usuario == null)
                {
                    throw new KeyNotFoundException($"User with ID {entity.IdUsuario} not found.");
                }

                var administradorDTO = new Administrador
                {
                    Id = entity.Id,
                    IdUsuario = entity.IdUsuario,
                    Usuario = new Usuario
                    {
                        Username = usuario.Username,
                        EmailPersonal = usuario.EmailPersonal,
                        Password = usuario.Password
                    }
                };

                administradorDTOs.Add(administradorDTO);
            }

            return administradorDTOs;
        }

        public async Task<Administrador?> GetByUserIdAsync(int idUsuario)
        {
            var entity = await _administradorRepository.GetByUserIdAsync(idUsuario);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Administrador with User ID {idUsuario} not found.");
            }

            return new Administrador
            {
                Id = entity.Id,
                IdUsuario = entity.IdUsuario,
                Usuario = new Usuario
                {
                    Username = entity.Usuario.Username,
                    EmailPersonal = entity.Usuario.EmailPersonal,
                    Password = entity.Usuario.Password
                }
            };
        }

        public async Task<AdministradorDTO> UpdateAsync(AdministradorDTO dto)
        {
            // Simple approach: retrieve entity, update, and reuse existing logic
            var existing = await _administradorRepository.GetByIdAsync(dto.IdUsuario);
            if (existing == null) throw new KeyNotFoundException();

            // Check if the new username is a duplicate
            if (await _usuarioRepository.ExisteNombreUsuarioAsync(dto.Usuario.Username, dto.IdUsuario))
            {
                throw new InvalidOperationException("The username is already taken.");
            }

            // Check if the new email is a duplicate
            if (await _usuarioRepository.ExisteCorreoElectronicoAsync(dto.Usuario.EmailPersonal, dto.IdUsuario))
            {
                throw new InvalidOperationException("The email is already taken.");
            }

            existing.Usuario.Username = dto.Usuario.Username;               // New username
            existing.Usuario.EmailPersonal = dto.Usuario.EmailPersonal;     // New email
            existing.Usuario.Password = dto.Usuario.Password;               // New password

            // Use the UpdateAsync method from the repository
            var updated = await _administradorRepository.UpdateAsync(new ActualizarAdminRequest
            {
                Id = existing.Id,
                NuevoUsername = existing.Usuario.Username,
                NuevaPassword = existing.Usuario.Password,
                NuevoEmailPersonal = existing.Usuario.EmailPersonal
            });

            return new AdministradorDTO
            {
                IdUsuario = updated.IdUsuario,
                Usuario = new UsuarioDTO
                {
                    Username = updated.Usuario.Username,
                    EmailPersonal = updated.Usuario.EmailPersonal,
                    Password = updated.Usuario.Password
                }
            };
        }

        public async Task DeleteAsync(int id)
        {
            var search = await _administradorRepository.GetByIdAsync(id);
            if (search == null)
            {
                throw new KeyNotFoundException($"Administrador with ID {id} not found.");
            }

            await _administradorRepository.DeleteAsync(id);
            await _usuarioRepository.EliminarAsync(search.IdUsuario);
        }
    }
}
