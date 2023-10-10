using PowerPlantCC.Constants;
using PowerPlantCC.Models;
using PowerPlantCC.Models.Request;
using PowerPlantCC.Models.Response;

namespace PowerPlantCC.Services
{
    public interface IProductionPlanService
    {
        /// <summary>
        /// Calculates the production plan for power plants
        /// </summary>
        /// <param name="productionPlanRequest"><see cref="ProductionPlanRequest"/></param>
        /// <returns><see cref="ProductionPlanResponse"></returns>
        ProductionPlanResponse[] CalculateProductionPlan(ProductionPlanRequest productionPlanRequest);
    }

    public class ProductionPlanService : IProductionPlanService
    {
        /// <inheritdoc/>>
        public ProductionPlanResponse[] CalculateProductionPlan(ProductionPlanRequest productionPlanRequest)
        {
            var prodPlanResponse = new ProductionPlanResponse[productionPlanRequest.powerplants.Length];
            var orderedPlants = GetPlantsByMeritOrder(productionPlanRequest.powerplants, productionPlanRequest.fuels);
            var loadRequired = productionPlanRequest.load;
            int i = 0;
            foreach (var plant in orderedPlants)
            {
                if (loadRequired <= 0)
                    prodPlanResponse[i] = new ProductionPlanResponse { Name = plant.Name, P = 0 };
                else
                {
                    // get the pmax
                    var pmax = GetPmax(plant);
                    
                    // for non-gasfired, do not worry about pmin as it will be 0.
                    if (loadRequired < pmax && plant.Type != PowerPlantTypeConstant.GasFired)
                    {
                        prodPlanResponse[i] = new ProductionPlanResponse { Name = plant.Name, P = loadRequired };
                        loadRequired -= loadRequired;
                    }
                    // for gasfired with load required greater than or equal to pmin(load required < pmax)
                    else if (loadRequired < pmax && plant.Type == PowerPlantTypeConstant.GasFired && loadRequired >= plant.Pmin)
                    {
                        prodPlanResponse[i] = new ProductionPlanResponse { Name = plant.Name, P = loadRequired };
                        loadRequired -= loadRequired;
                    }
                    // for gasfired with load required less than pmin(load required < pmax)
                    else if (loadRequired < pmax && plant.Type == PowerPlantTypeConstant.GasFired && loadRequired < plant.Pmin)
                    {
                        var loadAdjusted = AdjustLoad(productionPlanRequest.powerplants, prodPlanResponse, i, plant.Pmin - loadRequired);
                        prodPlanResponse[i] = new ProductionPlanResponse { Name = plant.Name, P = plant.Pmin };
                        loadRequired -= loadAdjusted ? loadRequired : plant.Pmin;
                    }
                    else
                    {
                        prodPlanResponse[i] = new ProductionPlanResponse { Name = plant.Name, P = pmax };
                        loadRequired -= pmax;
                    }
                }
                i++;
            }
            return prodPlanResponse;

            double GetPmax(PowerPlant powerplant) => powerplant.Type == PowerPlantTypeConstant.WindTurbine ? Math.Round(powerplant.Pmax * (productionPlanRequest.fuels[FuelConstant.Wind] / 100), 1) : powerplant.Pmax;
        }

        private IOrderedEnumerable<PowerPlant> GetPlantsByMeritOrder(PowerPlant[] powerplants, Dictionary<string, double> fuels)
        {
            foreach (var powerplant in powerplants)
            {
                switch (powerplant.Type)
                {
                    case PowerPlantTypeConstant.Turbojet:
                        powerplant.SetCost(fuels[FuelConstant.Kerosine]);
                        break;
                    case PowerPlantTypeConstant.GasFired:
                        powerplant.SetCost(fuels[FuelConstant.Gas] + (0.3 * fuels[FuelConstant.Co2]));
                        break;
                    default:
                        break;
                }
            }
            return powerplants.OrderBy(x => x.CostPerMW);
        }

        private bool AdjustLoad(PowerPlant[] powerplants, ProductionPlanResponse[] response, int currentIndex, double loadToReduce)
        {
            currentIndex -= 1;
            var powerplant = powerplants.First(x => x.Name == response[currentIndex].Name);
            if(powerplant.Pmax - loadToReduce >= powerplant.Pmin)
            {
                response[currentIndex].P -= loadToReduce;
                return true;
            }
            else
            {
                if(currentIndex != 0)
                {
                   return AdjustLoad(powerplants, response, currentIndex, loadToReduce);
                }
            }
            return false;
        }
    }
}
