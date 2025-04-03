using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;

namespace GestionAcademicaAPI.Repositories.Implementations
{
    public class PropuestaMateriaRepository : IPropuestaMateriaRepository
    {
        private readonly AppDbContext _context;

        public PropuestaMateriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PropuestaMateria>> GetAllAsync()
        {
            return await _context.Set<PropuestaMateria>().ToListAsync();
        }

        public async Task<PropuestaMateria?> GetByIdAsync(int id)
        {
            return await _context.Set<PropuestaMateria>().FindAsync(id);
        }

        public async Task<IEnumerable<PropuestaMateria>> GetByPropuestaIdAsync(int idPropuesta)
        {
            return await _context.Set<PropuestaMateria>()
                .Where(pm => pm.IdPropuesta == idPropuesta)
                .ToListAsync();
        }

        public async Task<IEnumerable<PropuestaMateria>> GetByMateriaIdAsync(int idMateria)
        {
            return await _context.Set<PropuestaMateria>()
                .Where(pm => pm.IdMateria == idMateria)
                .ToListAsync();
        }

        public async Task<PropuestaMateria> AddAsync(PropuestaMateria propuestaMateria)
        {
            _context.Set<PropuestaMateria>().Add(propuestaMateria);
            await _context.SaveChangesAsync();
            return propuestaMateria;
        }

        public async Task<IEnumerable<PropuestaMateria>> AddRangeAsync(IEnumerable<PropuestaMateria> propuestaMaterias)
        {
            _context.Set<PropuestaMateria>().AddRange(propuestaMaterias);
            await _context.SaveChangesAsync();
            return propuestaMaterias;
        }

        public async Task DeleteAsync(int id)
        {
            var propuestaMateria = await _context.Set<PropuestaMateria>().FindAsync(id);
            if (propuestaMateria != null)
            {
                _context.Set<PropuestaMateria>().Remove(propuestaMateria);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteByPropuestaIdAsync(int idPropuesta)
        {
            var propuestaMaterias = await _context.Set<PropuestaMateria>()
                .Where(pm => pm.IdPropuesta == idPropuesta)
                .ToListAsync();
            _context.Set<PropuestaMateria>().RemoveRange(propuestaMaterias);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountMateriasByPropuestaAsync(int idPropuesta)
        {
            return await _context.Set<PropuestaMateria>()
                .CountAsync(pm => pm.IdPropuesta == idPropuesta);
        }

        public async Task<int> CountPropuestasByMateriaAsync(int idMateria)
        {
            return await _context.Set<PropuestaMateria>()
                .CountAsync(pm => pm.IdMateria == idMateria);
        }
    }
}
