using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Repositories
{
    public class EscuelaRepository : IEscuelaRepository
    {
        private readonly AppDbContext _context;

        public EscuelaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Escuela>> GetAllAsync()
        {
            return await _context.Set<Escuela>().ToListAsync();
        }

        public async Task<Escuela?> GetByIdAsync(int id)
        {
            return await _context.Set<Escuela>().FindAsync(id);
        }

        public async Task<IEnumerable<Escuela>> FindAsync(Expression<Func<Escuela, bool>> predicate)
        {
            return await _context.Set<Escuela>().Where(predicate).ToListAsync();
        }

        public async Task<Escuela?> GetByNombreAsync(string nombre)
        {
            return await _context.Set<Escuela>().FirstOrDefaultAsync(e => e.Nombre == nombre);
        }

        public async Task<Escuela> AddAsync(Escuela escuela)
        {
            _context.Set<Escuela>().Add(escuela);
            await _context.SaveChangesAsync();
            return escuela;
        }

        public async Task UpdateAsync(Escuela escuela)
        {
            _context.Entry(escuela).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var escuela = await _context.Set<Escuela>().FindAsync(id);
            if (escuela != null)
            {
                _context.Set<Escuela>().Remove(escuela);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByNombreAsync(string nombre)
        {
            return await _context.Set<Escuela>().AnyAsync(e => e.Nombre == nombre);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<Escuela>().CountAsync();
        }

        public async Task<IEnumerable<Escuela>> GetEscuelasWithPropostasAsync()
        {
            return await _context.Set<Escuela>().Include(e => e.Propuestas).ToListAsync();
        }
    }
}
