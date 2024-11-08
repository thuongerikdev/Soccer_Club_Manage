using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.LineUp.Domain.LineUp;
namespace SM.LineUp.Infrastructure
{
    public class LineUpDbContext : DbContext
    {

        public DbSet<LineUpBase> LineUps { get; set; }
        public DbSet<PlayerLineUp> PlayerLineUps { get; set; }

        public LineUpDbContext(DbContextOptions<LineUpDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
