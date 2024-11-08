using Microsoft.EntityFrameworkCore;
using SM.Match.Domain.Matches;
using SM.Match.Domain.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Match.Infrastructure
{
    public class MatchDbContext: DbContext
    {
     
        public DbSet<Matches> Matches { get; set; }
        public DbSet <MatchesStatistic> MatchesStatistics { get; set; }
      
        public MatchDbContext(DbContextOptions<MatchDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            base.OnModelCreating(modelBuilder);
        }

       
    }
   
    
}
