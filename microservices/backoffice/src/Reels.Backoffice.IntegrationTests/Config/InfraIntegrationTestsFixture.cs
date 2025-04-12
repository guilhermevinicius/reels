using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reels.Backoffice.Domain.Models.Category;
using Reels.Backoffice.Domain.Models.Genre;
using Reels.Backoffice.Persistence.Configurations;
using Testcontainers.PostgreSql;

namespace Reels.Backoffice.IntegrationTests.Config;

[CollectionDefinition(nameof(InfraIntegrationTestsCollection))]
public sealed class InfraIntegrationTestsCollection : IClassFixture<InfraIntegrationTestsFixture>;

public sealed class InfraIntegrationTestsFixture : WebApplicationFactory<Program>, IAsyncLifetime, IDisposable
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().Build();
    public ISender Sender { get; private set; }

    public HttpClient Client { get; private set; } = new();

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        await InitializeClient();
    }

    public new async Task DisposeAsync()
    {
        await _postgreSqlContainer.StopAsync();
    }

    public new void Dispose()
    {
        GC.SuppressFinalize(this);
        base.Dispose();
        Client.Dispose();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            var dbContextOptionsDescriptor = services.SingleOrDefault(descriptor =>
                descriptor.ServiceType == typeof(DbContextOptions<DataContext>));

            services.Remove(dbContextOptionsDescriptor!);

            services.AddDbContext<DataContext>(option =>
                option.UseNpgsql(_postgreSqlContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention());

            var scope = services.BuildServiceProvider().CreateScope();
            Sender = scope.ServiceProvider.GetRequiredService<ISender>();
        });
    }

    private async Task InitializeClient()
    {
        var clientOptions = new WebApplicationFactoryClientOptions();

        Client = CreateClient(clientOptions);

        await PopulateIntegrationTest();
    }

    private async Task PopulateIntegrationTest()
    {
        var scope = Services.CreateScope();
        
        var provider = scope.ServiceProvider;
        
        await using var context = provider.GetRequiredService<DataContext>();
        
        await context.Database.EnsureCreatedAsync();

        await PopulateCategories(context);

        await PopulateGenre(context);

        await context.SaveChangesAsync();
    }
    
    #region Population

    private static async Task PopulateCategories(DbContext context)
    {
        var category = Category.Create(
            "Category",
            "CategoryDescription");

        category.Id = Guid.Parse("2278a870-8dc8-4d70-acb7-f6ece6754b09");
        
        var category2 = Category.Create(
            "Category",
            "CategoryDescription");

        category2.Id = Guid.Parse("2278a870-8dc8-4d70-acb7-f6ece6754b19");

        await context.AddAsync(category, CancellationToken.None);
        await context.AddAsync(category2, CancellationToken.None);
    }

    private static async Task PopulateGenre(DbContext context)
    {
        var genre = Genre.Create(
            "Category",
            false);

        genre.Id = Guid.Parse("2278a870-8dc8-4d70-acb7-f6ece6754b09");
        
        var genre2 = Genre.Create(
            "Category",
            true);

        genre2.Id = Guid.Parse("2278a870-8dc8-4d70-acb7-f6ece6754b19");

        await context.AddAsync(genre, CancellationToken.None);
        await context.AddAsync(genre2, CancellationToken.None);
    }

    #endregion
    
}