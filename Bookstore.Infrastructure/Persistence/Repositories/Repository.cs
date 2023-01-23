using Bookstore.Core.Interfaces.Common;
using Bookstore.Core.Interfaces.Persistence;
using Bookstore.Core.Utility;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Linq.Expressions;

namespace Bookstore.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly Container _container;

        protected QueryRequestOptions queryOptions;

        public string ContainerName => $"{typeof(T).Name}s";

        public Repository(CosmosClient cosmosClient)
        {
            _container = cosmosClient.GetContainer(Constants.DATABASE_NAME, ContainerName);

            queryOptions = new QueryRequestOptions
            {
                //Max number of results per set of results.
                //This is done to avoid returning excessive amounts of data in a single request and to improve performance.
                MaxItemCount = Constants.MAX_NUMBER_OF_RESULT_ITEMS
            };
        }

        public async Task<T> Add(T entity)
            => await _container.CreateItemAsync<T>(entity, new PartitionKey(entity.GetPartitionKey())).ConfigureAwait(false);

        public async Task<List<T>> GetAll()
        {
            using var iterator = _container
                .GetItemLinqQueryable<T>(requestOptions: queryOptions)
                .Take(Constants.MAX_NUMBER_OF_RESULT_ITEMS) //Set the max number of items to be taken and then returned
                .ToFeedIterator();

            List<T> result = new();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync().ConfigureAwait(false);
                result.AddRange(response);
            }

            return result;
        }

        public async Task<T> GetById(string entityId, string partitionKey)
        {
            try
            {
                var readResponse = await _container.ReadItemAsync<T>(entityId, new PartitionKey(partitionKey)).ConfigureAwait(false);
                return readResponse.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound) { return null; }
        }

        public async Task<List<T>> GetByFilter(Expression<Func<T, bool>> filter)
        {

            using FeedIterator<T> iterator = _container
                .GetItemLinqQueryable<T>(requestOptions: queryOptions)
                .Where(filter)
                .Take(Constants.MAX_NUMBER_OF_RESULT_ITEMS)
                .ToFeedIterator();

            List<T> result = new();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync().ConfigureAwait(false);
                result.AddRange(response);
            }

            return result;
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
