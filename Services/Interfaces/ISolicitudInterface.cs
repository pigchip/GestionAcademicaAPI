using GestionAcademicaAPI.Dtos;
using GestionAcademicaAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionAcademicaAPI.Services.Interfaces
{
    public interface ISolicitudService
    {
        Task<SolicitudResponseDto> AddAsyncSolicitud(SolicitudRequestDto solicitudDto);
        Task<SolicitudResponseDto> AddAsync(Solicitud solicitud);
        Task<IEnumerable<SolicitudResponseDto>> GetAllAsync();
        Task<SolicitudResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<SolicitudResponseDto>> GetByEstudianteIdAsync(int idEstudiante);
        Task<IEnumerable<SolicitudResponseDto>> GetByStatusAsync(string status);
        Task<IEnumerable<SolicitudResponseDto>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<SolicitudResponseDto> UpdateAsync(SolicitudUpdateDto solicitudDto);
        Task DeleteAsync(int id);
        Task<int> CountByEstudianteAsync(int idEstudiante);
        Task<int> CountByStatusAsync(string status);
        Task<SolicitudResponseDto?> GetLastSolicitudByEstudianteAsync(int idEstudiante);
    }
}