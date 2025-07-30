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

        public async Task CrearTareaAsync(CrearTareaDTO tarea)
        {
            await _context.AddAsync(TareaMapper.ToModel(tarea));
        }

        public async Task<Tarea> ObtenerTareaPorIdAsync(string id)
        {
            return await _context.GetByIdAsync(id);
        }

        public async Task ActualizarTareaAsync(string id, ActualizarTareaDTO tarea)
        {
            var existingTarea = await _context.GetByIdAsync(id);

            if (existingTarea != null)
                await _context.UpdateAsync(id, TareaMapper.ToModel(existingTarea, tarea));
        }

        public async Task EliminarTareaAsync(string id)
        {
            await _context.DeleteAsync(id);
        }
    }
}