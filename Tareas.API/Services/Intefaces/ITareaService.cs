using Tareas.API.DTO.Tarea;
using Tareas.API.Models;
using Tareas.API.Services.Implementaciones;

namespace Tareas.API.Services.Interfaces
{
    public interface ITareaService
    {
        Task<ServiceResponse<ResumenTareaDTO>> CrearTareaAsync(CrearTareaDTO tarea);
        Task<ServiceResponse<ListarTareaDTO>> ObtenerTareaPorIdAsync(string id);
        Task<ServiceResponse<ResumenTareaDTO>> ActualizarTareaAsync(string id, ActualizarTareaDTO tarea);
        Task<ServiceResponse<ResumenTareaDTO>> EliminarTareaAsync(string id);
    }
}