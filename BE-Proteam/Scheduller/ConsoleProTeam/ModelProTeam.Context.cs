﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleProTeam
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProteamEntities : DbContext
    {
        public ProteamEntities()
            : base("name=ProteamEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<Jenjab> Jenjabs { get; set; }
        public DbSet<Kelompok> Kelompoks { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<Manday> Mandays { get; set; }
        public DbSet<ResourceEmployee> ResourceEmployees { get; set; }
        public DbSet<Skillset> Skillsets { get; set; }
        public DbSet<UnitProfiling> UnitProfilings { get; set; }
    }
}