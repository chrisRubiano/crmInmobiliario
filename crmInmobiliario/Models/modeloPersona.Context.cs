﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace crmInmobiliario.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CRMINMOBILIARIOEntities : DbContext
    {
        public CRMINMOBILIARIOEntities()
            : base("name=CRMINMOBILIARIOEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<MediosContacto> MediosContacto { get; set; }
        public virtual DbSet<Municipios> Municipios { get; set; }
        public virtual DbSet<Paises> Paises { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<PersonasGenero> PersonasGenero { get; set; }
        public virtual DbSet<PersonasTipo> PersonasTipo { get; set; }
    }
}
