using System.Text.Json.Serialization;

namespace Clerk.Business.Entity
{
    public class StateResponse
    {
        [JsonPropertyName("postal-code")]
        public string Code { get; set; }

        [JsonPropertyName("state-fullname")] 
        public string Name { get; set; }
    }
}
