﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AugustCodingExercise
{
    public partial class CalcDBContext : DbContext
    {
        public CalcDBContext()
        {
        }

        public CalcDBContext(DbContextOptions<CalcDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DeductionType> DeductionType { get; set; }
        public virtual DbSet<DiscountType> DiscountType { get; set; }

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}