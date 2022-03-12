﻿namespace BettingSystem.Infrastructure.Competitions.Configurations;

using Domain.Competitions.Models.Leagues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Domain.Common.Models.ModelConstants.Common;

internal class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder
            .HasKey(g => g.Id);

        builder
            .Property(g => g.Name)
            .HasMaxLength(MaxNameLength)
            .IsRequired();
    }
}