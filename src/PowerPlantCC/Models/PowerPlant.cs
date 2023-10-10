using System.Text.Json.Serialization;

namespace PowerPlantCC.Models
{
    public class PowerPlant
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("efficiency")]
        public double Efficiency { get; set; }
        [JsonPropertyName("pmin")]
        public double Pmin { get; set; }
        [JsonPropertyName("pmax")]
        public double Pmax { get; set; }
        [JsonIgnore]
        public double CostPerMW { get; private set; } = 0;
        [JsonIgnore]
        public double CostPmax { get; private set; } = 0;
        [JsonIgnore]
        public double CostPmin { get; private set; } = 0;

        public void SetCost(double? price)
        {
            if(price != null)
            {
                CostPerMW = (double)(price / Efficiency);
                CostPmin = Pmin * CostPerMW;
                CostPmax = Pmax * CostPerMW;
            }
        }
    }
}
