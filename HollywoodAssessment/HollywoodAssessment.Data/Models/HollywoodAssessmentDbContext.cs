using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HollywoodAssessment.Data.Models
{
    public partial class HollywoodAssessmentDbContext : DbContext
    {
        public HollywoodAssessmentDbContext()
        {
        }

        public HollywoodAssessmentDbContext(DbContextOptions<HollywoodAssessmentDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventDetail> EventDetail { get; set; }
        public virtual DbSet<EventDetailStatus> EventDetailStatus { get; set; }
        public virtual DbSet<Tournament> Tournament { get; set; }
        public virtual DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              optionsBuilder.UseSqlServer("Name=HollywoodAssessmentDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.EventDateTime).HasColumnType("datetime");

                entity.Property(e => e.EventEndDateTime).HasColumnType("datetime");

                entity.Property(e => e.EventName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TournamentId).HasColumnName("TournamentID");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.TournamentId)
                    .HasConstraintName("FK__Event__Tournamen__145C0A3F");
            });

            modelBuilder.Entity<EventDetail>(entity =>
            {
                entity.Property(e => e.EventDetailId).HasColumnName("EventDetailID");

                entity.Property(e => e.EventDetailName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EventDetailOdd).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.EventDetailStatusId).HasColumnName("EventDetailStatusID");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.HasOne(d => d.EventDetailStatus)
                    .WithMany(p => p.EventDetail)
                    .HasForeignKey(d => d.EventDetailStatusId)
                    .HasConstraintName("FK__EventDeta__Event__182C9B23");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventDetail)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK__EventDeta__Event__173876EA");
            });

            modelBuilder.Entity<EventDetailStatus>(entity =>
            {
                entity.Property(e => e.EventDetailStatusId).HasColumnName("EventDetailStatusID");

                entity.Property(e => e.EventDetailStatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.Property(e => e.TournamentId).HasColumnName("TournamentID");

                entity.Property(e => e.TournamentName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
              entity.Property(e => e.Id).HasColumnName("Id");

              entity.Property(e => e.Id)
                .HasMaxLength(200)
                .IsUnicode(false);
            });
    }
    }
}
