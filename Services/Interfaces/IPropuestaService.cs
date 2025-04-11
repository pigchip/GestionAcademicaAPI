using GestionAcademicaAPI.Dtos;
using GestionAcademicaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GestionAcademicaAPI.Services.Interfaces
{
    public interface IPropuestaService
    {
        Task<IEnumerable<PropuestaInfoDto>> GetAllAsync();
        Task<PropuestaInfoDto?> GetByIdAsync(int id);
        Task<IEnumerable<PropuestaInfoDto>> FindAsync(Expression<Func<Propuesta, bool>> predicate);
        Task<IEnumerable<PropuestaInfoDto>> GetBySolicitudIdAsync(int idSolicitud);
        Task<IEnumerable<PropuestaInfoDto>> GetByEscuelaIdAsync(int idEscuela);
        Task<IEnumerable<PropuestaInfoDto>> GetByStatusAsync(string status);
        Task<IEnumerable<PropuestaInfoDto>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<PropuestaInfoDto> UpdateAsync(PropuestaUpdateDto propuestaDto);
        Task DeleteAsync(int id);
        Task<int> CountBySolicitudAsync(int idSolicitud);
        Task<int> CountByEscuelaAsync(int idEscuela);
        Task<int> CountByStatusAsync(string status);
        Task<PropuestaInfoDto?> GetLastPropuestaBySolicitudAsync(int idSolicitud);
    }
}