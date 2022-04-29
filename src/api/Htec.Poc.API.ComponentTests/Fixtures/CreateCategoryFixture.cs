﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Amido.Stacks.Testing.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using Htec.Poc.API.Authentication;
using Htec.Poc.API.Models.Requests;
using Htec.Poc.API.Models.Responses;
using Htec.Poc.Application.Integration;

namespace Htec.Poc.API.ComponentTests.Fixtures;

public class CreateCategoryFixture : ApiClientFixture
{
    readonly Domain.Reward existingReward;
    readonly Guid userRestaurantId = Guid.Parse("2AA18D86-1A4C-4305-95A7-912C7C0FC5E1");
    readonly CreateCategoryRequest newCategory;

    IRewardRepository repository;
    IApplicationEventPublisher applicationEventPublisher;

    public CreateCategoryFixture(Domain.Reward reward, CreateCategoryRequest newCategory, IOptions<JwtBearerAuthenticationConfiguration> jwtBearerAuthenticationOptions)
        : base(jwtBearerAuthenticationOptions)
    {
        this.existingReward = reward;
        this.newCategory = newCategory;
    }

    protected override void RegisterDependencies(IServiceCollection collection)
    {
        base.RegisterDependencies(collection);

        // Mocked external dependencies, the setup should
        // come later according to each scenario
        repository = Substitute.For<IRewardRepository>();
        applicationEventPublisher = Substitute.For<IApplicationEventPublisher>();

        collection.AddTransient(IoC => repository);
        collection.AddTransient(IoC => applicationEventPublisher);
    }

    /****** GIVEN ******************************************************/

    internal void GivenAnExistingReward()
    {
        repository.GetByIdAsync(id: Arg.Is<Guid>(id => id == existingReward.Id))
            .Returns(existingReward);

        repository.SaveAsync(entity: Arg.Is<Domain.Reward>(e => e.Id == existingReward.Id))
            .Returns(true);
    }

    internal void GivenARewardDoesNotExist()
    {
        repository.GetByIdAsync(id: Arg.Any<Guid>())
            .Returns((Domain.Reward)null);
    }

    internal void GivenTheRewardBelongsToUserRestaurant()
    {
        existingReward.With(m => m.TenantId, userRestaurantId);
    }

    internal void GivenTheRewardDoesNotBelongToUserRestaurant()
    {
        existingReward.With(m => m.TenantId, Guid.NewGuid());
    }

    internal void GivenTheCategoryDoesNotExist()
    {
        if (existingReward == null || existingReward.Categories == null)
            return;

        //Ensure in the future reward is not created with categories
        for (int i = 0; i < existingReward.Categories.Count(); i++)
        {
            existingReward.RemoveCategory(existingReward.Categories.First().Id);
        }
        existingReward.ClearEvents();
    }

    internal void GivenTheCategoryAlreadyExist()
    {
        existingReward.AddCategory(Guid.NewGuid(), newCategory.Name, "Some description");
        existingReward.ClearEvents();
    }

    /****** WHEN ******************************************************/

    internal async Task WhenTheCategoryIsSubmitted()
    {
        await CreateCategory(existingReward.Id, newCategory);
    }

    /****** THEN ******************************************************/

    internal async Task ThenTheCategoryIsAddedToReward()
    {
        var resourceCreated = await GetResponseObject<ResourceCreatedResponse>();
        resourceCreated.ShouldNotBeNull();

        var category = existingReward.Categories.SingleOrDefault(c => c.Name == newCategory.Name);

        category.ShouldNotBeNull();
        category.Id.ShouldBe(resourceCreated.Id);
        category.Name.ShouldBe(newCategory.Name);
        category.Description.ShouldBe(newCategory.Description);
    }

    internal void ThenRewardIsLoadedFromStorage()
    {
        repository.Received(1).GetByIdAsync(Arg.Is<Guid>(id => id == existingReward.Id));
    }

    internal void ThenTheRewardShouldBePersisted()
    {
        repository.Received(1).SaveAsync(Arg.Is<Domain.Reward>(reward => reward.Id == existingReward.Id));
    }

    internal void ThenTheRewardShouldNotBePersisted()
    {
        repository.DidNotReceive().SaveAsync(Arg.Any<Domain.Reward>());
    }

    internal void ThenARewardUpdatedEventIsRaised()
    {
        applicationEventPublisher.Received(1).PublishAsync(Arg.Any<RewardUpdatedEvent>());
    }

    internal void ThenARewardUpdatedEventIsNotRaised()
    {
        applicationEventPublisher.DidNotReceive().PublishAsync(Arg.Any<RewardCreatedEvent>());
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