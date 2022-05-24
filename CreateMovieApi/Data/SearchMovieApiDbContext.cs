using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CreateMovieApi.Models;

namespace CreateMovieApi.Data
{
    public partial class SearchMovieApiDbContext : DbContext
    {
        public SearchMovieApiDbContext()
        {
        }

        public SearchMovieApiDbContext(DbContextOptions<SearchMovieApiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movie { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //warning To protect potentially sensitive information in your connection string, you should move it out of source code.
                //You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration -
                //see https://go.microsoft.com/fwlink/?linkid=2131148.
                //For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SearchMovieApiDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("Movie");

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.MovieId).ValueGeneratedOnAdd();

                entity.Property(e => e.MovieTitle).HasMaxLength(50);

                entity.Property(e => e.ReleaseDate).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}