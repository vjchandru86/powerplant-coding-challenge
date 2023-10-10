using System.Text.Json.Serialization;

namespace PowerPlantCC.Models.Response
{
    public class ProductionPlanResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("p")]
        public double P { get; set; }
    }
}
