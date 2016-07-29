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
    
    public partial class Propiedades
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Propiedades()
        {
            this.CaracteristicasPropiedades = new HashSet<CaracteristicasPropiedades>();
            this.Domicilios = new HashSet<Domicilios>();
        }
    
        public int IdPropiedad { get; set; }
        public Nullable<int> Desarrollo { get; set; }
        public Nullable<int> TipoPropiedad { get; set; }
        public Nullable<int> TipoOperacion { get; set; }
        public Nullable<decimal> VentaPrecio { get; set; }
        public Nullable<decimal> RentaPrecio { get; set; }
        public Nullable<decimal> RentaTarifaDiaria { get; set; }
        public Nullable<decimal> RentaTarifaSemanal { get; set; }
        public Nullable<decimal> RentaTarifaMensual { get; set; }
        public Nullable<int> RentaEstadiaMinima { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> Moneda { get; set; }
        public Nullable<int> Recamaras { get; set; }
        public Nullable<int> PreparacionBanio { get; set; }
        public Nullable<bool> IncluyeInstalacionBanio { get; set; }
        public Nullable<int> Banios { get; set; }
        public Nullable<int> MedioBanios { get; set; }
        public Nullable<int> Estacionamientos { get; set; }
        public Nullable<decimal> Construccion { get; set; }
        public Nullable<decimal> Terreno { get; set; }
        public Nullable<decimal> LargoTerreno { get; set; }
        public Nullable<decimal> FrenteTerreno { get; set; }
        public Nullable<int> Acabados { get; set; }
        public string AcabadosEspecifique { get; set; }
        public Nullable<int> Antiguedad { get; set; }
        public Nullable<int> PisoEnQueSeEncuentra { get; set; }
        public Nullable<int> CantidadPisos { get; set; }
        public string MantenimientoMensual { get; set; }
        public string Codigo { get; set; }
        public string Observaciones { get; set; }
        public string Usuario { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string UsuarioUA { get; set; }
        public Nullable<System.DateTime> FechaUA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CaracteristicasPropiedades> CaracteristicasPropiedades { get; set; }
        public virtual Desarrollos Desarrollos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Domicilios> Domicilios { get; set; }
        public virtual Monedas Monedas { get; set; }
        public virtual PropiedadesAcabados PropiedadesAcabados { get; set; }
        public virtual PropiedadesAntiguedad PropiedadesAntiguedad { get; set; }
        public virtual PropiedadesTipo PropiedadesTipo { get; set; }
        public virtual PropiedadesTiposOperacion PropiedadesTiposOperacion { get; set; }
    }
}
