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

        public DbSet<MinigameReward> MinigameRewards {  get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<TournamentBase>()
        .Property(t => t.TournamentPrice)
        .HasColumnType("decimal(18, 2)");

    modelBuilder.Entity<Orders>()
        .Property(t => t.OrderAmount)
        .HasColumnType("decimal(18, 2)");

    modelBuilder.Entity<FundActionHistory>()
        .Property(t => t.Amount)
        .HasColumnType("decimal(18, 2)");

    // Cấu hình cho ClubFunds
    modelBuilder.Entity<ClubFunds>()
        .Property(f => f.Expense)
        .HasPrecision(18, 2);
    modelBuilder.Entity<ClubFunds>()
        .Property(f => f.Debt)
        .HasPrecision(18, 2);
    modelBuilder.Entity<ClubFunds>()
        .Property(f => f.Contribution)
        .HasPrecision(18, 2);
    modelBuilder.Entity<ClubFunds>()
        .Property(f => f.FundAmount)
        .HasPrecision(18, 2);

    // ClubTeam
     // CelebrateEvent
    modelBuilder.Entity<Minigames>()
        .HasOne<MinigameReward>()
        .WithMany()
        .HasForeignKey(e => e.MinigameRewardID)
        .OnDelete(DeleteBehavior.Restrict);

    // CelebrateEvent
    modelBuilder.Entity<CelebrateEvent>()
        .HasOne<ClubTeam>()
        .WithMany()
        .HasForeignKey(e => e.ClubID)
        .OnDelete(DeleteBehavior.Restrict);

    // ClubFunds
    modelBuilder.Entity<ClubFunds>()
        .HasOne<ClubTeam>()
        .WithMany()
        .HasForeignKey(f => f.ClubID)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<FundActionHistory>()
        .HasOne<ClubFunds>()
        .WithMany()
        .HasForeignKey(h => h.FundID)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<FundActionHistory>()
        .HasOne<ClubPlayers>()
        .WithMany()
        .HasForeignKey(h => h.PlayerID)
        .OnDelete(DeleteBehavior.Restrict);

    // LineUpBase
    modelBuilder.Entity<LineUpBase>()
        .HasOne<ClubTeam>()
        .WithMany()
        .HasForeignKey(l => l.ClubID)
        .OnDelete(DeleteBehavior.Restrict);

    // Matches
    modelBuilder.Entity<Matches>()
        .HasOne<ClubTeam>()
        .WithMany()
        .HasForeignKey(m => m.TeamA)
        .OnDelete(DeleteBehavior.Restrict);
    modelBuilder.Entity<Matches>()
        .HasOne<ClubTeam>()
        .WithMany()
        .HasForeignKey(m => m.TeamB)
        .OnDelete(DeleteBehavior.Restrict);

    // MatchesStatistic
    modelBuilder.Entity<MatchesStatistic>()
        .HasOne<ClubPlayers>()
        .WithMany()
        .HasForeignKey(s => s.PlayerID)
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

    // Minigames
    modelBuilder.Entity<Minigames>()
        .HasOne<TournamentBase>()
        .WithMany()
        .HasForeignKey(m => m.TournamentID)
        .OnDelete(DeleteBehavior.Restrict);

    // Predictions
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
        .HasForeignKey(p => p.UserID)
        .OnDelete(DeleteBehavior.Restrict);

    // Votes
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
        .HasForeignKey(v => v.UserID)
        .OnDelete(DeleteBehavior.Restrict);

    // Orders
    modelBuilder.Entity<Orders>()
        .HasOne<TournamentBase>()
        .WithMany()
        .HasForeignKey(o => o.TournamentID)
        .OnDelete(DeleteBehavior.Restrict);

    // ClubPlayers
    modelBuilder.Entity<ClubPlayers>()
        .HasOne<ClubTeam>()
        .WithMany()
        .HasForeignKey(p => p.ClubID)
        .OnDelete(DeleteBehavior.Restrict);

    // PlayerEvent
    modelBuilder.Entity<PlayerEvent>()
        .HasOne<ClubPlayers>()
        .WithMany()
        .HasForeignKey(pe => pe.PlayerID)
        .OnDelete(DeleteBehavior.Restrict);
    modelBuilder.Entity<PlayerEvent>()
        .HasOne<ClubEventBase>()
        .WithMany()
        .HasForeignKey(pe => pe.EventID)
        .OnDelete(DeleteBehavior.Restrict);

    // PlayerLineUp
    modelBuilder.Entity<PlayerLineUp>()
        .HasOne<ClubPlayers>()
        .WithMany()
        .HasForeignKey(pl => pl.PlayerID)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<PlayerLineUp>()
        .HasOne<LineUpBase>()
        .WithMany()
        .HasForeignKey(pl => pl.LineUpID)
        .OnDelete(DeleteBehavior.Restrict);

    // TournamentClub
    modelBuilder.Entity<TournamentClub>()
        .HasOne<TournamentBase>()
        .WithMany()
        .HasForeignKey(tc => tc.TournamentID)
        .OnDelete(DeleteBehavior.Restrict);
    modelBuilder.Entity<TournamentClub>()
        .HasOne<ClubTeam>()
        .WithMany()
        .HasForeignKey(tc => tc.ClubID)
        .OnDelete(DeleteBehavior.Restrict);
}
    }
}