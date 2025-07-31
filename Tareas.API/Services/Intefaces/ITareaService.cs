using Tareas.API.DTO.Tarea;
using Tareas.API.Models;
using Tareas.API.Services.Implementaciones;

namespace Tareas.API.Services.Interfaces
{
    public interface ITareaService
    {
        Task<ServiceResponse<Tarea>> CrearTareaAsync(CrearTareaDTO tarea);
        Task<ServiceResponse<Tarea>> ObtenerTareaPorIdAsync(string id);
        Task<ServiceResponse<Tarea>> ActualizarTareaAsync(string id, ActualizarTareaDTO tarea);
        Task<ServiceResponse<Tarea>> EliminarTareaAsync(string id);
    }
}