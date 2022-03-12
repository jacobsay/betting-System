﻿namespace BettingSystem.Domain.Competitions.Factories.Leagues;

using Common;
using Models.Leagues;

public interface ILeagueFactory : IFactory<League>
{
    ILeagueFactory WithName(string name);

    ILeagueFactory WithCountry(string name);

    ILeagueFactory WithCountry(Country country);
}