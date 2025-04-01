using GestionAcademicaAPI.DTOs;
using GestionAcademicaAPI.Models;

namespace GestionAcademicaAPI.Services.Interfaces
{
    public interface IAdministradorService
    {
        Task<AdministradorDTO> AddAsync(UsuarioDTO administradorDto);
        Task<Administrador?> GetByIdAsync(int id);
        Task<IEnumerable<Administrador>> GetAllAsync();
        Task<Administrador?> GetByUserIdAsync(int idUsuario);
        Task<AdministradorDTO> UpdateAsync(AdministradorDTO administradorDto);
        Task DeleteAsync(int id);
    }
}
