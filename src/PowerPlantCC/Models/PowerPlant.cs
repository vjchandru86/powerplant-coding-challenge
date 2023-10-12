using PowerPlantCC.Models.Response;
using System.Text.Json.Serialization;

namespace PowerPlantCC.Models
{
    public class PowerPlant
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("type")]
        public string Type { get; set; } = null!;
        [JsonPropertyName("efficiency")]
        public double Efficiency { get; set; }
        [JsonPropertyName("pmin")]
        public double Pmin { get; set; }
        [JsonPropertyName("pmax")]
        public double Pmax { get; set; }
        [JsonIgnore]
        public double CostPerMW { get; protected set; } = 0;
        [JsonIgnore]
        public double CostPmax { get; protected set; } = 0;
        [JsonIgnore]
        public double CostPmin { get; protected set; } = 0;

        public virtual void SetCostPerMW(Dictionary<string, double> fuels)
        {

        }

        public virtual ProductionPlanResponse GeneratePP(double loadRequired, PowerPlant[]? powerplants, ProductionPlanResponse[]? productionPlanResponse, int? currentIndex)
        {
            throw new NotImplementedException();
        }

        protected void SetCostPminPmax()
        {
            CostPmin = Pmin * CostPerMW;
            CostPmax = Pmax * CostPerMW;
        }
    }
}
