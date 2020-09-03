﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AugustCodingExercise
{
    public partial class DiscountType
    {
        [Key]
        [StringLength(10)]
        public string DiscountTypeCode { get; set; }
        [StringLength(250)]
        public string TypeDescription { get; set; }
        public bool? AppliesToEmp { get; set; }
        public bool? AppliesToDep { get; set; }
        public bool? AppliesToSpo { get; set; }
        [Column(TypeName = "numeric(9, 5)")]
        public decimal? EmpPercent { get; set; }
        [Column(TypeName = "numeric(9, 5)")]
        public decimal? DepPercent { get; set; }
        [Column(TypeName = "numeric(9, 5)")]
        public decimal? SpoPercent { get; set; }
    }
}