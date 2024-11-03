using Microsoft.EntityFrameworkCore;
using SM.Club.Domain.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Club.Infrastructure
{
    public class ClubDbContext: DbContext
    {
        public DbSet<ClubTeam> Clubs { get; set; }

        public ClubDbContext(DbContextOptions<ClubDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
   
    
}
