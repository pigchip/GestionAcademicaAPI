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
            return await _context.Escuelas.ToListAsync();
        }

        public async Task<Escuela?> GetByIdAsync(int id)
        {
            return await _context.Escuelas.FindAsync(id);
        }

        public async Task<IEnumerable<Escuela>> FindAsync(Expression<Func<Escuela, bool>> predicate)
        {
            return await _context.Escuelas.Where(predicate).ToListAsync();
        }

        public async Task<Escuela?> GetByNombreAsync(string nombre)
        {
            return await _context.Escuelas.FirstOrDefaultAsync(e => e.Nombre == nombre);
        }

        public async Task<Escuela> AddAsync(Escuela escuela)
        {
            _context.Escuelas.Add(escuela);
            await _context.SaveChangesAsync();
            return escuela;
        }

        public async Task UpdateAsync(Escuela escuela)
        {
            var existingEscuela = await _context.Escuelas.FindAsync(escuela.Id);
            if (existingEscuela == null)
                throw new KeyNotFoundException($"Escuela con ID {escuela.Id} no encontrada.");
            _context.Entry(existingEscuela).CurrentValues.SetValues(escuela);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var escuela = await _context.Escuelas.FindAsync(id);
            if (escuela != null)
            {
                _context.Escuelas.Remove(escuela);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByNombreAsync(string nombre)
        {
            return await _context.Escuelas.AnyAsync(e => e.Nombre == nombre);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Escuelas.CountAsync();
        }

        public async Task<IEnumerable<Escuela>> GetEscuelasWithPropostasAsync()
        {
                return await _context.Set<Escuela>().Include(e => e.Propuestas).ToListAsync();
        }
    }
}