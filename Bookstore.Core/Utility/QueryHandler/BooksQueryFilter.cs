using Bookstore.Core.Entities;
using System.Linq.Expressions;

namespace Bookstore.Core.Utility.QueryHandler
{
    public class BooksQueryFilter : QueryFilter<Book>
    {
        public override Expression GetQueryFilter()
        {
            switch (FilterType)
            {
                case QueryType.BooksByCategory:
                    Expression<Func<Book, bool>> isSameCategory = a => a.Category.Equals(Category, StringComparison.InvariantCultureIgnoreCase);
                    return isSameCategory;

                case QueryType.BooksByAuthor:
                    Expression<Func<Book, bool>> isSameAuthor = a => a.AuthorId == AuthorId;
                    return isSameAuthor;

                case QueryType.BooksByPublicationDateRange:
                    Expression<Func<Book, bool>> isInPublicationDateRange = a => a.PublicationDate >= PublicationDateStart && a.PublicationDate <= PublicationDateEnd;
                    return isInPublicationDateRange;
            }

            return null;
        }
    }
}
