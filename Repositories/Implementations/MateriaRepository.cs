using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Repositories.Implementations
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly AppDbContext _context;

        public MateriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Materia>> GetAllAsync()
        {
            return await _context.Set<Materia>().ToListAsync();
        }

        public async Task<Materia?> GetByIdAsync(int id)
        {
            return await _context.Set<Materia>().FindAsync(id);
        }

        public async Task<IEnumerable<Materia>> FindAsync(Expression<Func<Materia, bool>> predicate)
        {
            return await _context.Set<Materia>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Materia>> GetByEstudianteIdAsync(int idEstudiante)
        {
            return await _context.Set<Materia>().Where(m => m.IdEstudiante == idEstudiante).ToListAsync();
        }

        public async Task<IEnumerable<Materia>> GetByNombreMateriaEscomAsync(string nombreMateriaEscom)
        {
            return await _context.Set<Materia>().Where(m => m.NombreMateriaEscom == nombreMateriaEscom).ToListAsync();
        }

        public async Task<IEnumerable<Materia>> GetByNombreMateriaForaneaAsync(string nombreMateriaForanea)
        {
            return await _context.Set<Materia>().Where(m => m.NombreMateriaForanea == nombreMateriaForanea).ToListAsync();
        }

        public async Task<IEnumerable<Materia>> GetByStatusAsync(string status)
        {
            return await _context.Set<Materia>().Where(m => m.Status == status).ToListAsync();
        }

        public async Task<Materia> AddAsync(Materia materia)
        {
            _context.Set<Materia>().Add(materia);
            await _context.SaveChangesAsync();
            return materia;
        }

        public async Task UpdateAsync(Materia materia)
        {
            _context.Set<Materia>().Update(materia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var materia = await _context.Set<Materia>().FindAsync(id);
            if (materia != null)
            {
                _context.Set<Materia>().Remove(materia);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CountByEstudianteAsync(int idEstudiante)
        {
            return await _context.Set<Materia>().CountAsync(m => m.IdEstudiante == idEstudiante);
        }

        public async Task<int> CountByStatusAsync(string status)
        {
            return await _context.Set<Materia>().CountAsync(m => m.Status == status);
        }

        public async Task<bool> ExistsByNombreMateriaEscomAsync(string nombreMateriaEscom)
        {
            return await _context.Set<Materia>().AnyAsync(m => m.NombreMateriaEscom == nombreMateriaEscom);
        }

        public async Task<bool> ExistsByNombreMateriaForaneaAsync(string nombreMateriaForanea)
        {
            return await _context.Set<Materia>().AnyAsync(m => m.NombreMateriaForanea == nombreMateriaForanea);
        }
    }
}
