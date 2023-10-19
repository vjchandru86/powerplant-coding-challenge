using PowerPlantCC.Constants;
using PowerPlantCC.Models.Response;

namespace PowerPlantCC.Models
{
    internal class GasPlant : PowerPlant
    {
        public GasPlant(PowerPlant powerplant, Dictionary<string, double> fuels) 
        {
            Pmax = powerplant.Pmax;
            Pmin = powerplant.Pmin;
            Name = powerplant.Name;
            Type = powerplant.Type;
            Efficiency = powerplant.Efficiency;
            CostPerMW = (double)(fuels[FuelConstant.Gas] / Efficiency)
                        // add with carbon emission cost
                        + (0.3 * fuels[FuelConstant.Co2]);
            base.SetCostPminPmax();
        }

        public override ProductionPlanResponse GeneratePP(double loadRequired, PowerPlant[]? powerplants, ProductionPlanResponse[]? productionPlanResponse, int? currentIndex)
        {
            if(loadRequired >= Pmin)
            {
               return  new ProductionPlanResponse(Name, loadRequired);
            }
            else
            {
                var loadAdjusted = AdjustLoad(Pmin - loadRequired, powerplants, productionPlanResponse, currentIndex);
                return loadAdjusted ? new ProductionPlanResponse(Name, Pmin) : throw new Exception("Unable to generate exact load");
            }
        }

        private bool AdjustLoad(double loadToReduce, PowerPlant[]? powerplants, ProductionPlanResponse[]? response, int? currentIndex)
        {
            currentIndex -= 1;
            var powerplant = powerplants!.First(x => x.Name == response![currentIndex ?? 0].Name);
            if (powerplant.Pmax - loadToReduce >= powerplant.Pmin)
            {
                response![currentIndex ?? 0].P -= Convert.ToDecimal(loadToReduce);
                return true;
            }
            else
            {
                if (currentIndex != 0)
                {
                    return AdjustLoad(loadToReduce, powerplants, response, currentIndex);
                }
            }
            return false;
        }
    }
}
