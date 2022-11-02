using Bookstore.Core.Interfaces.Common;

namespace Bookstore.Core.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime BornDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsAlive { get; set; }
        public DateTime DiedDate { get; set; }

        public override string GetPartitionKey() => City;
    }
}
