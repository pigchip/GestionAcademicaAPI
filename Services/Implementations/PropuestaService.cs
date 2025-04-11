using GestionAcademicaAPI.Dtos;
using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GestionAcademicaAPI.Services.Implementations
{
    public class PropuestaService : IPropuestaService
    {
        private readonly IPropuestaRepository _propuestaRepository;

        public PropuestaService(IPropuestaRepository propuestaRepository)
        {
            _propuestaRepository = propuestaRepository;
        }

        public async Task<IEnumerable<PropuestaInfoDto>> GetAllAsync()
        {
            var propuestas = await _propuestaRepository.GetAllAsync();
            return propuestas.Select(p => MapToResponseDto(p)).ToList();
        }

        public async Task<PropuestaInfoDto?> GetByIdAsync(int id)
        {
            var propuesta = await _propuestaRepository.GetByIdAsync(id);
            return propuesta != null ? MapToResponseDto(propuesta) : null;
        }

        public async Task<IEnumerable<PropuestaInfoDto>> FindAsync(Expression<Func<Propuesta, bool>> predicate)
        {
            var propuestas = await _propuestaRepository.FindAsync(predicate);
            return propuestas.Select(p => MapToResponseDto(p)).ToList();
        }

        public async Task<IEnumerable<PropuestaInfoDto>> GetBySolicitudIdAsync(int idSolicitud)
        {
            var propuestas = await _propuestaRepository.GetBySolicitudIdAsync(idSolicitud);
            return propuestas.Select(p => MapToResponseDto(p)).ToList();
        }

        public async Task<IEnumerable<PropuestaInfoDto>> GetByEscuelaIdAsync(int idEscuela)
        {
            var propuestas = await _propuestaRepository.GetByEscuelaIdAsync(idEscuela);
            return propuestas.Select(p => MapToResponseDto(p)).ToList();
        }

        public async Task<IEnumerable<PropuestaInfoDto>> GetByStatusAsync(string status)
        {
            var propuestas = await _propuestaRepository.GetByStatusAsync(status);
            return propuestas.Select(p => MapToResponseDto(p)).ToList();
        }

        public async Task<IEnumerable<PropuestaInfoDto>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var propuestas = await _propuestaRepository.GetByDateRangeAsync(fechaInicio, fechaFin);
            return propuestas.Select(p => MapToResponseDto(p)).ToList();
        }

        public async Task<PropuestaInfoDto> UpdateAsync(PropuestaUpdateDto propuestaDto)
        {
            try
            {
                var propuesta = await _propuestaRepository.GetByIdAsync(propuestaDto.IdPropuesta);
                if (propuesta == null)
                {
                    throw new ArgumentException($"La propuesta con ID {propuestaDto.IdPropuesta} no existe.");
                }

                propuesta.Status = propuestaDto.Status;
                await _propuestaRepository.UpdateAsync(propuesta);
                return MapToResponseDto(propuesta);
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Error al actualizar la propuesta en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error al procesar la actualización de la propuesta.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _propuestaRepository.DeleteAsync(id);
        }

        public async Task<int> CountBySolicitudAsync(int idSolicitud)
        {
            return await _propuestaRepository.CountBySolicitudAsync(idSolicitud);
        }

        public async Task<int> CountByEscuelaAsync(int idEscuela)
        {
            return await _propuestaRepository.CountByEscuelaAsync(idEscuela);
        }

        public async Task<int> CountByStatusAsync(string status)
        {
            return await _propuestaRepository.CountByStatusAsync(status);
        }

        public async Task<PropuestaInfoDto?> GetLastPropuestaBySolicitudAsync(int idSolicitud)
        {
            var propuesta = await _propuestaRepository.GetLastPropuestaBySolicitudAsync(idSolicitud);
            return propuesta != null ? MapToResponseDto(propuesta) : null;
        }

        private PropuestaInfoDto MapToResponseDto(Propuesta propuesta)
        {
            return new PropuestaInfoDto
            {
                Id = propuesta.Id,
                IdSolicitud = propuesta.IdSolicitud,
                NombreEscuela = propuesta.Escuela?.Nombre ?? "Desconocida",
                Status = propuesta.Status,
                Fecha = propuesta.Fecha,
                Materias = new MateriasPropuestaList
                {
                    Values = propuesta.Materias.Select(m => new MateriaInfoDto
                    {
                        Id = m.Id,
                        NombreMateriaEscom = m.NombreMateriaEscom,
                        NombreMateriaForanea = m.NombreMateriaForanea,
                        TemarioMateriaForaneaUrl = m.TemarioMateriaForaneaUrl,
                        Status = m.Status
                    }).ToList()
                }
            };
        }
    }
}