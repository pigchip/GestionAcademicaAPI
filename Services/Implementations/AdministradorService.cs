using GestionAcademicaAPI.Dtos;
using GestionAcademicaAPI.DTOs;
using GestionAcademicaAPI.Helpers;
using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.Services.Implementations
{
    public class AdministradorService : IAdministradorService
    {
        private readonly IAdministradorRepository _administradorRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<AdministradorService> _logger;
        private readonly AppDbContext _context;

        public AdministradorService(
            IAdministradorRepository administradorRepository,
            IUsuarioRepository usuarioRepository,
            ILogger<AdministradorService> logger,
            AppDbContext context)
        {
            _administradorRepository = administradorRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _context = context;
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
            // Validate the DTO
            var validationContext = new ValidationContext(dto);
            Validator.ValidateObject(dto, validationContext, validateAllProperties: true);

            // Usar una transacción para garantizar la integridad de los datos
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Retrieve the existing entity including Usuario to work with a single instance
                var existing = await _context.Administradores
                    .Include(e => e.Usuario)
                    .FirstOrDefaultAsync(e => e.IdUsuario == dto.IdUsuario);

                if (existing == null)
                {
                    throw new KeyNotFoundException($"Administrador with ID {dto.IdUsuario} not found.");
                }

                // Check if the new username already exists for a different user
                var existingUserWithSameUsername = await _usuarioRepository.ExisteNombreUsuarioAsync(dto.Usuario.Username, dto.IdUsuario);
                if (existingUserWithSameUsername)
                {
                    throw new InvalidOperationException($"Username '{dto.Usuario.Username}' is already taken.");
                }

                // Check if the new email already exists for a different user
                var existingUserWithSameEmail = await _usuarioRepository.ExisteCorreoElectronicoAsync(dto.Usuario.EmailPersonal, dto.IdUsuario);
                if (existingUserWithSameEmail)
                {
                    throw new InvalidOperationException($"Email '{dto.Usuario.EmailPersonal}' is already taken.");
                }

                // Actualizar las propiedades del Usuario existente (misma instancia)
                existing.Usuario.Username = dto.Usuario.Username;
                existing.Usuario.EmailPersonal = dto.Usuario.EmailPersonal;
                if (!string.IsNullOrEmpty(dto.Usuario.Password))
                {
                    existing.Usuario.Password = Helpers.Helpers.HashPassword(dto.Usuario.Password);
                }

                // Guardar todos los cambios en una sola operación
                await _context.SaveChangesAsync();

                // Confirmar la transacción
                await transaction.CommitAsync();

                // Mapear la entidad actualizada a DTO
                var responseDTO = new AdministradorDTO
                {
                    IdUsuario = existing.IdUsuario,
                    Usuario = new UsuarioDTO
                    {
                        Username = existing.Usuario.Username,
                        EmailPersonal = existing.Usuario.EmailPersonal,
                        Password = existing.Usuario.Password
                    }
                };

                return responseDTO;
            }
            catch (Exception)
            {
                // Si algo falla, revertir la transacción
                await transaction.RollbackAsync();
                throw;
            }
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
