using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prison.Models;

namespace Prison.Data
{
    public class PrisonContext : DbContext
    {
        public PrisonContext (DbContextOptions<PrisonContext> options)
            : base(options)
        {
        }

        public DbSet<Prison.Models.ReeducationProgram> ReeducationProgram { get; set; } = default!;

        public DbSet<Prison.Models.ReeducationMeeting>? ReeducationMeeting { get; set; }

        public DbSet<Prison.Models.Warden>? Warden { get; set; }

        public DbSet<Prison.Models.ReeducationStaff>? ReeducationStaff { get; set; }

        public DbSet<Prison.Models.Prisoner>? Prisoner { get; set; }

        public DbSet<Prison.Models.Crime>? Crime { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //modelBuilder.Entity<ReeducationProgram>()
            //    .HasMany(e => e.ReeducationStaff)
            //    .WithMany(e => e.Programs);

            modelBuilder.Entity<Prisoner>()
                .HasMany(v => v.Visitors)
                .WithMany(v => v.Prisoners)
                .UsingEntity<PrisonerVisitorRelation>();
        }

        public DbSet<Prison.Models.Cell>? Cell { get; set; }

        public DbSet<Prison.Models.CellBlock>? CellBlock { get; set; }

        public DbSet<Prison.Models.Visitor>? Visitor { get; set; }

        public DbSet<Prison.Models.Job>? Job { get; set; }

        public DbSet<Prison.Models.Cook>? Cook { get; set; }
    }
}
