using Newtonsoft.Json;

namespace Clerk.Common
{
    public class ComponentErrorResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }
    }
}
