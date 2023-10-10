namespace PowerPlantCC.Models.Request
{
    public record ProductionPlanRequest(double load, Dictionary<string, double> fuels, PowerPlant[] powerplants);
}
