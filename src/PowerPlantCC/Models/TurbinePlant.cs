using PowerPlantCC.Constants;
using PowerPlantCC.Models.Response;

namespace PowerPlantCC.Models
{
    internal class TurbinePlant : PowerPlant
    {
        public TurbinePlant(PowerPlant powerplant, Dictionary<string, double> fuels)
        {
            Pmax = powerplant.Pmax;
            Pmin = powerplant.Pmin;
            Name = powerplant.Name;
            Type = powerplant.Type;
            Efficiency = powerplant.Efficiency;
            CostPerMW = (double)(fuels[FuelConstant.Kerosine] / Efficiency);
            base.SetCostPminPmax();
        }
    }
}
