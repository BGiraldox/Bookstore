using Bookstore.Core.Interfaces.Common;
using System.Linq.Expressions;

namespace Bookstore.Core.Utility.QueryHandler
{
    public class QueryFilter<T> : IQueryFilter<T> where T : BaseEntity
    {
        public QueryType? FilterType { get; set; }
        public string? Country { get; set; }
        public bool? IsAlive { get; set; }
        public int? Age { get; set; }
        public string? Category { get; set; }
        public string? AuthorId { get; set; }
        public DateTime? PublicationDateStart { get; set; }
        public DateTime? PublicationDateEnd { get; set; }

        public virtual Expression GetQueryFilter() => throw new NotImplementedException();
    }
}
