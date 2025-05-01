using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CMS.Models;

public partial class NeondbContext : DbContext
{
    public NeondbContext()
    {
    }

    public NeondbContext(DbContextOptions<NeondbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Meter> Meters { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Reading> Readings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-calm-shadow-a4b2xfme-pooler.us-east-1.aws.neon.tech;Database=neondb;Username=neondb_owner;Password=npg_OcH9IYfr1Fyv");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Billid).HasName("bills_pkey");

            entity.ToTable("bills");

            entity.Property(e => e.Billid)
                .HasMaxLength(10)
                .HasColumnName("billid");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Duedate).HasColumnName("duedate");
            entity.Property(e => e.Readingid)
                .HasMaxLength(10)
                .HasColumnName("readingid");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Unpaid'::character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.Reading).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Readingid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bills_readingid_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.Customerid)
                .HasMaxLength(10)
                .HasColumnName("customerid");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Contact)
                .HasMaxLength(15)
                .HasColumnName("contact");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Meter>(entity =>
        {
            entity.HasKey(e => e.Meterid).HasName("meters_pkey");

            entity.ToTable("meters");

            entity.Property(e => e.Meterid)
                .HasMaxLength(10)
                .HasColumnName("meterid");
            entity.Property(e => e.Customerid)
                .HasMaxLength(10)
                .HasColumnName("customerid");
            entity.Property(e => e.Meternumber)
                .HasMaxLength(20)
                .HasColumnName("meternumber");
            entity.Property(e => e.Metertype)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Residential'::character varying")
                .HasColumnName("metertype");

            entity.HasOne(d => d.Customer).WithMany(p => p.Meters)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("meters_customerid_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.Property(e => e.Paymentid)
                .HasMaxLength(10)
                .HasColumnName("paymentid");
            entity.Property(e => e.Amountpaid)
                .HasPrecision(10, 2)
                .HasColumnName("amountpaid");
            entity.Property(e => e.Billid)
                .HasMaxLength(10)
                .HasColumnName("billid");
            entity.Property(e => e.Paymentdate).HasColumnName("paymentdate");
            entity.Property(e => e.Paymentmethod)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Cash'::character varying")
                .HasColumnName("paymentmethod");

            entity.HasOne(d => d.Bill).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Billid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_billid_fkey");
        });

        modelBuilder.Entity<Reading>(entity =>
        {
            entity.HasKey(e => e.Readingid).HasName("readings_pkey");

            entity.ToTable("readings");

            entity.Property(e => e.Readingid)
                .HasMaxLength(10)
                .HasColumnName("readingid");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Meterid)
                .HasMaxLength(10)
                .HasColumnName("meterid");
            entity.Property(e => e.Unitsconsumed).HasColumnName("unitsconsumed");

            entity.HasOne(d => d.Meter).WithMany(p => p.Readings)
                .HasForeignKey(d => d.Meterid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("readings_meterid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
