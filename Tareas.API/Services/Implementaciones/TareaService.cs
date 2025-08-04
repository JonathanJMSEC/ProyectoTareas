using Tareas.API.Models;
using Tareas.API.Repository.Implementaciones;
using Tareas.API.Services.Interfaces;
using Tareas.API.DTO.Tarea;
using Tareas.API.Repository.Interfaces;
using Tareas.API.Mappers;
using Tareas.API.Services.Helpers;

namespace Tareas.API.Services.Implementaciones
{
    /// <summary>
    /// Implementación del servicio de tareas.
    /// Hereda de ITareaService para definir operaciones relacionadas con tareas.
    /// </summary>
   public class TareaService : ITareaService
    {
        private readonly TareaRepository _context;

        public TareaService(TareaRepository context)
        {
            _context = context;
        }

        /// <summary>
        /// Crea una nueva tarea en el sistema.
        /// Valida los datos de la tarea y la agrega a la base de datos.
        /// </summary>
        /// <param name="tarea">Los datos de la tarea a crear</param>
        /// <returns>Un resultado indicando si se completó la tarea y la tarea</returns>
        public async Task<ServiceResponse<ResumenTareaDTO>> CrearTareaAsync(string idUsuario, CrearTareaDTO tarea)
        {
            if (!ValidacionesTarea.EsTareaValida(tarea).Success)
                return ServiceResponse<ResumenTareaDTO>.Error("Los datos de la tarea son inválidos.");
            if  (!ValidacionesTarea.EsIdValido(idUsuario).Success)
                return ServiceResponse<ResumenTareaDTO>.Error("El ID de la tarea es inválido.");

            var tareaACrear = TareaMapper.ToModel(tarea);

            await _context.AddAsync(idUsuario, tareaACrear);
            return ServiceResponse<ResumenTareaDTO>.Ok(TareaMapper.ToResumenDTO(tareaACrear), "Tarea creada exitosamente");
        }

        /// <summary>
        /// Obtiene una tarea por su ID.
        /// Valida que el ID sea correcto y que la tarea exista.
        /// </summary>
        /// <param name="id">ID de la tarea a obtener</param>
        /// <returns>Respuesta con la tarea obtenida y mensaje de éxito o error.</returns
        public async Task<ServiceResponse<ListarTareaDTO>> ObtenerTareaPorIdAsync(string idTarea, string idUsuario)
        {
            if (!ValidacionesTarea.EsIdValido(idTarea).Success)
                return ServiceResponse<ListarTareaDTO>.Error("El ID de la tarea es inválido.");
            if  (!ValidacionesTarea.EsIdValido(idUsuario).Success)
                return ServiceResponse<ListarTareaDTO>.Error("El ID del usuario es inválido.");

            var tarea = await _context.GetByIdAsync(idTarea, idUsuario);

            if (!ValidacionesTarea.EsTareaExistente(idTarea, tarea).Success)
                return ServiceResponse<ListarTareaDTO>.Error("La tarea no existe.");

            return ServiceResponse<ListarTareaDTO>.Ok(TareaMapper.ToListarDTO(tarea), "Tarea obtenida exitosamente");
        }

        /// <summary>
        /// Actuañliza una tarea existente.
        /// Valida que el ID de la tarea sea correcto y que la tarea exista.
        /// </summary>
        /// <param name="tarea">DTO con los nuevos datos de la tarea y un mensaje  de exito o error</param
        public async Task<ServiceResponse<ResumenTareaDTO>> ActualizarTareaAsync(string usuarioId, string idTarea, ActualizarTareaDTO tarea)
        {
            if (!ValidacionesTarea.EsIdValido(idTarea).Success)
                return ServiceResponse<ResumenTareaDTO>.Error("El ID de la tarea es inválido.");
            if  (!ValidacionesTarea.EsIdValido(usuarioId).Success)
                return ServiceResponse<ResumenTareaDTO>.Error("El ID de la tarea es inválido.");

            if (!ValidacionesTarea.EsTareaActualizable(idTarea, tarea).Success)
                return ServiceResponse<ResumenTareaDTO>.Error("Los datos de la tarea son inválidos.");

            var existingTarea = await _context.GetByIdAsync(idTarea, usuarioId);

            if (!ValidacionesTarea.EsTareaExistente(idTarea, existingTarea).Success)
                return ServiceResponse<ResumenTareaDTO>.Error("La tarea no existe.");

            await _context.UpdateAsync(usuarioId, tarea, idTarea);

            return ServiceResponse<ResumenTareaDTO>.Ok(TareaMapper.ToResumenDTO(existingTarea), "Tarea actualizada exitosamente");
        }

        /// <summary>
        /// Elimina una tarea por su ID.    
        /// Valida que el ID de la tarea sea correcto y que la tarea exista.
        /// </summary>
        /// <returns>Respuesta con la tarea eliminada y mensaje de éxito o error.</returns
        public async Task<ServiceResponse<ResumenTareaDTO>> EliminarTareaAsync(string idTarea, string idUsuario)
        {
            if  (!ValidacionesTarea.EsIdValido(idTarea).Success)
                return ServiceResponse<ResumenTareaDTO>.Error("El ID de la tarea es inválido.");
            if  (!ValidacionesTarea.EsIdValido(idUsuario).Success)
                return ServiceResponse<ResumenTareaDTO>.Error("El ID del usuario es inválido.");

            var tarea = await _context.GetByIdAsync(idTarea, idUsuario);

            if (!ValidacionesTarea.EsTareaExistente(idTarea, tarea).Success)
                return ServiceResponse<ResumenTareaDTO>.Error("La tarea no existe.");

            await _context.DeleteAsync(idUsuario, idTarea);

            return ServiceResponse<ResumenTareaDTO>.Ok(TareaMapper.ToResumenDTO(tarea), "Tarea eliminada exitosamente");
        }
    }
}