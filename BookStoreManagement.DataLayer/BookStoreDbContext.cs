using BookStoreManagement.Entities.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagement.DataLayer
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookTable> Book { get; set; }
        public virtual DbSet<AuthorTable> Author { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BookTable>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Book");
                entity.Property(e => e.Name).HasMaxLength(2000);
                entity.Property(e => e.AuthorId).IsRequired().HasMaxLength(200);
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuthorTable>(entity =>
            {
                entity.ToTable("Author");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(2000);
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

        }

    }
}
