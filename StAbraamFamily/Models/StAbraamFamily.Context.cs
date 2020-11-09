﻿ 

namespace StAbraamFamily.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StAbraamEntities : DbContext
    {
        public StAbraamEntities()
            : base("name=StAbraamEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Child> Children { get; set; }
        public virtual DbSet<Clinic> Clinics { get; set; }
        public virtual DbSet<EvaluationLevel> EvaluationLevels { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<FamilyServant> FamilyServants { get; set; }
        public virtual DbSet<FamilyVisit> FamilyVisits { get; set; }
        public virtual DbSet<Father> Fathers { get; set; }
        public virtual DbSet<Hospital> Hospitals { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Servant> Servants { get; set; }
        public virtual DbSet<ServiceAction> ServiceActions { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
