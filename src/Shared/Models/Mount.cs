using System.Text.Json.Serialization;

namespace CharacterPlanner.Shared.Models;
public record Mount
{
    [JsonPropertyName("key")]
    public DataKey Key { get; set; }
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    public Mount(DataKey key, int id, string name)
    {
        Key = key;
        Id = id;
        Name = name;
    }
}
