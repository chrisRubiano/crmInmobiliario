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

    [MetadataType(typeof(CotizacionesMeta))]
    public partial class Cotizaciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cotizaciones()
        {
            this.Pagos = new HashSet<Pagos>();
        }
    
        public int IdCotizacion { get; set; }
        public Nullable<int> Propiedad { get; set; }
        public Nullable<int> Persona { get; set; }
        public Nullable<System.DateTime> FechaCotizacion { get; set; }
        public Nullable<decimal> PrecioFinalVenta { get; set; }
        public Nullable<decimal> PorcentajeEnganche { get; set; }
        public Nullable<decimal> Enganche { get; set; }
        public Nullable<int> Parcialidades { get; set; }
        public Nullable<decimal> PorcentajeMensualidades { get; set; }
        public Nullable<decimal> PagoMensual { get; set; }
        public string Vendedor { get; set; }
    
        public virtual Personas Personas { get; set; }
        public virtual Propiedades Propiedades { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pagos> Pagos { get; set; }
    }
}
