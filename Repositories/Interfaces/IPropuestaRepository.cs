using GestionAcademicaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GestionAcademicaAPI.Repositories.Interfaces
{
    public interface IPropuestaRepository
    {
        Task<IEnumerable<Propuesta>> GetAllAsync();
        Task<Propuesta?> GetByIdAsync(int id);
        Task<IEnumerable<Propuesta>> FindAsync(Expression<Func<Propuesta, bool>> predicate);
        Task<IEnumerable<Propuesta>> GetBySolicitudIdAsync(int idSolicitud);
        Task<IEnumerable<Propuesta>> GetByEscuelaIdAsync(int idEscuela);
        Task<IEnumerable<Propuesta>> GetByStatusAsync(string status);
        Task<IEnumerable<Propuesta>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin);
        Task UpdateAsync(Propuesta propuesta);
        Task DeleteAsync(int id);
        Task<int> CountBySolicitudAsync(int idSolicitud);
        Task<int> CountByEscuelaAsync(int idEscuela);
        Task<int> CountByStatusAsync(string status);
        Task<Propuesta?> GetLastPropuestaBySolicitudAsync(int idSolicitud);
    }
}