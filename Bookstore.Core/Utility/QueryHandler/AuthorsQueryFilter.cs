using Bookstore.Core.Entities;
using System.Linq.Expressions;

namespace Bookstore.Core.Utility.QueryHandler
{
    public class AuthorsQueryFilter : QueryFilter<Author>
    {
        public override Expression GetQueryFilter()
        {
            switch (FilterType)
            {
                case QueryType.AuthorsByCountry:
                    Expression<Func<Author, bool>> isSameCountry = a => a.Country.Equals(Country, StringComparison.InvariantCultureIgnoreCase);
                    return isSameCountry;

                case QueryType.LiveOrDeathAutors:
                    Expression<Func<Author, bool>> isAliveAuthor = a => a.IsAlive == IsAlive;
                    return isAliveAuthor;

                case QueryType.AuthorsByAge:
                    Expression<Func<Author, bool>> isSameAge = a => a.IsAlive == true && (DateTime.Today.Year - a.BornDate.Year) == Age;
                    return isSameAge;
            }

            return null;
        }
    }
}
