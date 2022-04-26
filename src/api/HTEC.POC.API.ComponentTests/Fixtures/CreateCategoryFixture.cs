using System;
using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using HTEC.POC.Application.CQRS.Events;
using Amido.Stacks.Testing.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using HTEC.POC.API.Authentication;
using HTEC.POC.API.Models.Requests;
using HTEC.POC.API.Models.Responses;
using HTEC.POC.Application.Integration;

namespace HTEC.POC.API.ComponentTests.Fixtures;

public class CreateCategoryFixture : ApiClientFixture
{
    readonly Domain.Rewards existingRewards;
    readonly Guid userRestaurantId = Guid.Parse("2AA18D86-1A4C-4305-95A7-912C7C0FC5E1");
    readonly CreateCategoryRequest newCategory;

    IRewardsRepository repository;
    IApplicationEventPublisher applicationEventPublisher;

    public CreateCategoryFixture(Domain.Rewards rewards, CreateCategoryRequest newCategory, IOptions<JwtBearerAuthenticationConfiguration> jwtBearerAuthenticationOptions)
        : base(jwtBearerAuthenticationOptions)
    {
        this.existingRewards = rewards;
        this.newCategory = newCategory;
    }

    protected override void RegisterDependencies(IServiceCollection collection)
    {
        base.RegisterDependencies(collection);

        // Mocked external dependencies, the setup should
        // come later according to each scenario
        repository = Substitute.For<IRewardsRepository>();
        applicationEventPublisher = Substitute.For<IApplicationEventPublisher>();

        collection.AddTransient(IoC => repository);
        collection.AddTransient(IoC => applicationEventPublisher);
    }

    /****** GIVEN ******************************************************/

    internal void GivenAnExistingRewards()
    {
        repository.GetByIdAsync(id: Arg.Is<Guid>(id => id == existingRewards.Id))
            .Returns(existingRewards);

        repository.SaveAsync(entity: Arg.Is<Domain.Rewards>(e => e.Id == existingRewards.Id))
            .Returns(true);
    }

    internal void GivenARewardsDoesNotExist()
    {
        repository.GetByIdAsync(id: Arg.Any<Guid>())
            .Returns((Domain.Rewards)null);
    }

    internal void GivenTheRewardsBelongsToUserRestaurant()
    {
        existingRewards.With(m => m.TenantId, userRestaurantId);
    }

    internal void GivenTheRewardsDoesNotBelongToUserRestaurant()
    {
        existingRewards.With(m => m.TenantId, Guid.NewGuid());
    }

    internal void GivenTheCategoryDoesNotExist()
    {
        if (existingRewards == null || existingRewards.Categories == null)
            return;

        //Ensure in the future rewards is not created with categories
        for (int i = 0; i < existingRewards.Categories.Count(); i++)
        {
            existingRewards.RemoveCategory(existingRewards.Categories.First().Id);
        }
        existingRewards.ClearEvents();
    }

    internal void GivenTheCategoryAlreadyExist()
    {
        existingRewards.AddCategory(Guid.NewGuid(), newCategory.Name, "Some description");
        existingRewards.ClearEvents();
    }

    /****** WHEN ******************************************************/

    internal async Task WhenTheCategoryIsSubmitted()
    {
        await CreateCategory(existingRewards.Id, newCategory);
    }

    /****** THEN ******************************************************/

    internal async Task ThenTheCategoryIsAddedToRewards()
    {
        var resourceCreated = await GetResponseObject<ResourceCreatedResponse>();
        resourceCreated.ShouldNotBeNull();

        var category = existingRewards.Categories.SingleOrDefault(c => c.Name == newCategory.Name);

        category.ShouldNotBeNull();
        category.Id.ShouldBe(resourceCreated.Id);
        category.Name.ShouldBe(newCategory.Name);
        category.Description.ShouldBe(newCategory.Description);
    }

    internal void ThenRewardsIsLoadedFromStorage()
    {
        repository.Received(1).GetByIdAsync(Arg.Is<Guid>(id => id == existingRewards.Id));
    }

    internal void ThenTheRewardsShouldBePersisted()
    {
        repository.Received(1).SaveAsync(Arg.Is<Domain.Rewards>(rewards => rewards.Id == existingRewards.Id));
    }

    internal void ThenTheRewardsShouldNotBePersisted()
    {
        repository.DidNotReceive().SaveAsync(Arg.Any<Domain.Rewards>());
    }

    internal void ThenARewardsUpdatedEventIsRaised()
    {
        applicationEventPublisher.Received(1).PublishAsync(Arg.Any<RewardsUpdatedEvent>());
    }

    internal void ThenARewardsUpdatedEventIsNotRaised()
    {
        applicationEventPublisher.DidNotReceive().PublishAsync(Arg.Any<RewardsCreatedEvent>());
    }

    internal void ThenACategoryCreatedEventIsRaised()
    {
        applicationEventPublisher.Received(1).PublishAsync(Arg.Any<CategoryCreatedEvent>());
    }

    internal void ThenACategoryCreatedEventIsNotRaised()
    {
        applicationEventPublisher.DidNotReceive().PublishAsync(Arg.Any<CategoryCreatedEvent>());
    }
}
