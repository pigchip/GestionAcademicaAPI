using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;

namespace GestionAcademicaAPI.Repositories.Implementations
{
    public class SolicitudRepository : ISolicitudRepository
    {
        private readonly AppDbContext _context;

        public SolicitudRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Solicitud> AddAsync(Solicitud solicitud)
        {
            _context.Solicitudes.Add(solicitud);
            await _context.SaveChangesAsync();
            return solicitud;
        }

        public async Task<IEnumerable<Solicitud>> GetAllAsync()
        {
            return await _context.Solicitudes
                .Include(s => s.Propuestas)
                    .ThenInclude(p => p.Escuela)
                .Include(s => s.Propuestas)
                    .ThenInclude(p => p.Materias)
                .ToListAsync();
        }

        public async Task<Solicitud?> GetByIdAsync(int id)
        {
            return await _context.Solicitudes
                .Include(s => s.Propuestas)
                    .ThenInclude(p => p.Escuela)
                .Include(s => s.Propuestas)
                    .ThenInclude(p => p.Materias)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Solicitud>> GetByEstudianteIdAsync(int idEstudiante)
        {
            return await _context.Solicitudes
                .Include(s => s.Propuestas)
                    .ThenInclude(p => p.Escuela)
                .Include(s => s.Propuestas)
                    .ThenInclude(p => p.Materias)
                .Where(s => s.IdEstudiante == idEstudiante)
                .ToListAsync();
        }

        public async Task<IEnumerable<Solicitud>> GetByStatusAsync(string status)
        {
            return await _context.Solicitudes
                .Include(s => s.Propuestas)
                    .ThenInclude(p => p.Escuela)
                .Include(s => s.Propuestas)
                    .ThenInclude(p => p.Materias)
                .Where(s => s.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Solicitud>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Solicitudes
                .Include(s => s.Propuestas)
                    .ThenInclude(p => p.Escuela)
                .Include(s => s.Propuestas)
                    .ThenInclude(p => p.Materias)
                .Where(s => s.Fecha >= fechaInicio && s.Fecha <= fechaFin)
                .ToListAsync();
        }

        public async Task UpdateAsync(Solicitud solicitud)
        {
            _context.Entry(solicitud).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var solicitud = await _context.Solicitudes.FindAsync(id);
            if (solicitud != null)
            {
                _context.Solicitudes.Remove(solicitud);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CountByEstudianteAsync(int idEstudiante)
        {
            return await _context.Solicitudes.CountAsync(s => s.IdEstudiante == idEstudiante);
        }

        public async Task<int> CountByStatusAsync(string status)
        {
            return await _context.Solicitudes.CountAsync(s => s.Status == status);
        }

        public async Task<Solicitud?> GetLastSolicitudByEstudianteAsync(int idEstudiante)
        {
            return await _context.Solicitudes
                .Include(s => s.Propuestas)
                    .ThenInclude(p => p.Escuela)
                .Include(s => s.Propuestas)
                    .ThenInclude(p => p.Materias)
                .Where(s => s.IdEstudiante == idEstudiante)
                .OrderByDescending(s => s.Fecha)
                .FirstOrDefaultAsync();
        }
    }
}