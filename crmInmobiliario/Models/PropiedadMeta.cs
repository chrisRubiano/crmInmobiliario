using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class PropiedadMeta
    {
        public int IdPropiedad { get; set; }

        public Nullable<int> Desarrollo { get; set; }
        public Nullable<int> Edificio { get; set; }
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
        public Nullable<bool> PreparacionBanio { get; set; }
        public Nullable<bool> IncluyeInstalacionBanio { get; set; }
        public Nullable<bool> Banios { get; set; }
        public Nullable<int> MedioBanios { get; set; }
        public Nullable<bool> Estacionamiento { get; set; }
        public Nullable<decimal> PrecioEstacionamiento { get; set; }
        public Nullable<decimal> M2Estacionamiento { get; set; }
        public Nullable<decimal> PrecioM2Estacionamiento { get; set; }
        public Nullable<int> CajonesEstacionamiento { get; set; }
        public Nullable<int> CajonesAdicionales { get; set; }
        public Nullable<decimal> M2CajonAdicional { get; set; }
        public Nullable<decimal> PrecioM2CajonAdicional { get; set; }
        public Nullable<decimal> Terreno { get; set; }
        public Nullable<decimal> M2Terreno { get; set; }
        public Nullable<decimal> PrecioM2Terreno { get; set; }
        public Nullable<decimal> LargoTerreno { get; set; }
        public Nullable<decimal> FrenteTerreno { get; set; }
        public Nullable<decimal> Construccion { get; set; }
        public Nullable<decimal> M2Interiores { get; set; }
        public Nullable<decimal> PrecioM2Interiores { get; set; }
        public Nullable<bool> Terraza { get; set; }
        public Nullable<decimal> M2Terraza { get; set; }
        public Nullable<decimal> PrecioM2Terraza { get; set; }
        public Nullable<bool> Bodega { get; set; }
        public Nullable<decimal> M2Bodega { get; set; }
        public Nullable<decimal> PrecioM2Bodega { get; set; }
        public Nullable<decimal> FrenteLocal { get; set; }
        public Nullable<decimal> LargoLocal { get; set; }
        public Nullable<int> Acabados { get; set; }
        public string AcabadosEspecifique { get; set; }
        public Nullable<int> Antiguedad { get; set; }
        public string Nivel { get; set; }
        public Nullable<int> Niveles { get; set; }
        public Nullable<int> SistemaAC { get; set; }
        public string MantenimientoMensual { get; set; }
        public Nullable<bool> Reglamento { get; set; }
        public string URLReglamento { get; set; }
        public Nullable<int> Consecutivo { get; set; }
        public string Codigo { get; set; }
        public string Observaciones { get; set; }
        public string Usuario { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string UsuarioUA { get; set; }
        public Nullable<System.DateTime> FechaUA { get; set; }
        public Nullable<int> Estatus { get; set; }
        public string Uso1 { get; set; }
        public string Uso2 { get; set; }
        public string Uso3 { get; set; }
        public string Uso4 { get; set; }
        public string Uso5 { get; set; }

    }
}