﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AugustCodingExercise
{
    public partial class PayrollDBContext : DbContext
    {
        public PayrollDBContext()
        {
        }

        public PayrollDBContext(DbContextOptions<PayrollDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PayrollRuns> PayrollRuns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Scaffolding:ConnectionString", "Data Source=(local);Initial Catalog=AugustCodingDB;Integrated Security=true");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}