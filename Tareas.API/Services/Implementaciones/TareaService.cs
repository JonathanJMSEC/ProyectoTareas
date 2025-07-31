using Tareas.API.Models;
using Tareas.API.Services.Interfaces;
using Tareas.API.DTO.Tarea;
using Tareas.API.Repository.Interfaces;
using Tareas.API.Mappers;

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
            if (tarea == null)
                return ServiceResponse<Tarea>.Error("Tarea no puede ser nula");

            if (string.IsNullOrWhiteSpace(tarea.Titulo))
                return ServiceResponse<Tarea>.Error("El título de la tarea es obligatorio");

            if (tarea.FechaLimite < DateTime.Now)
                return ServiceResponse<Tarea>.Error("La fecha límite no puede ser anterior a la fecha actual");

            var tareaACrear = TareaMapper.ToModel(tarea);

            await _context.AddAsync(tareaACrear);
            return ServiceResponse<Tarea>.Ok(tareaACrear, "Tarea creada exitosamente");
        }

        public async Task<ServiceResponse<Tarea>> ObtenerTareaPorIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ServiceResponse<Tarea>.Error("El ID de la tarea es obligatorio");
            
            var tarea = await _context.GetByIdAsync(id);

            if (tarea == null)
                return ServiceResponse<Tarea>.Error("Tarea no encontrada");
            
            return ServiceResponse<Tarea>.Ok(tarea, "Tarea obtenida exitosamente");
        }

        public async Task<ServiceResponse<Tarea>> ActualizarTareaAsync(string id, ActualizarTareaDTO tarea)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ServiceResponse<Tarea>.Error("El ID de la tarea es obligatorio");
                
            if (tarea == null)
                return ServiceResponse<Tarea>.Error("Tarea no puede ser nula");

            if (string.IsNullOrWhiteSpace(tarea.Titulo) || string.IsNullOrWhiteSpace(tarea.Descripcion))
                return ServiceResponse<Tarea>.Error("El título y el estado de la tarea es obligatorio");

            if (tarea.FechaLimite < DateTime.Now)
                return ServiceResponse<Tarea>.Error("La fecha límite no puede ser anterior a la fecha actual");

            var existingTarea = await _context.GetByIdAsync(id);

            if (existingTarea == null)
                return ServiceResponse<Tarea>.Error("Tarea no encontrada");

            await _context.UpdateAsync(id, TareaMapper.ToModel(existingTarea, tarea));
            
            return ServiceResponse<Tarea>.Ok(existingTarea, "Tarea actualizada exitosamente");
        }

        public async Task<ServiceResponse<Tarea>> EliminarTareaAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ServiceResponse<Tarea>.Error("El ID de la tarea es obligatorio");

            var tarea = await _context.GetByIdAsync(id);

            if (tarea == null)
                return ServiceResponse<Tarea>.Error("Tarea no encontrada");

            await _context.DeleteAsync(id);
            
            return ServiceResponse<Tarea>.Ok(tarea, "Tarea eliminada exitosamente");
        }
    }
}