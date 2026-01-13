
using IITR.DonorBridge.DataService;
using IITR.DonorBridge.DataService.Interfaces;
using IITR.DonorBridge.DataService.Repositories;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "IITR DonorBridge API",
        Description = "An ASP.NET Core Web API for managing DonorBridge operations"
    });
});

builder.Services.AddSingleton(new DbProvider(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITestRepository,TestRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "IITR DonorBridge API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
