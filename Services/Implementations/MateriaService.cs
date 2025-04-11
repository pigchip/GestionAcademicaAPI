using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using GestionAcademicaAPI.Services.Interfaces;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Services.Implementations
{
    public class MateriaService : IMateriaService
    {
        private readonly IMateriaRepository _materiaRepository;

        public MateriaService(IMateriaRepository materiaRepository)
        {
            _materiaRepository = materiaRepository;
        }

        public async Task<IEnumerable<Materia>> GetAllAsync()
        {
            return await _materiaRepository.GetAllAsync();
        }

        public async Task<Materia?> GetByIdAsync(int id)
        {
            return await _materiaRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Materia>> FindAsync(Expression<Func<Materia, bool>> predicate)
        {
            return await _materiaRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Materia>> GetByEstudianteIdAsync(int idEstudiante)
        {
            return await _materiaRepository.GetByEstudianteIdAsync(idEstudiante);
        }

        public async Task<IEnumerable<Materia>> GetByNombreMateriaEscomAsync(string nombreMateriaEscom)
        {
            return await _materiaRepository.GetByNombreMateriaEscomAsync(nombreMateriaEscom);
        }

        public async Task<IEnumerable<Materia>> GetByNombreMateriaForaneaAsync(string nombreMateriaForanea)
        {
            return await _materiaRepository.GetByNombreMateriaForaneaAsync(nombreMateriaForanea);
        }

        public async Task<IEnumerable<Materia>> GetByStatusAsync(string status)
        {
            return await _materiaRepository.GetByStatusAsync(status);
        }

        public async Task<Materia> AddAsync(Materia materia)
        {
            return await _materiaRepository.AddAsync(materia);
        }

        public async Task UpdateAsync(Materia materia)
        {
            await _materiaRepository.UpdateAsync(materia);
        }

        public async Task DeleteAsync(int id)
        {
            await _materiaRepository.DeleteAsync(id);
        }

        public async Task<int> CountByEstudianteAsync(int idEstudiante)
        {
            return await _materiaRepository.CountByEstudianteAsync(idEstudiante);
        }

        public async Task<int> CountByStatusAsync(string status)
        {
            return await _materiaRepository.CountByStatusAsync(status);
        }

        public async Task<bool> ExistsByNombreMateriaEscomAsync(string nombreMateriaEscom)
        {
            return await _materiaRepository.ExistsByNombreMateriaEscomAsync(nombreMateriaEscom);
        }

        public async Task<bool> ExistsByNombreMateriaForaneaAsync(string nombreMateriaForanea)
        {
            return await _materiaRepository.ExistsByNombreMateriaForaneaAsync(nombreMateriaForanea);
        }

        // Implementación del nuevo método
        public async Task UpdateTemarioAsync(UpdateTemarioDto request)
        {
            await _materiaRepository.UpdateTemarioAsync(request);
        }

        // Implementación del nuevo método
        public async Task UpdateStatusAsync(UpdateStatusDto request)
        {
            await _materiaRepository.UpdateStatusAsync(request);
        }
    }
}