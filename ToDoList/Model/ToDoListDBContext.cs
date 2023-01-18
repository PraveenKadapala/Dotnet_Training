using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ToDoList.Model
{
    public partial class ToDoListDBContext : DbContext
    {
        public ToDoListDBContext()
        {
        }

        public ToDoListDBContext(DbContextOptions<ToDoListDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; } = null!;
        public virtual DbSet<ToDo> ToDos { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=dotnet-traning.cdgkfoacvf6u.us-east-1.rds.amazonaws.com;user id=admin;password=mysql1234;database=ToDoListDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<ToDo>(entity =>
            {
                entity.ToTable("ToDo");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "userId_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Approval).HasColumnName("approval");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Task)
                    .HasMaxLength(45)
                    .HasColumnName("task");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ToDos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Admin).HasColumnName("admin");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
