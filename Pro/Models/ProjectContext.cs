using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pro.Models;

public partial class ProjectContext : DbContext
{
    public ProjectContext()
    {
    }

    public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NewTable> NewTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ICPU0076\\SQLEXPRESS;Initial Catalog=Project_;Persist Security Info=False;User ID=sa;Password=sql@123;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False;Command Timeout=0");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NewTable>(entity =>
        {
            entity.HasKey(e => e.AccNo).HasName("PK__NewTable__9A20FDBBC8537DBD");

            entity.ToTable("NewTable");

            entity.Property(e => e.AccNo)
                .ValueGeneratedNever()
                .HasColumnName("acc_no");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.Gender)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mobile_no");
            entity.Property(e => e.Pwd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("pwd");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
