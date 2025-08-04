using Tareas.API.DTO.Tarea;
using Tareas.API.Services.Implementaciones;

namespace Tareas.API.Services.Interfaces
{
    public interface ITareaService
    {
        Task<ServiceResponse<ResumenTareaDTO>> CrearTareaAsync(string idUsuario, CrearTareaDTO tarea);
        Task<ServiceResponse<ListarTareaDTO>> ObtenerTareaPorIdAsync(string idTarea, string idUsuario);
        Task<ServiceResponse<ResumenTareaDTO>> ActualizarTareaAsync(string  usuarioId,  string id, ActualizarTareaDTO tarea);
        Task<ServiceResponse<ResumenTareaDTO>> EliminarTareaAsync(string id, string idUsuario);
    }
}