﻿namespace BettingSystem.Infrastructure.Persistence.Configurations
{
    using Domain.Models.Matches;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.StartDate)
                .IsRequired();

            builder
                .Ignore(m => m.Result);

            builder
                .HasOne(m => m.HomeTeam)
                .WithMany()
                .HasForeignKey("HomeTeamId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(m => m.AwayTeam)
                .WithMany()
                .HasForeignKey("AwayTeamId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(m => m.Stadium)
                .WithOne()
                .HasForeignKey<Match>("StadiumId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .OwnsOne(m => m.Statistics, s =>
                {
                    s.WithOwner();

                    s.Property(st => st.HomeScore);
                    s.Property(st => st.AwayScore);
                });
        }
    }
}