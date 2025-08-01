using Tareas.API.DTO.Tarea;
using Tareas.API.Models;
using Tareas.API.Services.Implementaciones;

namespace Tareas.API.Services.Helpers
{
    public static class ValidacionesTarea
    {
        public static ServiceResponse<CrearTareaDTO> EsTareaValida(CrearTareaDTO tarea)
        {
            if (tarea == null)
                return ServiceResponse<CrearTareaDTO>.Error("La tarea no puede ser nula.");

            if (string.IsNullOrWhiteSpace(tarea.Titulo))
                return ServiceResponse<CrearTareaDTO>.Error("El título de la tarea es obligatorio.");

            if (tarea.FechaLimite < DateTime.Now)
                return ServiceResponse<CrearTareaDTO>.Error("La fecha límite debe ser una fecha futura.");

            return ServiceResponse<CrearTareaDTO>.Ok(tarea, "La tarea es válida.");
        }

        public static ServiceResponse<ActualizarTareaDTO> EsTareaActualizable(string id, ActualizarTareaDTO tarea)
        {
            if (tarea == null)
                return ServiceResponse<ActualizarTareaDTO>.Error("La tarea no puede ser nula.");

            if (string.IsNullOrWhiteSpace(tarea.Titulo) || string.IsNullOrWhiteSpace(tarea.Descripcion))
                return ServiceResponse<ActualizarTareaDTO>.Error("El título y la descripción son obligatorios.");

            if (tarea.FechaLimite < DateTime.Now)
                return ServiceResponse<ActualizarTareaDTO>.Error("La fecha límite debe ser una fecha futura.");

            return ServiceResponse<ActualizarTareaDTO>.Ok(tarea, "La tarea es actualizable.");
        }

        public static ServiceResponse<Tarea> EsTareaExistente(string id, Tarea tarea)
        {
            if (tarea == null)
                return ServiceResponse<Tarea>.Error($"No se encontró una tarea con el ID: {id}.");

            return ServiceResponse<Tarea>.Ok(tarea, "La tarea existe.");
        }

        public static ServiceResponse<Tarea> EsIdValido(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ServiceResponse<Tarea>.Error("El ID de la tarea no puede ser nulo o vacío.");

            if (!Guid.TryParse(id, out _))
                return ServiceResponse<Tarea>.Error("El ID de la tarea debe ser un GUID válido.");

            return ServiceResponse<Tarea>.Ok(null, "El ID es válido.");
        }
        
        public static ServiceResponse<IEnumerable<Tarea>> EsListaDeTareasValida(IEnumerable<Tarea> tareas)
        {
            if (tareas == null || !tareas.Any())
                return ServiceResponse<IEnumerable<Tarea>>.Error("La lista de tareas no puede ser nula o vacía.");

            return ServiceResponse<IEnumerable<Tarea>>.Ok(tareas, "La lista de tareas es válida.");
        }
    }
}
