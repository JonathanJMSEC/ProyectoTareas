namespace Tareas.API.Repository.Interfaces
{   
    /// <summary>
    /// Interfaz generica para el repositorio de entidades.
    /// Define operaciones CRUD básicas para cualquier entidad.
    /// </summary>
    /// <typeparam name="T">Representa entidad en la base de datos</typeparam>
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);
    }
}