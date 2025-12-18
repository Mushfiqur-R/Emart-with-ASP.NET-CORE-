using System;
using System.Collections.Generic;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data;

public partial class EcommerceDBContext : DbContext
{
    public EcommerceDBContext(DbContextOptions<EcommerceDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Catagory> Catagories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catagory>(entity =>
        {
            entity.ToTable("Catagory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Catagory).WithMany(p => p.Products)
                .HasForeignKey(d => d.CatagoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Catagory");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
