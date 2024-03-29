﻿using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Htec.Poc.API.Models.Responses;

namespace Htec.Poc.API.UnitTests.Models;

public class SearchRewardResponseTests
{
    [Fact]
    public void Size_Should_ReturnInt()
    {
        // Arrange
        // Act
        // Assert
        typeof(SearchRewardResponse)
            .Properties()
            .First(x => x.Name == "Size")
            .Should()
            .Return<int>();
    }

    [Fact]
    public void Offset_Should_ReturnInt()
    {
        // Arrange
        // Act
        // Assert
        typeof(SearchRewardResponse)
            .Properties()
            .First(x => x.Name == "Offset")
            .Should()
            .Return<int>();
    }

    [Fact]
    public void Results_Should_ReturnList()
    {
        // Arrange
        // Act
        // Assert
        typeof(SearchRewardResponse)
            .Properties()
            .First(x => x.Name == "Results")
            .Should()
            .Return<List<SearchRewardResponseItem>>();
    }
}
