//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(Persona))]
    public partial class Personas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personas()
        {
            this.Cotizaciones = new HashSet<Cotizaciones>();
            this.Documentos = new HashSet<Documentos>();
            this.Domicilios = new HashSet<Domicilios>();
            this.Notas = new HashSet<Notas>();
        }
    
        public int IdPersona { get; set; }
        public Nullable<int> Categoria { get; set; }
        public int Tipo { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public Nullable<int> Genero { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public Nullable<int> MedioContacto { get; set; }
        public Nullable<int> Interes { get; set; }
        public Nullable<int> CategoriaInteres { get; set; }
        public string Usuario { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string UsuarioUA { get; set; }
        public Nullable<System.DateTime> FechaUA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cotizaciones> Cotizaciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documentos> Documentos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Domicilios> Domicilios { get; set; }
        public virtual MediosContacto MediosContacto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notas> Notas { get; set; }
        public virtual PersonasCategoria PersonasCategoria { get; set; }
        public virtual PersonasGenero PersonasGenero { get; set; }
        public virtual PersonasIntereses PersonasIntereses { get; set; }
        public virtual PersonasTipo PersonasTipo { get; set; }
        public virtual PropiedadesTipo PropiedadesTipo { get; set; }
    }
}
