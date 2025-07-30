using Tareas.API.DTO.Tarea;
using Tareas.API.Models;

namespace Tareas.API.Services.Interfaces
{
    public interface ITareaService
    {
        Task CrearTareaAsync(CrearTareaDTO tarea);
        Task<Tarea> ObtenerTareaPorIdAsync(string id);
        Task ActualizarTareaAsync(string id, ActualizarTareaDTO tarea);
        Task EliminarTareaAsync(string id);
    }
}