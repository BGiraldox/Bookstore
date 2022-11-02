using Newtonsoft.Json;

namespace Bookstore.Core.Interfaces.Common
{
    public abstract class BaseEntity
    {
        [JsonProperty(PropertyName = "id")]
        public virtual string Id { get; set; }

        public abstract string GetPartitionKey();
    }
}
