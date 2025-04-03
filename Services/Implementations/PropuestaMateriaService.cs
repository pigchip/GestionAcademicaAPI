using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Interfaces;

namespace GestionAcademicaAPI.Services.Implementations
{
    public class PropuestaMateriaService : IPropuestaMateriaService
    {
        private readonly IPropuestaMateriaRepository _propuestaMateriaRepository;

        public PropuestaMateriaService(IPropuestaMateriaRepository propuestaMateriaRepository)
        {
            _propuestaMateriaRepository = propuestaMateriaRepository;
        }

        public async Task<IEnumerable<PropuestaMateria>> GetAllAsync()
        {
            return await _propuestaMateriaRepository.GetAllAsync();
        }

        public async Task<PropuestaMateria?> GetByIdAsync(int id)
        {
            return await _propuestaMateriaRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<PropuestaMateria>> GetByPropuestaIdAsync(int idPropuesta)
        {
            return await _propuestaMateriaRepository.GetByPropuestaIdAsync(idPropuesta);
        }

        public async Task<IEnumerable<PropuestaMateria>> GetByMateriaIdAsync(int idMateria)
        {
            return await _propuestaMateriaRepository.GetByMateriaIdAsync(idMateria);
        }

        public async Task<PropuestaMateria> AddAsync(PropuestaMateria propuestaMateria)
        {
            return await _propuestaMateriaRepository.AddAsync(propuestaMateria);
        }

        public async Task<IEnumerable<PropuestaMateria>> AddRangeAsync(IEnumerable<PropuestaMateria> propuestaMaterias)
        {
            return await _propuestaMateriaRepository.AddRangeAsync(propuestaMaterias);
        }

        public async Task DeleteAsync(int id)
        {
            await _propuestaMateriaRepository.DeleteAsync(id);
        }

        public async Task DeleteByPropuestaIdAsync(int idPropuesta)
        {
            await _propuestaMateriaRepository.DeleteByPropuestaIdAsync(idPropuesta);
        }

        public async Task<int> CountMateriasByPropuestaAsync(int idPropuesta)
        {
            return await _propuestaMateriaRepository.CountMateriasByPropuestaAsync(idPropuesta);
        }

        public async Task<int> CountPropuestasByMateriaAsync(int idMateria)
        {
            return await _propuestaMateriaRepository.CountPropuestasByMateriaAsync(idMateria);
        }
    }
}
