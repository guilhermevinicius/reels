using System.Text;
using Bogus;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Reels.Backoffice.Application.Contracts.Storage;
using Reels.Backoffice.Domain.Models.Category;
using Reels.Backoffice.Domain.Models.Genre;
using Reels.Backoffice.Domain.Models.Video;
using Reels.Backoffice.Domain.Models.Video.Enums;
using Reels.Backoffice.Infrastructure.Storage;
using Reels.Backoffice.Persistence.Configurations;
using Testcontainers.Minio;
using Testcontainers.PostgreSql;

namespace Reels.Backoffice.IntegrationTests.Config;

[CollectionDefinition(nameof(InfraIntegrationTestsCollection))]
public sealed class InfraIntegrationTestsCollection : IClassFixture<InfraIntegrationTestsFixture>;

public sealed class InfraIntegrationTestsFixture : WebApplicationFactory<Program>, IAsyncLifetime, IDisposable
{
    private const string AccessKey = "accessKey";
    private const string AccessSecret = "accessSecret";
    
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().Build();
    private readonly MinioContainer _minioContainer = new MinioBuilder()
        .WithPortBinding(9050, true)
        .WithEnvironment("MINIO_ROOT_USER", AccessKey)
        .WithEnvironment("MINIO_ROOT_PASSWORD", AccessSecret)
        .Build();
    
    public ISender Sender { get; private set; }

    public HttpClient Client { get; private set; } = new();

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        await _minioContainer.StartAsync();
        await InitializeClient();
    }

    public new async Task DisposeAsync()
    {
        await _postgreSqlContainer.StopAsync();
        await _minioContainer.StopAsync();
    }

    public new void Dispose()
    {
        GC.SuppressFinalize(this);
        base.Dispose();
        Client.Dispose();
    }

    public async Task<HttpResponseMessage> SendRequest(HttpMethod httpMethod, string requestUri)
    {
        var request = new HttpRequestMessage(httpMethod, requestUri);

        return await Client.SendAsync(request);
    }
    
    public async Task<HttpResponseMessage> SendRequest(HttpMethod httpMethod, string requestUri, string? jsonContent = null)
    {
        var request = new HttpRequestMessage(httpMethod, requestUri);

        if (!string.IsNullOrEmpty(jsonContent))
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        return await Client.SendAsync(request);
    }
    
    public async Task<HttpResponseMessage> SendRequest(HttpMethod httpMethod, string requestUri, MultipartFormDataContent content)
    {
        var request = new HttpRequestMessage(httpMethod, requestUri);
        request.Content = content;

        return await Client.SendAsync(request);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureServices(services =>
        {
            var dbContextOptionsDescriptor = services.SingleOrDefault(descriptor =>
                descriptor.ServiceType == typeof(DbContextOptions<DataContext>));

            services.Remove(dbContextOptionsDescriptor!);

            var storage = services.SingleOrDefault(descriptor =>
                descriptor.ServiceType == typeof(IMinioClient));

            services.Remove(storage!);

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
        
        var storage = provider.GetRequiredService<IStorageService>();

        await PopulateStorage(storage);

        await context.Database.EnsureCreatedAsync();

        await PopulateCategories(context);

        await PopulateGenre(context);

        await PopulateVideos(context);

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

        var category3 = Category.Create(
            "Category",
            "CategoryDescription");

        category3.Id = Guid.Parse("2278a870-8dc8-4d70-acb7-f6ece6754b29");

        await context.AddAsync(category, CancellationToken.None);
        await context.AddAsync(category2, CancellationToken.None);
        await context.AddAsync(category3, CancellationToken.None);
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

        var genre3 = Genre.Create(
            "Category",
            true);

        genre3.Id = Guid.Parse("2278a870-8dc8-4d70-acb7-f6ece6754b29");

        await context.AddAsync(genre, CancellationToken.None);
        await context.AddAsync(genre2, CancellationToken.None);
        await context.AddAsync(genre3, CancellationToken.None);
    }

    private static async Task PopulateVideos(DataContext context)
    {
        var video = Video.Create(
            "Title",
            "Description",
            2025,
            true,
            true,
            90,
            Rating.Rate12);

        video.Id = Guid.Parse("2278a870-8dc8-4d70-acb7-f6ece6754b29");

        video.UpdateThumb("/uplods/video.jpg");
        video.UpdateThumbHalf("/uplods/video.jpg");

        await context.AddAsync(video, CancellationToken.None);
    }

    private static async Task PopulateStorage(IStorageService storage)
    {
        var file = new MemoryStream("Files!"u8.ToArray());

        await storage.UploadFile(
            "/uplods/video.jpg",
            "image/png",
            "video.jpg",
            file);
    }

    #endregion
}