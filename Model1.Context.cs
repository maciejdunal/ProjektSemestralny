﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjektSemestralny
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Wypozyczalnia_Gier_komputerowychEntities : DbContext
    {
        public Wypozyczalnia_Gier_komputerowychEntities()
            : base("name=Wypozyczalnia_Gier_komputerowychEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Gry> Gries { get; set; }
        public virtual DbSet<Klienci> Kliencis { get; set; }
        public virtual DbSet<Pracownicy> Pracownicies { get; set; }
        public virtual DbSet<Wypozyczenia> Wypozyczenias { get; set; }
        public virtual DbSet<Zwroty> Zwroties { get; set; }
    }
}
