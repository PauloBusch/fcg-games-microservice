using FCG.Games.Application.UseCases;
using FCG.Games.Infrastructure.ElasticSearch;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services
    .AddControllers();

services
    .AddOpenApi()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateGameUseCase>());
    
services
    .AddElasticSearchModule(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseAuthorization();

app.MapControllers();

app.Run();
