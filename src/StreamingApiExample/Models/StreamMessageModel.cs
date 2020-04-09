namespace StreamingApiExample.Models
{
    using System.Text.Json.Serialization;

    public class StreamMessageModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
