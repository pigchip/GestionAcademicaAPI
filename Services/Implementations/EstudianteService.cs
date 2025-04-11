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
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AppDbContext _context;

        public EstudianteService(
            IEstudianteRepository estudianteRepository,
            IUsuarioRepository usuarioRepository,
            AppDbContext context)
        {
            _estudianteRepository = estudianteRepository;
            _usuarioRepository = usuarioRepository;
            _context = context;
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
                InePdf = entity.InePdf,
                Nombre = entity.Nombre,
                ApellidoPat = entity.ApellidoPat,
                ApellidoMat = entity.ApellidoMat,
                EmailEscolar = entity.EmailEscolar,
                Boleta = entity.Boleta,
                Carrera = entity.Carrera
            };
        }

        public async Task<Estudiante> GetByCredentials(string username, string password)
        {
            var usuario = await _usuarioRepository.AutenticarAsync(username, password);
            if (usuario == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            var estudiante = await _estudianteRepository.GetByUserIdAsync(usuario.Id);
            if (estudiante == null)
            {
                throw new KeyNotFoundException("Student not found.");
            }
            return new Estudiante
            {
                Id = estudiante.Id,
                IdUsuario = estudiante.IdUsuario,
                Usuario = new Usuario
                {
                    Username = usuario.Username,
                    EmailPersonal = usuario.EmailPersonal,
                    Password = usuario.Password
                },
                Nombre = estudiante.Nombre,
                ApellidoPat = estudiante.ApellidoPat,
                ApellidoMat = estudiante.ApellidoMat,
                EmailEscolar = estudiante.EmailEscolar,
                Boleta = estudiante.Boleta,
                Carrera = estudiante.Carrera
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

        /// <summary>
        /// Obtiene todos los estudiantes con sus datos relacionados (Usuario, Solicitudes, Materias)
        /// </summary>
        /// <returns>IEnumerable de estudiantes con todas sus entidades relacionadas</returns>
        public async Task<IEnumerable<EstudianteDetalladoDTO>> GetAllDetailAsync()
        {
            // Utilizar el método del repositorio que carga todas las entidades relacionadas
            var entities = await _estudianteRepository.GetAllWithDetailsAsync();
            if (!entities.Any())
            {
                throw new KeyNotFoundException("No students found.");
            }

            // Mapear las entidades a DTOs para evitar ciclos de referencia
            var estudiantesDTOs = new List<EstudianteDetalladoDTO>();

            foreach (var entity in entities)
            {
                var estudianteDTO = new EstudianteDetalladoDTO
                {
                    Id = entity.Id,
                    Nombre = entity.Nombre,
                    ApellidoPat = entity.ApellidoPat,
                    ApellidoMat = entity.ApellidoMat,
                    EmailEscolar = entity.EmailEscolar,
                    InePdf = entity.InePdf,
                    Boleta = entity.Boleta,
                    Carrera = entity.Carrera,
                    IdUsuario = entity.IdUsuario,
                    Usuario = new UsuarioInfoDTO
                    {
                        Id = entity.Usuario.Id,
                        Username = entity.Usuario.Username,
                        EmailPersonal = entity.Usuario.EmailPersonal
                    }
                };

                // Mapear las solicitudes
                if (entity.Solicitudes != null)
                {
                    foreach (var solicitud in entity.Solicitudes)
                    {
                        var solicitudDTO = new SolicitudInfoDTO
                        {
                            Id = solicitud.Id,
                            Status = solicitud.Status,
                            Fecha = solicitud.Fecha
                        };

                        // Mapear las propuestas de cada solicitud
                        if (solicitud.Propuestas != null)
                        {
                            foreach (var propuesta in solicitud.Propuestas)
                            {
                                var propuestaDTO = new PropuestaInfoDTO
                                {
                                    Id = propuesta.Id,
                                    Status = propuesta.Status,
                                    Fecha = propuesta.Fecha,
                                    Escuela = propuesta.Escuela != null ? new EscuelaInfoDTO
                                    {
                                        Id = propuesta.Escuela.Id,
                                        Nombre = propuesta.Escuela.Nombre
                                    } : null
                                };

                                // Mapear las materias de cada propuesta
                                if (propuesta.Materias != null)
                                {
                                    foreach (var materia in propuesta.Materias)
                                    {
                                        propuestaDTO.Materias.Add(new MateriaInfoDTO
                                        {
                                            Id = materia.Id,
                                            NombreMateriaEscom = materia.NombreMateriaEscom,
                                            NombreMateriaForanea = materia.NombreMateriaForanea,
                                            TemarioMateriaForaneaUrl = materia.TemarioMateriaForaneaUrl,
                                            Status = materia.Status
                                        });
                                    }
                                }

                                solicitudDTO.Propuestas.Add(propuestaDTO);
                            }
                        }

                        // Mapear los comentarios de cada solicitud
                        if (solicitud.Comentarios != null)
                        {
                            foreach (var comentario in solicitud.Comentarios)
                            {
                                var comentarioDTO = new ComentarioInfoDTO
                                {
                                    Id = comentario.Id,
                                    Contenido = comentario.Contenido,
                                    Fecha = comentario.Fecha
                                };

                                // Mapear usuario del comentario
                                if (comentario.Usuario != null)
                                {
                                    comentarioDTO.Usuario = new UsuarioInfoDTO
                                    {
                                        Id = comentario.Usuario.Id,
                                        Username = comentario.Usuario.Username,
                                        EmailPersonal = comentario.Usuario.EmailPersonal
                                    };
                                }

                                solicitudDTO.Comentarios.Add(comentarioDTO);
                            }
                        }

                        estudianteDTO.Solicitudes.Add(solicitudDTO);
                    }
                }

                // Mapear las materias del estudiante
                if (entity.Materias != null)
                {
                    foreach (var materia in entity.Materias)
                    {
                        estudianteDTO.Materias.Add(new MateriaInfoDTO
                        {
                            Id = materia.Id,
                            NombreMateriaEscom = materia.NombreMateriaEscom,
                            NombreMateriaForanea = materia.NombreMateriaForanea,
                            TemarioMateriaForaneaUrl = materia.TemarioMateriaForaneaUrl,
                            Status = materia.Status
                        });
                    }
                }

                estudiantesDTOs.Add(estudianteDTO);
            }

            return estudiantesDTOs;
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

            // Usar una transacción para garantizar la integridad de los datos
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Retrieve the existing entity including Usuario to work with a single instance
                var existing = await _context.Estudiantes
                    .Include(e => e.Usuario)
                    .FirstOrDefaultAsync(e => e.IdUsuario == dto.IdUsuario);

                if (existing == null)
                {
                    throw new KeyNotFoundException($"Estudiante with ID {dto.IdUsuario} not found.");
                }

                // Check if the new username already exists for a different user
                var existingUserWithSameUsername = await _usuarioRepository.ExisteNombreUsuarioAsync(dto.Username, dto.IdUsuario);
                if (existingUserWithSameUsername)
                {
                    throw new InvalidOperationException($"Username '{dto.Username}' is already taken.");
                }

                // Check if the new email already exists for a different user
                var existingUserWithSameEmail = await _usuarioRepository.ExisteCorreoElectronicoAsync(dto.EmailPersonal, dto.IdUsuario);
                if (existingUserWithSameEmail)
                {
                    throw new InvalidOperationException($"Email '{dto.EmailPersonal}' is already taken.");
                }

                // Actualizar las propiedades del Usuario existente (misma instancia)
                existing.Usuario.Username = dto.Username;
                existing.Usuario.EmailPersonal = dto.EmailPersonal;
                if (!string.IsNullOrEmpty(dto.Password))
                {
                    existing.Usuario.Password = Helpers.Helpers.HashPassword(dto.Password);
                }

                // Actualizar las propiedades del Estudiante
                existing.Nombre = dto.Nombre;
                existing.InePdf = dto.InePdf;
                existing.ApellidoPat = dto.ApellidoPat;
                existing.ApellidoMat = dto.ApellidoMat;
                existing.EmailEscolar = dto.EmailEscolar;
                existing.Boleta = dto.Boleta;
                existing.Carrera = dto.Carrera;

                // Guardar todos los cambios en una sola operación
                await _context.SaveChangesAsync();

                // Confirmar la transacción
                await transaction.CommitAsync();

                // Mapear la entidad actualizada a DTO
                var responseDTO = new EstudianteDTO
                {
                    IdUsuario = existing.IdUsuario,
                    Username = existing.Usuario.Username,
                    EmailPersonal = existing.Usuario.EmailPersonal,
                    InePdf = existing.InePdf,
                    Password = null, // Por seguridad, no devolvemos la contraseña
                    Nombre = existing.Nombre,
                    ApellidoPat = existing.ApellidoPat,
                    ApellidoMat = existing.ApellidoMat,
                    EmailEscolar = existing.EmailEscolar,
                    Boleta = existing.Boleta,
                    Carrera = existing.Carrera
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

        /// <inheritdoc/>
        public async Task<bool> ExisteCorreoElectronicoPersonalAsync(string correoElectronico, int? idExcluir = null)
        {
            return await _estudianteRepository.ExisteCorreoElectronicoPersonalAsync(correoElectronico, idExcluir);
        }
    }
}
