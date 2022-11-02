using Bookstore.Core.Interfaces.Common;
using Bookstore.Core.Interfaces.Persistence;
using Bookstore.Core.Utility;
using Microsoft.Azure.Cosmos;

namespace Bookstore.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly Container _container;

        public string ContainerName => $"{typeof(T).Name}s";

        public Repository(CosmosClient cosmosClient)
        {
            _container = cosmosClient.GetContainer(Constants.DATABASE_NAME, ContainerName);
        }

        public async Task<T> Add(T entity) 
            => await _container.CreateItemAsync<T>(entity, new PartitionKey(entity.GetPartitionKey())).ConfigureAwait(false);
        
        public async Task<List<T>> GetAll()
        {
            var query = _container.GetItemQueryIterator<T>(new QueryDefinition("select * from c"));

            List<T> result = new();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync().ConfigureAwait(false);
                result.AddRange(response);
            }

            return result;
        }

        public Task<T> GetById(T entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Update(T entity)
        {
            try
            {
                return await _container.ReplaceItemAsync<T>(entity, entity.Id).ConfigureAwait(false);
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound) { return null; }
        }
        
        public async Task Delete(string entityId, string partitionKey)
            => await _container.DeleteItemAsync<T>(entityId, new PartitionKey(partitionKey)).ConfigureAwait(false);
    }
}
