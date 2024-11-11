using Microsoft.EntityFrameworkCore;
using SM.Tournament.Domain.Club;
using SM.Tournament.Domain.Club.Club;
using SM.Tournament.Domain.Club.ClubEvent;
using SM.Tournament.Domain.Club.ClubFund;
using SM.Tournament.Domain.LineUp;
using SM.Tournament.Domain.Match;
using SM.Tournament.Domain.Minigame;
using SM.Tournament.Domain.Orders;
using SM.Tournament.Domain.Player;
using SM.Tournament.Domain.Tournament;
using System;

namespace SM.Tournament.Infrastructure
{
    public class TournamentDbContext : DbContext
    {
        public TournamentDbContext(DbContextOptions<TournamentDbContext> options) : base(options) { }

        // DbSets for each entity
        public DbSet<ClubTeam> ClubTeams { get; set; }
        public DbSet<CelebrateEvent> CelebrateEvents { get; set; }
        public DbSet<TeamMeetingEvent> TeamMeetingEvents { get; set; }
        public DbSet<TrainingEvent> TrainingEvents { get; set; }
        public DbSet<ClubFunds> ClubFunds { get; set; }
        public DbSet<FundActionHistory> FundActionHistories { get; set; }
        public DbSet<LineUpBase> LineUps { get; set; }
        public DbSet<LineUpMatches> LineUpMatches { get; set; }
        public DbSet<Matches> Matches { get; set; }
        public DbSet<MatchesStatistic> MatchesStatistics { get; set; }
        public DbSet<Minigames> Minigames { get; set; }
        public DbSet<Predictions> Predictions { get; set; }
        public DbSet<Votes> Votes { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<ClubPlayers> ClubPlayers { get; set; }
        public DbSet<PlayerEvent> PlayerEvents { get; set; }
        public DbSet<PlayerLineUp> PlayerLineUps { get; set; }
        public DbSet<TournamentBase> Tournaments { get; set; }
        public DbSet<TournamentClub> TournamentClubs { get; set; }
        public DbSet<PlayerFund> PlayerFunds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure foreign keys for entities

            // ClubTeam and ClubFunds
            modelBuilder.Entity<ClubFunds>()
                .HasOne<ClubTeam>()
                .WithMany()
                .HasForeignKey(f => f.ClubID)
                .OnDelete(DeleteBehavior.Restrict);

            // ClubTeam and FundActionHistory
            modelBuilder.Entity<FundActionHistory>()
                .HasOne<ClubFunds>()
                .WithMany()
                .HasForeignKey(f => f.FundID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FundActionHistory>()
                .HasOne<ClubTeam>()
                .WithMany()
                .HasForeignKey(f => f.ClubID)
                .OnDelete(DeleteBehavior.Restrict);

            // ClubTeam and PlayerEvent
            modelBuilder.Entity<PlayerEvent>()
                .HasOne<ClubTeam>()
                .WithMany()
                .HasForeignKey(e => e.ClubID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlayerEvent>()
                .HasOne<ClubPlayers>()
                .WithMany()
                .HasForeignKey(e => e.PlayerID)
                .OnDelete(DeleteBehavior.Restrict);

            // PlayerLineUp and ClubTeam
            modelBuilder.Entity<PlayerLineUp>()
                .HasOne<ClubTeam>()
                .WithMany()
                .HasForeignKey(p => p.ClubID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlayerLineUp>()
                .HasOne<ClubPlayers>()
                .WithMany()
                .HasForeignKey(p => p.PlayerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlayerLineUp>()
                .HasOne<LineUpBase>()
                .WithMany()
                .HasForeignKey(p => p.LineUpID)
                .OnDelete(DeleteBehavior.Restrict);

            // ClubPlayers and PlayerFund
            modelBuilder.Entity<PlayerFund>()
                .HasOne<ClubPlayers>()
                .WithMany()
                .HasForeignKey(f => f.PlayerID)
                .OnDelete(DeleteBehavior.Restrict);

            // TournamentBase and TournamentClub
            modelBuilder.Entity<TournamentClub>()
                .HasOne<TournamentBase>()
                .WithMany()
                .HasForeignKey(c => c.TournamentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TournamentClub>()
                .HasOne<ClubTeam>()
                .WithMany()
                .HasForeignKey(c => c.ClubID)
                .OnDelete(DeleteBehavior.Restrict);

            // Matches and Tournament
            modelBuilder.Entity<Matches>()
                .HasOne<TournamentBase>()
                .WithMany()
                .HasForeignKey(m => m.TournamentID)
                .OnDelete(DeleteBehavior.Restrict);

            // MatchesStatistic and Player, ClubTeam, LineUp, Matches
            modelBuilder.Entity<MatchesStatistic>()
                .HasOne<ClubPlayers>()
                .WithMany()
                .HasForeignKey(s => s.PlayerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatchesStatistic>()
                .HasOne<ClubTeam>()
                .WithMany()
                .HasForeignKey(s => s.ClubID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatchesStatistic>()
                .HasOne<LineUpBase>()
                .WithMany()
                .HasForeignKey(s => s.LineUpID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatchesStatistic>()
                .HasOne<Matches>()
                .WithMany()
                .HasForeignKey(s => s.MatchesID)
                .OnDelete(DeleteBehavior.Restrict);

            // Predictions and Minigames, Matches, ClubPlayers
            modelBuilder.Entity<Predictions>()
                .HasOne<Minigames>()
                .WithMany()
                .HasForeignKey(p => p.MinigameID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Predictions>()
                .HasOne<Matches>()
                .WithMany()
                .HasForeignKey(p => p.MatchID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Predictions>()
                .HasOne<ClubPlayers>()
                .WithMany()
                .HasForeignKey(p => p.PlayerID)
                .OnDelete(DeleteBehavior.Restrict);

            // Votes and Minigames, Matches, ClubPlayers
            modelBuilder.Entity<Votes>()
                .HasOne<Minigames>()
                .WithMany()
                .HasForeignKey(v => v.MinigameID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Votes>()
                .HasOne<Matches>()
                .WithMany()
                .HasForeignKey(v => v.MatchID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Votes>()
                .HasOne<ClubPlayers>()
                .WithMany()
                .HasForeignKey(v => v.PlayerID)
                .OnDelete(DeleteBehavior.Restrict);

            // LineUpMatches and Matches, LineUp
            modelBuilder.Entity<LineUpMatches>()
                .HasOne<Matches>()
                .WithMany()
                .HasForeignKey(lm => lm.MatchID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LineUpMatches>()
                .HasOne<LineUpBase>()
                .WithMany()
                .HasForeignKey(lm => lm.LineUpID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
