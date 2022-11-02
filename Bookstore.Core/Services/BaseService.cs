using Bookstore.Core.Interfaces.Common;
using Bookstore.Core.Interfaces.Persistence;

namespace Bookstore.Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> Add(T entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            return await _repository.Add(entity).ConfigureAwait(false);
        }

        public async Task<List<T>> GetAll() => await _repository.GetAll().ConfigureAwait(false);

        public async Task<T> GetById(string entityId, string partitionKey) => await _repository.GetById(entityId, partitionKey).ConfigureAwait(false);

        public async Task<T> Update(T entity) => await _repository.Update(entity).ConfigureAwait(false);


        public async Task Delete(string entityId, string partitionKey) => await _repository.Delete(entityId, partitionKey).ConfigureAwait(false);

    }
}
