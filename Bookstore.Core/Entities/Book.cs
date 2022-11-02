using Bookstore.Core.Interfaces.Common;

namespace Bookstore.Core.Entities
{
    public class Book : BaseEntity
    {
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Image { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Language { get; set; }
        public int PrintLenght { get; set; }
        public string Description { get; set; }

        public override string GetPartitionKey() => Category;
    }
}
