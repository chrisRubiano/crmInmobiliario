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
    
    public partial class CRMINMOBILIARIOEntities7 : DbContext
    {
        public CRMINMOBILIARIOEntities7()
            : base("name=CRMINMOBILIARIOEntities7")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CaracteristicasPropiedades> CaracteristicasPropiedades { get; set; }
        public virtual DbSet<Configuraciones> Configuraciones { get; set; }
        public virtual DbSet<Cotizaciones> Cotizaciones { get; set; }
        public virtual DbSet<Desarrollos> Desarrollos { get; set; }
        public virtual DbSet<Documentos> Documentos { get; set; }
        public virtual DbSet<Domicilios> Domicilios { get; set; }
        public virtual DbSet<DomiciliosCategoria> DomiciliosCategoria { get; set; }
        public virtual DbSet<DomiciliosTipo> DomiciliosTipo { get; set; }
        public virtual DbSet<Edificios> Edificios { get; set; }
        public virtual DbSet<EdificiosDetalle> EdificiosDetalle { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<Fotografias> Fotografias { get; set; }
        public virtual DbSet<MediosContacto> MediosContacto { get; set; }
        public virtual DbSet<MediosEnterarse> MediosEnterarse { get; set; }
        public virtual DbSet<Monedas> Monedas { get; set; }
        public virtual DbSet<Municipios> Municipios { get; set; }
        public virtual DbSet<Notas> Notas { get; set; }
        public virtual DbSet<Paises> Paises { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<PersonasCategoria> PersonasCategoria { get; set; }
        public virtual DbSet<PersonasGenero> PersonasGenero { get; set; }
        public virtual DbSet<PersonasIntereses> PersonasIntereses { get; set; }
        public virtual DbSet<PersonasTipo> PersonasTipo { get; set; }
        public virtual DbSet<Propiedades> Propiedades { get; set; }
        public virtual DbSet<PropiedadesAcabados> PropiedadesAcabados { get; set; }
        public virtual DbSet<PropiedadesAntiguedad> PropiedadesAntiguedad { get; set; }
        public virtual DbSet<PropiedadesCaracteristicas> PropiedadesCaracteristicas { get; set; }
        public virtual DbSet<PropiedadesCaracteristicasCategorias> PropiedadesCaracteristicasCategorias { get; set; }
        public virtual DbSet<PropiedadesEstatus> PropiedadesEstatus { get; set; }
        public virtual DbSet<PropiedadesSistemaAC> PropiedadesSistemaAC { get; set; }
        public virtual DbSet<PropiedadesTipo> PropiedadesTipo { get; set; }
        public virtual DbSet<PropiedadesTiposOperacion> PropiedadesTiposOperacion { get; set; }
        public virtual DbSet<ProspectosPropiedades> ProspectosPropiedades { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tareas> Tareas { get; set; }
        public virtual DbSet<TareasCategorias> TareasCategorias { get; set; }
        public virtual DbSet<TareasEstatus> TareasEstatus { get; set; }

        internal object GetModel()
        {
            throw new NotImplementedException();
        }
    }
}
