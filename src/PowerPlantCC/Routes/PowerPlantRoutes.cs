using Microsoft.AspNetCore.Mvc;
using PowerPlantCC.Models.Request;
using PowerPlantCC.Models.Response;
using PowerPlantCC.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace PowerPlantCC.Routes
{
    public static class PowerPlantRoutes
    {
        public static void MapEndpoints(this WebApplication app)
        {
            app.MapPost("/productionplan", GetProductionPlan)
               .Produces<ProductionPlanResponse[]>(StatusCodes.Status200OK)
               .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
               .WithTags("PowerPlant")
               .WithMetadata(new SwaggerOperationAttribute("Generates production plan", "Generates production plan based on the load, fuels and powerplants")); ;
        }

        static IResult GetProductionPlan(ProductionPlanRequest productionPlanRequest, [FromServices] IProductionPlanService productionPlanService)
        {
            return Results.Ok(productionPlanService.CalculateProductionPlan(productionPlanRequest));
        }
    }
}
