using System;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatabaseLayer.Models
{
    public partial class BoatContext : DbContext
    {
        public BoatContext()
        {
        }

        public BoatContext(DbContextOptions<BoatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Boats> Boats { get; set; }
        public virtual DbSet<RentBoatToCustomer> RentBoatToCustomer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-0FUDUVF;Database=BoatDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Boats>(entity =>
            {
                entity.HasKey(e => e.BoatId)
                    .HasName("PK__Boat__5E");
            });
        }
    }
}
