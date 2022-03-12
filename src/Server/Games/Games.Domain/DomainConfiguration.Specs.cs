﻿namespace BettingSystem.Domain.Games;

using System.Linq;
using Common;
using Factories.Matches;
using Factories.Teams;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class DomainConfigurationSpecs
{
    [Fact]
    public void AddDomainShouldRegisterFactories()
    {
        var serviceCollection = new ServiceCollection();

        var services = serviceCollection
            .AddDomain()
            .BuildServiceProvider();

        services
            .GetService<IMatchFactory>()
            .Should()
            .NotBeNull();

        services
            .GetService<ITeamFactory>()
            .Should()
            .NotBeNull();
    }

    [Fact]
    public void AddDomainShouldRegisterInitialData()
    {
        var serviceCollection = new ServiceCollection();

        var services = serviceCollection
            .AddDomain()
            .BuildServiceProvider();

        services
            .GetServices<IInitialData>()
            .ToList()
            .ForEach(initialData => initialData
                .Should()
                .NotBeNull());
    }
}