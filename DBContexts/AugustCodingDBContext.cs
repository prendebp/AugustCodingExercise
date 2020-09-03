﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AugustCodingExercise
{
    public partial class AugustCodingDBContext : DbContext
    {
        public AugustCodingDBContext()
        {
        }

        public AugustCodingDBContext(DbContextOptions<AugustCodingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DeductionType> DeductionType { get; set; }
        public virtual DbSet<DiscountType> DiscountType { get; set; }
        public virtual DbSet<PayrollRuns> PayrollRuns { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonType> PersonType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Scaffolding:ConnectionString", "Data Source=(local);Initial Catalog=AugustCodingDB;Integrated Security=true");

            modelBuilder.Entity<DeductionType>(entity =>
            {
                entity.Property(e => e.TypeDescription).IsFixedLength();
            });

            modelBuilder.Entity<DiscountType>(entity =>
            {
                entity.Property(e => e.TypeDescription).IsFixedLength();
            });

            modelBuilder.Entity<PayrollRuns>(entity =>
            {
                entity.Property(e => e.PayrollId).ValueGeneratedNever();

                entity.HasOne(d => d.DeductionTypeNavigation)
                    .WithMany(p => p.PayrollRuns)
                    .HasForeignKey(d => d.DeductionType)
                    .HasConstraintName("FK_PayrollRuns_DeductionType");

                entity.HasOne(d => d.DiscountTypeNavigation)
                    .WithMany(p => p.PayrollRuns)
                    .HasForeignKey(d => d.DiscountType)
                    .HasConstraintName("FK_PayrollRuns_DiscountType");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PayrollRuns)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PayrollRuns_Person");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasOne(d => d.PersonCodeNavigation)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.PersonCode)
                    .HasConstraintName("FK_Person_PersonType");
            });

            modelBuilder.Entity<PersonType>(entity =>
            {
                entity.Property(e => e.TypeDescription).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}