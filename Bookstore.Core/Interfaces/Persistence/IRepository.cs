using Bookstore.Core.Interfaces.Common;
using System.Linq.Expressions;

namespace Bookstore.Core.Interfaces.Persistence
{
    public interface IRepository<T> where T : BaseEntity
    {
        string ContainerName { get; }

        Task<T> Add(T entity);

        Task<List<T>> GetAll();

        Task<T> GetById(string entityId, string partitionKey);

        Task<List<T>> GetByFilter(Expression<Func<T, bool>> filter);

        Task<T> Update(T entity);

        Task Delete(string entityId, string partitionKey);
    }
}
