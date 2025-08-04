using Tareas.API.Models;
using Tareas.API.DTO.Tarea;

namespace Tareas.API.Repository.Interfaces
{
    public interface ITareaRepository
    {
        Task<Tarea> GetByIdAsync(string idTarea, string idUsuario);
        Task AddAsync(string idUsuario, Tarea entity);
        Task UpdateAsync(string idUsuario, CrearTareaDTO tareaDto, string idTarea);
        Task DeleteAsync(string idUsuario, string idTarea);
    }
}