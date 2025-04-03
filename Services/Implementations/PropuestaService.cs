using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GestionAcademicaAPI.Services.Implementations
{
    public class PropuestaService : IPropuestaService
    {
        private readonly IPropuestaRepository _propuestaRepository;

        public PropuestaService(IPropuestaRepository propuestaRepository)
        {
            _propuestaRepository = propuestaRepository;
        }

        public async Task<Propuesta> AddAsync(Propuesta propuesta)
        {
            return await _propuestaRepository.AddAsync(propuesta);
        }

        public async Task<IEnumerable<Propuesta>> GetAllAsync()
        {
            return await _propuestaRepository.GetAllAsync();
        }

        public async Task<Propuesta?> GetByIdAsync(int id)
        {
            return await _propuestaRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Propuesta>> FindAsync(Expression<Func<Propuesta, bool>> predicate)
        {
            return await _propuestaRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Propuesta>> GetBySolicitudIdAsync(int idSolicitud)
        {
            return await _propuestaRepository.GetBySolicitudIdAsync(idSolicitud);
        }

        public async Task<IEnumerable<Propuesta>> GetByEscuelaIdAsync(int idEscuela)
        {
            return await _propuestaRepository.GetByEscuelaIdAsync(idEscuela);
        }

        public async Task<IEnumerable<Propuesta>> GetByStatusAsync(string status)
        {
            return await _propuestaRepository.GetByStatusAsync(status);
        }

        public async Task<IEnumerable<Propuesta>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _propuestaRepository.GetByDateRangeAsync(fechaInicio, fechaFin);
        }

        public async Task UpdateAsync(Propuesta propuesta)
        {
            await _propuestaRepository.UpdateAsync(propuesta);
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

        public async Task<Propuesta?> GetLastPropuestaBySolicitudAsync(int idSolicitud)
        {
            return await _propuestaRepository.GetLastPropuestaBySolicitudAsync(idSolicitud);
        }
    }
}
