using Bookstore.Core.Utility.QueryHandler;

namespace Bookstore.Core.Interfaces.Common
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T> Add(T entity);

        Task<List<T>> GetAll();

        Task<List<T>> GetAllByFilter(QueryFilter<T> filter);

        Task<T> GetById(string entityId, string partitionKey);

        Task<T> Update(T entity);

        Task Delete(string entityId, string partitionKey);
    }
}
