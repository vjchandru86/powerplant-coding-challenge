using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace PowerPlantCC.Models.Response
{
    public class ProductionPlanResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("p")]
        public decimal P { get; set; }

        public ProductionPlanResponse(string name, double p)
        {
            Name = name;
            P = Convert.ToDecimal(string.Format("{0:#.0}", p));
        }
    }
}
