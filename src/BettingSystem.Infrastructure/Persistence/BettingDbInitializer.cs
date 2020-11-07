﻿namespace BettingSystem.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;

    internal class BettingDbInitializer : IInitializer
    {
        private readonly BettingDbContext db;

        public BettingDbInitializer(BettingDbContext db) => this.db = db;

        public void Initialize() => this.db.Database.Migrate();
    }
}