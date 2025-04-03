using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionAcademicaAPI.Services.Implementations
{
    public class SolicitudService : ISolicitudService
    {
        private readonly ISolicitudRepository _solicitudRepository;

        public SolicitudService(ISolicitudRepository solicitudRepository)
        {
            _solicitudRepository = solicitudRepository;
        }

        public async Task<Solicitud> AddAsync(Solicitud solicitud)
        {
            return await _solicitudRepository.AddAsync(solicitud);
        }

        public async Task<IEnumerable<Solicitud>> GetAllAsync()
        {
            return await _solicitudRepository.GetAllAsync();
        }

        public async Task<Solicitud?> GetByIdAsync(int id)
        {
            return await _solicitudRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Solicitud>> GetByEstudianteIdAsync(int idEstudiante)
        {
            return await _solicitudRepository.GetByEstudianteIdAsync(idEstudiante);
        }

        public async Task<IEnumerable<Solicitud>> GetByStatusAsync(string status)
        {
            return await _solicitudRepository.GetByStatusAsync(status);
        }

        public async Task<IEnumerable<Solicitud>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _solicitudRepository.GetByDateRangeAsync(fechaInicio, fechaFin);
        }

        public async Task UpdateAsync(Solicitud solicitud)
        {
            await _solicitudRepository.UpdateAsync(solicitud);
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

        public async Task<Solicitud?> GetLastSolicitudByEstudianteAsync(int idEstudiante)
        {
            return await _solicitudRepository.GetLastSolicitudByEstudianteAsync(idEstudiante);
        }
    }
}
