using Bogus;

namespace Reels.Backoffice.UnitTests.Common;

public abstract class BaseTest
{
    protected readonly Faker Faker = new("pt_BR");
}