﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pandemica.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class pandemicaDBEntities1 : DbContext
    {
        public pandemicaDBEntities1()
            : base("name=pandemicaDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clinic> Clinics { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<VaccinationCenter> VaccinationCenters { get; set; }
    }
}
