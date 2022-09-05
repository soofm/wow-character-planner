using System.Text.Json.Serialization;

namespace CharacterPlanner.Shared.Models;
public record ListMountsResponse
{
    [JsonPropertyName("mounts")]
    public Mount[] Mounts { get; set; }
    public ListMountsResponse(Mount[] mounts)
    {
        Mounts = mounts;
    }
}
