using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Interfaces;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Services.Implementations
{
    /// <summary>
    /// Servicio para gestionar entidades de tipo Escuela
    /// </summary>
    public class EscuelaService : IEscuelaService
    {
        private readonly IEscuelaRepository _escuelaRepository;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EscuelaService"/>.
        /// </summary>
        /// <param name="escuelaRepository">Repositorio de escuelas</param>
        public EscuelaService(IEscuelaRepository escuelaRepository)
        {
            _escuelaRepository = escuelaRepository ?? throw new ArgumentNullException(nameof(escuelaRepository));
        }

        public async Task<IEnumerable<Escuela>> GetAllAsync()
        {
            return await _escuelaRepository.GetAllAsync();
        }

        public async Task<Escuela?> GetByIdAsync(int id)
        {
            return await _escuelaRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Escuela>> FindAsync(Expression<Func<Escuela, bool>> predicate)
        {
            return await _escuelaRepository.FindAsync(predicate);
        }

        public async Task<Escuela?> GetByNombreAsync(string nombre)
        {
            return await _escuelaRepository.GetByNombreAsync(nombre);
        }

        public async Task<Escuela> AddAsync(Escuela escuela)
        {
            return await _escuelaRepository.AddAsync(escuela);
        }

        public async Task UpdateAsync(Escuela escuela)
        {
            await _escuelaRepository.UpdateAsync(escuela);
        }

        public async Task DeleteAsync(int id)
        {
            await _escuelaRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsByNombreAsync(string nombre)
        {
            return await _escuelaRepository.ExistsByNombreAsync(nombre);
        }

        public async Task<int> CountAsync()
        {
            return await _escuelaRepository.CountAsync();
        }

        public async Task<IEnumerable<Escuela>> GetEscuelasWithPropostasAsync()
        {
            return await _escuelaRepository.GetEscuelasWithPropostasAsync();
        }
    }
}
