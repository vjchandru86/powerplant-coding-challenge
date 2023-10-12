using PowerPlantCC.Constants;
using PowerPlantCC.Models;
using PowerPlantCC.Models.Request;
using PowerPlantCC.Models.Response;

namespace PowerPlantCC.Services
{
    public interface IProductionPlanService
    {
        /// <summary>
        /// Generates prod plan
        /// </summary>
        /// <param name="load"></param>
        /// <param name="powerplants"></param>
        /// <param name="fuels"></param>
        /// <returns>prod plan</returns>
        ProductionPlanResponse[] CalculateProductionPlan(double load, PowerPlant[] powerplants, Dictionary<string, double> fuels);
    }

    public class ProductionPlanService : IProductionPlanService
    {
        /// <inheritdoc/>>
        public ProductionPlanResponse[] CalculateProductionPlan(double loadRequired, PowerPlant[] powerplants, Dictionary<string, double> fuels)
        {
            var prodPlanResponse = new ProductionPlanResponse[powerplants.Length];
            var orderedPlants = GetPlantsByMeritOrder(powerplants, fuels);
            int i = 0;
            foreach (var plant in orderedPlants)
            {
                if (loadRequired <= 0)
                    prodPlanResponse[i] = new ProductionPlanResponse { Name = plant.Name, P = 0 };
                else
                {
                    if (loadRequired < plant.Pmax)
                    {
                        prodPlanResponse[i] = plant.GeneratePP(loadRequired, powerplants, prodPlanResponse, i);
                        loadRequired -= loadRequired;
                    }
                    else
                    {
                        prodPlanResponse[i] = plant.GeneratePP(plant.Pmax, powerplants, prodPlanResponse, i);
                        loadRequired -= plant.Pmax;
                    }
                }
                i++;
            }
            return prodPlanResponse;
        }

        private IOrderedEnumerable<PowerPlant> GetPlantsByMeritOrder(PowerPlant[] powerplants, Dictionary<string, double> fuels)
        {
            foreach (var powerplant in powerplants)
            {
                powerplant.SetCostPerMW(fuels);
            }
            return powerplants.OrderBy(x => x.CostPerMW);
        }
    }
}
