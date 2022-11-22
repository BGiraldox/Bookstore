namespace Bookstore.Core.Utility.QueryHandler
{
    public enum QueryType
    {
        //Author Queries
        AuthorsByCountry,
        LiveOrDeathAutors,
        AuthorsByAge,

        //Book Queries
        BooksByCategory,
        BooksByAuthor,
        BooksByPublicationDateRange
    }
}
