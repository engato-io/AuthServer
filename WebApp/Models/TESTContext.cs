using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApp.Models
{
    public partial class TESTContext : DbContext
    {
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonDetails> PersonDetails { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        public TESTContext(DbContextOptions<TESTContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Genre)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Genre_Status");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.PersonId).ValueGeneratedNever();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Person_Status");
            });

            modelBuilder.Entity<PersonDetails>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("PK_PersonDetails");

                entity.Property(e => e.PersonId).ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.PersonDetails)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PersonDetails_Genre");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.PersonDetails)
                    .HasForeignKey<PersonDetails>(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PersonDetails_Person");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });
        }
    }
}