using Tareas.API.Models;
using Tareas.API.Services.Interfaces;
using Tareas.API.DTO.Tarea;
using Tareas.API.Repository.Interfaces;
using Tareas.API.Mappers;
using Tareas.API.Services.Helpers;

namespace Tareas.API.Services.Implementaciones
{
   public class TareaService : ITareaService
    {
        private readonly IRepository<Tarea> _context;

        public TareaService(IRepository<Tarea> context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Tarea>> CrearTareaAsync(CrearTareaDTO tarea)
        {
            ValidacionesTarea.EsTareaValida(tarea);

            var tareaACrear = TareaMapper.ToModel(tarea);

            await _context.AddAsync(tareaACrear);
            return ServiceResponse<Tarea>.Ok(tareaACrear, "Tarea creada exitosamente");
        }

        public async Task<ServiceResponse<Tarea>> ObtenerTareaPorIdAsync(string id)
        {
            ValidacionesTarea.EsIdValido(id);
            
            var tarea = await _context.GetByIdAsync(id);

            ValidacionesTarea.EsTareaExistente(id, tarea);
            
            return ServiceResponse<Tarea>.Ok(tarea, "Tarea obtenida exitosamente");
        }

        public async Task<ServiceResponse<Tarea>> ActualizarTareaAsync(string id, ActualizarTareaDTO tarea)
        {
            ValidacionesTarea.EsIdValido(id);
                
            ValidacionesTarea.EsTareaActualizable(id, tarea);

            var existingTarea = await _context.GetByIdAsync(id);

            ValidacionesTarea.EsTareaExistente(id, existingTarea);

            await _context.UpdateAsync(id, TareaMapper.ToModel(existingTarea, tarea));
            
            return ServiceResponse<Tarea>.Ok(existingTarea, "Tarea actualizada exitosamente");
        }

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