using PowerPlantCC.Constants;
using PowerPlantCC.Models.Response;

namespace PowerPlantCC.Models
{
    internal class WindPlant : PowerPlant
    {
        public WindPlant(PowerPlant powerplant, Dictionary<string, double> fuels)
        {
            Pmax = Math.Round(powerplant.Pmax * (fuels[FuelConstant.Wind] / 100), 1);
            Pmin = powerplant.Pmin;
            Name = powerplant.Name;
            Type = powerplant.Type;
            Efficiency = powerplant.Efficiency;
        }
        public override ProductionPlanResponse GeneratePP(double loadRequired, PowerPlant[]? powerplants, ProductionPlanResponse[]? productionPlanResponse, int? currentIndex)
        {
            return new ProductionPlanResponse { Name = Name, P = loadRequired };
        }
    }
}
