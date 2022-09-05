using System.Text.Json.Serialization;

namespace CharacterPlanner.Shared.Models;
public record DataKey
{
    [JsonPropertyName("href")]
    public string Href { get; set; }
    public DataKey(string href)
    {
        Href = href;
    }
}
