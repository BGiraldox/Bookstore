using System.Linq.Expressions;

namespace Bookstore.Core.Interfaces.Common
{
    public interface IQueryFilter<T> where T : BaseEntity
    {
        abstract Expression GetQueryFilter();
    }
}
