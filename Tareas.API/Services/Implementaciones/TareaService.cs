using Tareas.API.Models;
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
        private readonly IRepository<Tarea> _context;

        public TareaService(IRepository<Tarea> context)
        {
            _context = context;
        }

        /// <summary>
        /// Crea una nueva tarea en el sistema.
        /// Valida los datos de la tarea y la agrega a la base de datos.
        /// </summary>
        /// <param name="tarea">Los datos de la tarea a crear</param>
        /// <returns>Un resultado indicando si se completó la tarea y la tarea</returns>
        public async Task<ServiceResponse<Tarea>> CrearTareaAsync(CrearTareaDTO tarea)
        {
            ValidacionesTarea.EsTareaValida(tarea);

            var tareaACrear = TareaMapper.ToModel(tarea);

            await _context.AddAsync(tareaACrear);
            return ServiceResponse<Tarea>.Ok(tareaACrear, "Tarea creada exitosamente");
        }

        /// <summary>
        /// Obtiene una tarea por su ID.
        /// Valida que el ID sea correcto y que la tarea exista.
        /// </summary>
        /// <param name="id">ID de la tarea a obtener</param>
        /// <returns>Respuesta con la tarea obtenida y mensaje de éxito o error.</returns
        public async Task<ServiceResponse<Tarea>> ObtenerTareaPorIdAsync(string id)
        {
            ValidacionesTarea.EsIdValido(id);

            var tarea = await _context.GetByIdAsync(id);

            ValidacionesTarea.EsTareaExistente(id, tarea);

            return ServiceResponse<Tarea>.Ok(tarea, "Tarea obtenida exitosamente");
        }

        /// <summary>
        /// Actuañliza una tarea existente.
        /// Valida que el ID de la tarea sea correcto y que la tarea exista.
        /// </summary>
        /// <param name="tarea">DTO con los nuevos datos de la tarea y un mensaje  de exito o error</param
        public async Task<ServiceResponse<Tarea>> ActualizarTareaAsync(string id, ActualizarTareaDTO tarea)
        {
            ValidacionesTarea.EsIdValido(id);

            ValidacionesTarea.EsTareaActualizable(id, tarea);

            var existingTarea = await _context.GetByIdAsync(id);

            ValidacionesTarea.EsTareaExistente(id, existingTarea);

            await _context.UpdateAsync(id, TareaMapper.ToModel(existingTarea, tarea));

            return ServiceResponse<Tarea>.Ok(existingTarea, "Tarea actualizada exitosamente");
        }

        /// <summary>
        /// Elimina una tarea por su ID.    
        /// Valida que el ID de la tarea sea correcto y que la tarea exista.
        /// </summary>
        /// <returns>Respuesta con la tarea eliminada y mensaje de éxito o error.</returns
        public async Task<ServiceResponse<Tarea>> EliminarTareaAsync(string id)
        {
            ValidacionesTarea.EsIdValido(id);

            var tarea = await _context.GetByIdAsync(id);

            ValidacionesTarea.EsTareaExistente(id, tarea);

            await _context.DeleteAsync(id);

            return ServiceResponse<Tarea>.Ok(tarea, "Tarea eliminada exitosamente");
        }
    }
}