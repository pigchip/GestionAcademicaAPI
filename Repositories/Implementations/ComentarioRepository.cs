using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Repositories.Implementations
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly AppDbContext _context;

        public ComentarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comentario>> GetAllAsync()
        {
            return await _context.Comentarios.ToListAsync();
        }

        public async Task<Comentario?> GetByIdAsync(int id)
        {
            return await _context.Comentarios.FindAsync(id);
        }

        public async Task<IEnumerable<Comentario>> FindAsync(Expression<Func<Comentario, bool>> predicate)
        {
            return await _context.Comentarios.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Comentario>> GetByUserIdAsync(int idUsuario)
        {
            return await _context.Comentarios.Where(c => c.IdUsuario == idUsuario).ToListAsync();
        }

        public async Task<IEnumerable<Comentario>> GetBySolicitudIdAsync(int idSolicitud)
        {
            return await _context.Comentarios.Where(c => c.IdSolicitud == idSolicitud).ToListAsync();
        }

        public async Task<IEnumerable<Comentario>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Comentarios.Where(c => c.Fecha >= fechaInicio && c.Fecha <= fechaFin).ToListAsync();
        }

        public async Task<Comentario> AddAsync(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
            return comentario;
        }

        public async Task UpdateAsync(Comentario comentario)
        {
            _context.Entry(comentario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario != null)
            {
                _context.Comentarios.Remove(comentario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CountBySolicitudAsync(int idSolicitud)
        {
            return await _context.Comentarios.CountAsync(c => c.IdSolicitud == idSolicitud);
        }

        public async Task<int> CountByUserAsync(int idUsuario)
        {
            return await _context.Comentarios.CountAsync(c => c.IdUsuario == idUsuario);
        }
    }
}
