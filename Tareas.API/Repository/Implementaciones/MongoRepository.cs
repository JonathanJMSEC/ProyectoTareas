using MongoDB.Bson;
using MongoDB.Driver;
using Tareas.API.Repository.Interfaces;

namespace Tareas.API.Repository.Implementaciones
{
    /// <summary>
    /// Implementación genérica del repositorio para operaciones CRUD básicas.
    /// Utiliza MongoDB como base de datos. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoRepository<T> : IRepository<T> where T : class
    {
        protected IMongoCollection<T> Collection { get; }

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            Collection = database.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var filter = Builders<T>.Filter.Empty;
            return await Collection.Find(filter).ToListAsync();
        }

        public Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public Task UpdateAsync(string id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return Collection.ReplaceOneAsync(filter, entity);
        }

        public Task DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return Collection.DeleteOneAsync(filter);
        }
    }
}