﻿namespace BettingSystem.Infrastructure.Betting
{
    using System.Reflection;
    using Common;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDatabase(configuration)
                .AddCommonInfrastructure(
                    configuration,
                    Assembly.GetExecutingAssembly());

        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<BettingDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sqlServer => sqlServer.MigrationsAssembly(
                            typeof(BettingDbContext).Assembly.FullName)))
                .AddScoped<IBettingDbContext>(provider => provider
                    .GetService<BettingDbContext>()!)
                .AddTransient<IInitializer, BettingDbInitializer>();
    }
}