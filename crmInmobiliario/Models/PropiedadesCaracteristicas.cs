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
    
    public partial class PropiedadesCaracteristicas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PropiedadesCaracteristicas()
        {
            this.CaracteristicasPropiedades = new HashSet<CaracteristicasPropiedades>();
        }
    
        public int IdCaracteristica { get; set; }
        public Nullable<int> Categoria { get; set; }
        public string Caracteristica { get; set; }
        public string Clave { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CaracteristicasPropiedades> CaracteristicasPropiedades { get; set; }
        public virtual PropiedadesCaracteristicasCategorias PropiedadesCaracteristicasCategorias { get; set; }
    }
}
