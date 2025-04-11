using GestionAcademicaAPI.Dtos;
using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations; // For attributes like [Required]
using System.ComponentModel.DataAnnotations.Schema; // For database-related attributes
using System.Collections.ObjectModel; // For collection types like ObservableCollection

namespace GestionAcademicaAPI.Services.Implementations
{
    public class SolicitudService : ISolicitudService
    {
        private readonly ISolicitudRepository _solicitudRepository;
        private readonly IEscuelaRepository _escuelaRepository;
        private readonly IPropuestaRepository _propuestaRepository;
        private readonly IMateriaRepository _materiaRepository;
        private readonly IEstudianteRepository _estudianteRepository;

        public SolicitudService(
            ISolicitudRepository solicitudRepository,
            IEscuelaRepository escuelaRepository,
            IPropuestaRepository propuestaRepository,
            IMateriaRepository materiaRepository,
            IEstudianteRepository estudianteRepository)
        {
            _solicitudRepository = solicitudRepository;
            _escuelaRepository = escuelaRepository;
            _propuestaRepository = propuestaRepository;
            _materiaRepository = materiaRepository;
            _estudianteRepository = estudianteRepository;
        }

        public async Task<SolicitudResponseDto> AddAsyncSolicitud(SolicitudRequestDto solicitudDto)
        {
            try
            {
                var estudiante = await _estudianteRepository.GetByIdAsync(solicitudDto.IdEstudiante);
                if (estudiante == null)
                {
                    throw new ArgumentException($"El estudiante con ID {solicitudDto.IdEstudiante} no existe.");
                }

                var escuela = await _escuelaRepository.GetByNombreAsync(solicitudDto.NombreEscuela);
                if (escuela == null)
                {
                    escuela = new Escuela { Nombre = solicitudDto.NombreEscuela };
                    await _escuelaRepository.AddAsync(escuela);
                }

                var solicitud = new Solicitud
                {
                    IdEstudiante = solicitudDto.IdEstudiante,
                    Status = "Pendiente",
                    Fecha = DateTime.UtcNow,
                    Propuestas = new List<Propuesta>()
                };

                foreach (var propuestaDto in solicitudDto.Solicitud)
                {
                    var propuesta = new Propuesta
                    {
                        IdSolicitud = solicitud.Id,
                        IdEscuela = escuela.Id,
                        Escuela = escuela,
                        Status = "Pendiente",
                        Fecha = DateTime.UtcNow,
                        Materias = new List<Materia>()
                    };

                    foreach (var materiaDto in propuestaDto.Propuesta)
                    {
                        var materia = new Materia
                        {
                            NombreMateriaEscom = materiaDto.NombreMateriaEscom,
                            NombreMateriaForanea = materiaDto.NombreMateriaForanea,
                            Status = "Pendiente",
                            IdEstudiante = solicitudDto.IdEstudiante,
                            IdPropuesta = propuesta.Id
                        };
                        propuesta.Materias.Add(materia);
                    }

                    solicitud.Propuestas.Add(propuesta);
                }

                var result = await _solicitudRepository.AddAsync(solicitud);
                return MapToResponseDto(result);
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Error al guardar la solicitud en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error al procesar la solicitud.", ex);
            }
        }

        public async Task<SolicitudResponseDto> AddAsync(Solicitud solicitud)
        {
            var result = await _solicitudRepository.AddAsync(solicitud);
            return MapToResponseDto(result);
        }

        public async Task<IEnumerable<SolicitudResponseDto>> GetAllAsync()
        {
            var solicitudes = await _solicitudRepository.GetAllAsync();
            return solicitudes.Select(s => MapToResponseDto(s)).ToList();
        }

        public async Task<SolicitudResponseDto?> GetByIdAsync(int id)
        {
            var solicitud = await _solicitudRepository.GetByIdAsync(id);
            return solicitud != null ? MapToResponseDto(solicitud) : null;
        }

        public async Task<IEnumerable<SolicitudResponseDto>> GetByEstudianteIdAsync(int idEstudiante)
        {
            var solicitudes = await _solicitudRepository.GetByEstudianteIdAsync(idEstudiante);
            return solicitudes.Select(s => MapToResponseDto(s)).ToList();
        }

        public async Task<IEnumerable<SolicitudResponseDto>> GetByStatusAsync(string status)
        {
            var solicitudes = await _solicitudRepository.GetByStatusAsync(status);
            return solicitudes.Select(s => MapToResponseDto(s)).ToList();
        }

        public async Task<IEnumerable<SolicitudResponseDto>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var solicitudes = await _solicitudRepository.GetByDateRangeAsync(fechaInicio, fechaFin);
            return solicitudes.Select(s => MapToResponseDto(s)).ToList();
        }

        public async Task<SolicitudResponseDto> UpdateAsync(SolicitudUpdateDto solicitudDto)
        {
            try
            {
                var solicitud = await _solicitudRepository.GetByIdAsync(solicitudDto.IdSolicitud);
                if (solicitud == null)
                {
                    throw new ArgumentException($"La solicitud con ID {solicitudDto.IdSolicitud} no existe.");
                }

                // Actualizar solo el campo Status
                solicitud.Status = solicitudDto.Status;

                await _solicitudRepository.UpdateAsync(solicitud);
                return MapToResponseDto(solicitud);
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Error al actualizar la solicitud en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error al procesar la actualización de la solicitud.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _solicitudRepository.DeleteAsync(id);
        }

        public async Task<int> CountByEstudianteAsync(int idEstudiante)
        {
            return await _solicitudRepository.CountByEstudianteAsync(idEstudiante);
        }

        public async Task<int> CountByStatusAsync(string status)
        {
            return await _solicitudRepository.CountByStatusAsync(status);
        }

        public async Task<SolicitudResponseDto?> GetLastSolicitudByEstudianteAsync(int idEstudiante)
        {
            var solicitud = await _solicitudRepository.GetLastSolicitudByEstudianteAsync(idEstudiante);
            return solicitud != null ? MapToResponseDto(solicitud) : null;
        }

        private SolicitudResponseDto MapToResponseDto(Solicitud solicitud)
        {
            return new SolicitudResponseDto
            {
                Id = solicitud.Id,
                IdEstudiante = solicitud.IdEstudiante,
                Status = solicitud.Status,
                Fecha = solicitud.Fecha,
                Propuestas = new PropuestaList
                {
                    Values = solicitud.Propuestas.Select(p => new PropuestaResponseDto
                    {
                        Id = p.Id,
                        NombreEscuela = p.Escuela?.Nombre ?? "Desconocida",
                        Status = p.Status,
                        Fecha = p.Fecha,
                        Materias = new MateriaList
                        {
                            Values = p.Materias.Select(m => new MateriaResponseDto
                            {
                                Id = m.Id,
                                NombreMateriaEscom = m.NombreMateriaEscom,
                                NombreMateriaForanea = m.NombreMateriaForanea,
                                TemarioMateriaForaneaUrl = m.TemarioMateriaForaneaUrl,
                                Status = m.Status
                            }).ToList()
                        }
                    }).ToList()
                },
                Comentarios = new ComentarioList // Agregar comentarios
                {
                    Values = solicitud.Comentarios.Select(c => new ComentarioResponseDTO
                    {
                        Id = c.Id,
                        Contenido = c.Contenido,
                        IdSolicitud = c.IdSolicitud,
                        IdUsuario = c.IdUsuario,
                        Fecha = c.Fecha
                    }).ToList()
                }
            };
        }
    }
}