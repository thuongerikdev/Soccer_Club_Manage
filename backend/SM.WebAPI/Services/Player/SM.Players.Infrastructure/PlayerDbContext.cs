using Microsoft.EntityFrameworkCore;
using SM.Player.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Player.Infrastructure
{
    public class PlayerDbContext: DbContext
    {
          public DbSet<ClubPlayers> Players { get; set; }

        public PlayerDbContext(DbContextOptions<PlayerDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         

            base.OnModelCreating(modelBuilder);
        }

       
    }
   
    
}
