namespace Bookstore.Core.Interfaces.Common
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T> Add(T entity);

        Task<List<T>> GetAll();

        Task<T> GetById(T entityId);

        Task<T> Update(T entity);

        Task Delete(string entityId, string partitionKey);
    }
}
