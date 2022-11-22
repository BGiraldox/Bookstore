using Bookstore.Core.Interfaces.Common;
using Bookstore.Core.Utility.QueryHandler;
using FluentValidation;

namespace Bookstore.Core.Validators
{
    public class QueryFilterValidator<T> : AbstractValidator<QueryFilter<T>> where T : BaseEntity
    {
        public QueryFilterValidator()
        {
            RuleFor(f => f.FilterType)
                .NotEmpty()
                .IsInEnum();

            When(f => f.FilterType == QueryType.AuthorsByCountry, () =>
            {
                RuleFor(f => f.Country).NotEmpty();
            });

            When(f => f.FilterType == QueryType.LiveOrDeathAutors, () =>
            {
                RuleFor(f => f.IsAlive).NotEmpty();
            });

            When(f => f.FilterType == QueryType.AuthorsByAge, () =>
            {
                RuleFor(f => f.Age).NotEmpty();
            });

            When(f => f.FilterType == QueryType.BooksByCategory, () =>
            {
                RuleFor(f => f.Category).NotEmpty();
            });

            When(f => f.FilterType == QueryType.BooksByAuthor, () =>
            {
                RuleFor(f => f.AuthorId).NotEmpty();
            });

            When(f => f.FilterType == QueryType.BooksByPublicationDateRange, () =>
            {
                RuleFor(f => f.PublicationDateEnd)
                .NotEmpty()
                .GreaterThan(f => f.PublicationDateStart);

                RuleFor(f => f.PublicationDateStart)
                .NotEmpty()
                .LessThan(f => f.PublicationDateEnd);
            });
        }
    }
}
