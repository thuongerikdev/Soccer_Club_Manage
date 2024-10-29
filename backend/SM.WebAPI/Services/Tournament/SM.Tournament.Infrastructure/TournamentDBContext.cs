using Microsoft.EntityFrameworkCore;
using SM.Tournament.Domain.Club;
using SM.Tournament.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Infrastructure
{
    public class TournamentDBContext: DbContext
    {
        public DbSet<ClubTeam> Clubs { get; set; }
        public DbSet<ClubPlayers> Players { get; set; }
        public TournamentDBContext(DbContextOptions<TournamentDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ClubPlayers>()
                .HasOne<ClubTeam>()
                .WithMany() // Nếu ClubTeam có nhiều ClubPlayers
                .HasForeignKey(e => e.ClubId) // Sử dụng ClubId cho ClubPlayers
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public object FindAsync(int playerId)
        {
            throw new NotImplementedException();
        }
    }
   
    
}
