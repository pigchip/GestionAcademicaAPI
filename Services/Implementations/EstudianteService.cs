using GestionAcademicaAPI.Dtos;
using GestionAcademicaAPI.DTOs;
using GestionAcademicaAPI.Helpers;
using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.Services.Implementations
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<EstudianteService> _logger;

        public EstudianteService(
            IEstudianteRepository estudianteRepository,
            IUsuarioRepository usuarioRepository,
            ILogger<EstudianteService> logger)
        {
            _estudianteRepository = estudianteRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        public async Task<EstudianteDTO> AddAsync(EstudianteDTO dto)
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

            // Map Usuario entity to Estudiante entity
            var estudianteEntity = new Estudiante
            {
                IdUsuario = savedUsuario.Id,
                Usuario = savedUsuario,
                Nombre = dto.Nombre,
                ApellidoPat = dto.ApellidoPat,
                ApellidoMat = dto.ApellidoMat,
                EmailEscolar = dto.EmailEscolar,
                Boleta = dto.Boleta,
                Carrera = dto.Carrera
            };

            // Save the Estudiante entity
            var savedEstudiante = await _estudianteRepository.AddAsync(estudianteEntity);

            // Create a response DTO from the saved entity
            var response = new EstudianteDTO
            {
                IdUsuario = savedEstudiante.IdUsuario,
                Username = savedEstudiante.Usuario.Username,
                EmailPersonal = savedEstudiante.Usuario.EmailPersonal,
                Password = savedEstudiante.Usuario.Password,
                Nombre = savedEstudiante.Nombre,
                ApellidoPat = savedEstudiante.ApellidoPat,
                ApellidoMat = savedEstudiante.ApellidoMat,
                EmailEscolar = savedEstudiante.EmailEscolar,
                Boleta = savedEstudiante.Boleta,
                Carrera = savedEstudiante.Carrera
            };

            // Return the fully populated DTO
            return response;
        }

        public async Task<Estudiante?> GetByIdAsync(int id)
        {
            var entity = await _estudianteRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Estudiante with ID {id} not found.");
            }

            var usuario = await _usuarioRepository.ObtenerPorIdAsync(entity.IdUsuario);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"User with ID {entity.IdUsuario} not found.");
            }

            return new Estudiante
            {
                Id = entity.Id,
                IdUsuario = entity.IdUsuario,
                Usuario = new Usuario
                {
                    Username = usuario.Username,
                    EmailPersonal = usuario.EmailPersonal,
                    Password = usuario.Password
                },
                Nombre = entity.Nombre,
                ApellidoPat = entity.ApellidoPat,
                ApellidoMat = entity.ApellidoMat,
                EmailEscolar = entity.EmailEscolar,
                Boleta = entity.Boleta,
                Carrera = entity.Carrera
            };
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            var entities = await _estudianteRepository.GetAllAsync();
            if (!entities.Any())
            {
                throw new KeyNotFoundException("No students found.");
            }

            var estudianteDTOs = new List<Estudiante>();

            foreach (var entity in entities)
            {
                var usuario = await _usuarioRepository.ObtenerPorIdAsync(entity.IdUsuario);
                if (usuario == null)
                {
                    throw new KeyNotFoundException($"User with ID {entity.IdUsuario} not found.");
                }

                var estudianteDTO = new Estudiante
                {
                    Id = entity.Id,
                    IdUsuario = entity.IdUsuario,
                    Usuario = new Usuario
                    {
                        Username = usuario.Username,
                        EmailPersonal = usuario.EmailPersonal,
                        Password = usuario.Password
                    },
                    Nombre = entity.Nombre,
                    ApellidoPat = entity.ApellidoPat,
                    ApellidoMat = entity.ApellidoMat,
                    EmailEscolar = entity.EmailEscolar,
                    Boleta = entity.Boleta,
                    Carrera = entity.Carrera
                };

                estudianteDTOs.Add(estudianteDTO);
            }

            return estudianteDTOs;
        }

        public async Task<Estudiante?> GetByUserIdAsync(int idUsuario)
        {
            var entity = await _estudianteRepository.GetByUserIdAsync(idUsuario);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Estudiante with User ID {idUsuario} not found.");
            }

            return new Estudiante
            {
                Id = entity.Id,
                IdUsuario = entity.IdUsuario,
                Usuario = new Usuario
                {
                    Username = entity.Usuario.Username,
                    EmailPersonal = entity.Usuario.EmailPersonal,
                    Password = entity.Usuario.Password
                },
                Nombre = entity.Nombre,
                ApellidoPat = entity.ApellidoPat,
                ApellidoMat = entity.ApellidoMat,
                EmailEscolar = entity.EmailEscolar,
                Boleta = entity.Boleta,
                Carrera = entity.Carrera
            };
        }

        public async Task<EstudianteDTO> UpdateAsync(EstudianteDTO dto)
        {
            // Validate the DTO
            var validationContext = new ValidationContext(dto);
            Validator.ValidateObject(dto, validationContext, validateAllProperties: true);

            // Simple approach: retrieve entity, update, and reuse existing logic
            var existing = await _estudianteRepository.GetByUserIdAsync(dto.IdUsuario);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Estudiante with ID {dto.IdUsuario} not found.");
            }

            // Use the UpdateAsync method from the repository
            var updatedEstudiante = await _estudianteRepository.UpdateAsync(new Estudiante
            {
                Id = existing.Id,
                Nombre = dto.Nombre,
                ApellidoPat = dto.ApellidoPat,
                ApellidoMat = dto.ApellidoMat,
                EmailEscolar = dto.EmailEscolar,
                Carrera = dto.Carrera,
                Boleta = dto.Boleta,
                IdUsuario = dto.IdUsuario,
                Usuario = new Usuario
                {
                    Username = dto.Username,
                    EmailPersonal = dto.EmailPersonal,
                    Password = dto.Password
                }
            });

            // Map the updated Estudiante entity to EstudianteDTO
            var responseEstudianteDTO = new EstudianteDTO
            {
                IdUsuario = updatedEstudiante.IdUsuario,
                Username = updatedEstudiante.Usuario.Username,
                EmailPersonal = updatedEstudiante.Usuario.EmailPersonal,
                Password = updatedEstudiante.Usuario.Password,
                Nombre = updatedEstudiante.Nombre,
                ApellidoPat = updatedEstudiante.ApellidoPat,
                ApellidoMat = updatedEstudiante.ApellidoMat,
                EmailEscolar = updatedEstudiante.EmailEscolar,
                Boleta = updatedEstudiante.Boleta,
                Carrera = updatedEstudiante.Carrera
            };

            // Return the updated DTO
            return responseEstudianteDTO;
        }

        public async Task DeleteAsync(int id)
        {
            var search = await _estudianteRepository.GetByIdAsync(id);
            if (search == null)
            {
                throw new KeyNotFoundException($"Estudiante with ID {id} not found.");
            }

            await _estudianteRepository.DeleteAsync(id);
            await _usuarioRepository.EliminarAsync(search.IdUsuario);
        }

        /// <summary>
        /// Verifica si existe un estudiante con la boleta proporcionada
        /// </summary>
        /// <param name="boleta">Número de boleta</param>
        /// <returns>True si existe, False en caso contrario</returns>
        public async Task<bool> ExistsByBoletaAsync(int boleta)
        {
            return await _estudianteRepository.ExistsByBoletaAsync(boleta);
        }

        /// <summary>
        /// Verifica si existe un estudiante para un usuario específico
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>True si existe, False en caso contrario</returns>
        public async Task<bool> ExistsForUserAsync(int idUsuario)
        {
            return await _estudianteRepository.ExistsForUserAsync(idUsuario);
        }

        /// <summary>
        /// Obtiene un estudiante por su número de boleta
        /// </summary>
        /// <param name="boleta">Número de boleta del estudiante</param>
        /// <returns>Estudiante encontrado o null</returns>
        public async Task<Estudiante?> GetByBoletaAsync(int boleta)
        {
            return await _estudianteRepository.GetByBoletaAsync(boleta);
        }

        /// <summary>
        /// Obtiene un estudiante por su carrera
        /// </summary>
        /// <param name="carrera">Carrera de estudiante</param>
        /// <returns>Estudiante encontrado o null</returns>
        public async Task<IEnumerable<Estudiante>> GetByCarreraAsync(string carrera)
        {
            return await _estudianteRepository.GetByCarreraAsync(carrera);
        }
    }
}
