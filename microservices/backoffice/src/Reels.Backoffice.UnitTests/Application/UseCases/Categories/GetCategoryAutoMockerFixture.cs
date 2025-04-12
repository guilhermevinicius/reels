using Moq.AutoMock;
using Reels.Backoffice.Application.UseCases.Categories;
using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Category;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Categories;

[CollectionDefinition(nameof(GetCategoryAutoMockerCollection))]
public class GetCategoryAutoMockerCollection : IClassFixture<GetCategoryAutoMockerFixture>;

public class GetCategoryAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new ();

    internal GetCategoryQueryHandler GetInstance()
    {
        AutoMocker = new AutoMocker();
        return AutoMocker.CreateInstance<GetCategoryQueryHandler>();
    }

    public void MockGetCategory()
    {
        AutoMocker.GetMock<IRepositoryQuery>()
            .Setup(x => x.QueryAsNoTracking<Category>())
            .Returns(new List<Category>()
            {
                GetCategory()
            }.AsQueryable());
    }

    #region Private Methods

    private Category GetCategory()
    {
        var category = Category.Create(
            Faker.Random.String(),
            Faker.Lorem.Paragraph());

        category.Id = Guid.Parse("186184cd-7fc2-4205-a7c3-97eb4ea82b34");
        
        return category;
    }

    #endregion
    
}