using Reels.Backoffice.Api.Configurations;
using Reels.Backoffice.Application.Configurations;
using Reels.Backoffice.Persistence.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureApi()
    .ConfigureApplication()
    .ConfigurePersistence(builder.Configuration);

var app = builder.Build();

await app
    .UseApi()
    .RunAsync();

public partial class Program;