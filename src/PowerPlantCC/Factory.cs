using PowerPlantCC.Constants;
using PowerPlantCC.Models;

namespace PowerPlantCC
{
    public class Factory
    {
        /// <summary>
        /// creates new powerplant instance based on base plant.
        /// </summary>
        /// <param name="powerplant"></param>
        /// <param name="fuels"></param>
        /// <returns>powerplant instance</returns>
        public static PowerPlant CreatePowerplant(PowerPlant powerplant, Dictionary<string, double> fuels)
        {
            PowerPlant actualPowerPlant = new PowerPlant();
            switch (powerplant.Type)
            {
                case PowerPlantTypeConstant.Turbojet:
                    actualPowerPlant = new TurbinePlant(powerplant) ;
                    break;
                case PowerPlantTypeConstant.GasFired:
                    actualPowerPlant = new GasPlant(powerplant);
                    break;
                case PowerPlantTypeConstant.WindTurbine:
                    actualPowerPlant = new WindPlant(powerplant, fuels);
                    break;
                default:
                    break;
            }
            return actualPowerPlant;
        }
    }
}
