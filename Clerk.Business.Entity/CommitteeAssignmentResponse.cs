using System.Text.Json.Serialization;

namespace Clerk.Business.Entity
{
    public class CommitteeAssignmentResponse
    {
        [JsonPropertyName("comcode")] 
        public string Code { get; set; }
        public string Rank { get; set; }
    }
}
