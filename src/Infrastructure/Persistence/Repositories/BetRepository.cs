﻿namespace BettingSystem.Infrastructure.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Features.Bets;
    using Application.Features.Bets.Queries.Details;
    using Application.Features.Bets.Queries.Mine;
    using AutoMapper;
    using Domain.Models.Bets;
    using Microsoft.EntityFrameworkCore;

    internal class BetRepository : DataRepository<Bet>, IBetRepository
    {
        private readonly IMapper mapper;

        public BetRepository(BettingDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public async Task<BetDetailsResponseModel> GetDetails(
            int id,
            CancellationToken cancellationToken)
            => await this.mapper
                .ProjectTo<BetDetailsResponseModel>(this
                    .AllAsNoTracking()
                    .Where(b => b.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<IEnumerable<MineBetsResponseModel>> GetMine(
            int gamblerId,
            CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<MineBetsResponseModel>(this
                    .GetGamblerBetsQuery(gamblerId))
                .ToListAsync(cancellationToken);

        private IQueryable<Bet> GetGamblerBetsQuery(
            int gamblerId)
            => this
                .Data
                .Gamblers
                .Where(g => g.Id == gamblerId)
                .SelectMany(g => g.Bets);
    }
}