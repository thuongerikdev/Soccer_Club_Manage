using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Infrastructure
{
    public class TournamentDbContext: DbContext
    {

        public TournamentDbContext(DbContextOptions<TournamentDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         

            base.OnModelCreating(modelBuilder);
        }

       
    }
   
    
}
