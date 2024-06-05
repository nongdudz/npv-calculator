using Microsoft.OpenApi.Models;
using NPVCalculator.Server.Models;
using NPVCalculator.Server.Services.Calculator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("api/calculator/npv", async (NPVRequest request, ICalculatorService calculatorService) =>
{
    var result = await calculatorService.CalculateNPVAsync(request);
    return result;
})
    .WithName("CalculateNpvWithCashFlowSeries")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Calculate Net Present Value with cash flow series",
        Description = "Returns calculate Net Present Value and cash flow series.",
    });

app.MapPost("api/calculator/npv-range", async (NPVRequest request, ICalculatorService calculatorService) =>
{
    var result = await calculatorService.CalculateNPVWithDiscountRateRangeAsync(request);
    return result;
})
    .WithName("CalculateNpvRangeWithCashFlowSeries")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Calculate Net Present Value with discount rate range",
        Description = "Returns calculate Net Present Value with discount rate range and cash flow series.",
    });

app.MapFallbackToFile("/index.html");

app.Run();