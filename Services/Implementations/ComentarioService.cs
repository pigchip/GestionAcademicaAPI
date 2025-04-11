using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Interfaces;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Services.Implementations
{
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;

        public ComentarioService(IComentarioRepository comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }

        public async Task<IEnumerable<Comentario>> GetAllAsync()
        {
            return await _comentarioRepository.GetAllAsync();
        }

        public async Task<Comentario?> GetByIdAsync(int id)
        {
            return await _comentarioRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Comentario>> FindAsync(Expression<Func<Comentario, bool>> predicate)
        {
            return await _comentarioRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Comentario>> GetByUserIdAsync(int idUsuario)
        {
            return await _comentarioRepository.GetByUserIdAsync(idUsuario);
        }

        public async Task<IEnumerable<Comentario>> GetBySolicitudIdAsync(int idSolicitud)
        {
            return await _comentarioRepository.GetBySolicitudIdAsync(idSolicitud);
        }

        public async Task<IEnumerable<Comentario>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _comentarioRepository.GetByDateRangeAsync(fechaInicio, fechaFin);
        }

        public async Task<Comentario> AddAsync(Comentario comentario)
        {
            return await _comentarioRepository.AddAsync(comentario);
        }

        public async Task UpdateAsync(Comentario comentario)
        {
            await _comentarioRepository.UpdateAsync(comentario);
        }

        public async Task DeleteAsync(int id)
        {
            await _comentarioRepository.DeleteAsync(id);
        }

        public async Task<int> CountBySolicitudAsync(int idSolicitud)
        {
            return await _comentarioRepository.CountBySolicitudAsync(idSolicitud);
        }

        public async Task<int> CountByUserAsync(int idUsuario)
        {
            return await _comentarioRepository.CountByUserAsync(idUsuario);
        }
    }
}
