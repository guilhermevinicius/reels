using Bogus;

namespace Reels.Backoffice.IntegrationTests.Config;

public abstract class BaseTest
{
    protected readonly Faker Faker = new("pt_BR");
}