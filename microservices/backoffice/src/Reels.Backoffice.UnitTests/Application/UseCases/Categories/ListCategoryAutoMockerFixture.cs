using Moq.AutoMock;
using Reels.Backoffice.Application.UseCases.Categories;
using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Category;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Categories;

[CollectionDefinition(nameof(ListCategoryAutoMockerCollection))]
public class ListCategoryAutoMockerCollection : IClassFixture<ListCategoryAutoMockerFixture>;

public class ListCategoryAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new ();

    internal ListCategoryQueryHandler GetInstance()
    {
        AutoMocker = new AutoMocker();
        return AutoMocker.CreateInstance<ListCategoryQueryHandler>();
    }

    public void MockGetAllCategory()
    {
        AutoMocker.GetMock<IRepositoryQuery>()
            .Setup(x => x.QueryAsNoTracking<Category>())
            .Returns(new List<Category>()
            {
                GetCategory(),
                GetCategory()
            }.AsQueryable());
    }

    #region Private Methods

    private Category GetCategory()
    {
        var category = Category.Create(
            Faker.Random.String(),
            Faker.Lorem.Paragraph());

        return category;
    }

    #endregion

}