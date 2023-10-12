using PowerPlantCC.Constants;
using PowerPlantCC.Models.Request;
using PowerPlantCC.Models.Response;

namespace PowerPlantCC.Models
{
    internal class TurbinePlant : PowerPlant
    {
        public TurbinePlant(PowerPlant powerplant)
        {
            Pmax = powerplant.Pmax;
            Pmin = powerplant.Pmin;
            Name = powerplant.Name;
            Type = powerplant.Type;
            Efficiency = powerplant.Efficiency;
        }

        public override ProductionPlanResponse GeneratePP(double loadRequired, PowerPlant[]? powerplants, ProductionPlanResponse[]? productionPlanResponse, int? currentIndex)
        {
            return new ProductionPlanResponse { Name = Name, P = loadRequired };
        }

        public override void SetCostPerMW(Dictionary<string, double> fuels)
        {
            CostPerMW = (double)(fuels[FuelConstant.Kerosine] / Efficiency);
            base.SetCostPminPmax();
        }
    }
}
