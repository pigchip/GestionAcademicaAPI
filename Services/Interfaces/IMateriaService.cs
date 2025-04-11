using GestionAcademicaAPI.Models;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Services.Interfaces
{
    public interface IMateriaService
    {
        Task<IEnumerable<Materia>> GetAllAsync();
        Task<Materia?> GetByIdAsync(int id);
        Task<IEnumerable<Materia>> FindAsync(Expression<Func<Materia, bool>> predicate);
        Task<IEnumerable<Materia>> GetByEstudianteIdAsync(int idEstudiante);
        Task<IEnumerable<Materia>> GetByNombreMateriaEscomAsync(string nombreMateriaEscom);
        Task<IEnumerable<Materia>> GetByNombreMateriaForaneaAsync(string nombreMateriaForanea);
        Task<IEnumerable<Materia>> GetByStatusAsync(string status);
        Task<Materia> AddAsync(Materia materia);
        Task UpdateAsync(Materia materia);
        Task DeleteAsync(int id);
        Task<int> CountByEstudianteAsync(int idEstudiante);
        Task<int> CountByStatusAsync(string status);
        Task<bool> ExistsByNombreMateriaEscomAsync(string nombreMateriaEscom);
        Task<bool> ExistsByNombreMateriaForaneaAsync(string nombreMateriaForanea);
        // Nuevo método para actualizar el temario
        Task UpdateTemarioAsync(UpdateTemarioDto requestTemario);
        // Nuevo método para actualizar el temario
        Task UpdateStatusAsync(UpdateStatusDto requestTemario);
    }
}