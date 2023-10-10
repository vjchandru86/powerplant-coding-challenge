using PowerPlantCC.Middlewares;
using PowerPlantCC.Routes;
using PowerPlantCC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.EnableAnnotations();
});

// add console logger
builder.Services.AddLogging(builder => builder.AddConsole());

builder.Services.AddCors();

// register service
builder.Services.AddScoped<IProductionPlanService, ProductionPlanService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// setting global cors policy to allow all
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// add errorhandler middleware
app.UseMiddleware<ErrorHandlerMiddleware>();

// add powerplant endpoints
PowerPlantRoutes.MapEndpoints(app);

app.Run();