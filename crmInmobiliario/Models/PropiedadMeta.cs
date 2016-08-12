using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class PropiedadMeta
    {
        public int IdPropiedad { get; set; }

        public Nullable<int> Desarrollo { get; set; }
        public Nullable<int> Edificio { get; set; }

        [Display(Name = "Tipo de propiedad")]
        public Nullable<int> TipoPropiedad { get; set; }
        [Display(Name = "Tipo de operacion")]
        public Nullable<int> TipoOperacion { get; set; }

        [Display(Name = "Precio de venta")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> VentaPrecio { get; set; }

        [Display(Name = "Precio de renta")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> RentaPrecio { get; set; }

        [Display(Name = "Tarifa diaria de renta")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> RentaTarifaDiaria { get; set; }
        public Nullable<decimal> RentaTarifaSemanal { get; set; }
        public Nullable<decimal> RentaTarifaMensual { get; set; }
        public Nullable<int> RentaEstadiaMinima { get; set; }
        public string Titulo { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        public Nullable<int> Moneda { get; set; }
        public Nullable<int> Recamaras { get; set; }

        [Display(Name = "Preparación baño")]
        public Nullable<bool> PreparacionBanio { get; set; }

        [Display(Name = "Incluye instalación baño")]
        public Nullable<bool> IncluyeInstalacionBanio { get; set; }

        [Display(Name = "Baños")]
        public Nullable<bool> Banios { get; set; }
        public Nullable<int> MedioBanios { get; set; }
        public Nullable<bool> Estacionamiento { get; set; }

        [Display(Name = "Precio de estacionamiento")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> PrecioEstacionamiento { get; set; }

        [Display(Name = "m² de estacionamiento")]
        public Nullable<decimal> M2Estacionamiento { get; set; }
        public Nullable<decimal> PrecioM2Estacionamiento { get; set; }

        [Display(Name = "Cajones de estacionamiento")]
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

        [Display(Name = "m² de interiores")]
        public Nullable<decimal> M2Interiores { get; set; }

        [Display(Name = "Precio m² de interiores")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> PrecioM2Interiores { get; set; }
        public Nullable<bool> Terraza { get; set; }

        [Display(Name = "m² de terraza")]
        public Nullable<decimal> M2Terraza { get; set; }

        [Display(Name = "Precio m² de terraza")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> PrecioM2Terraza { get; set; }
        public Nullable<bool> Bodega { get; set; }

        [Display(Name = "m² de bodega")]
        public Nullable<decimal> M2Bodega { get; set; }

        [Display(Name = "Precio m² de bodega")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> PrecioM2Bodega { get; set; }

        [Display(Name = "Frente del local")]
        public Nullable<decimal> FrenteLocal { get; set; }

        [Display(Name = "Largo del local")]
        public Nullable<decimal> LargoLocal { get; set; }
        public Nullable<int> Acabados { get; set; }

        [Display(Name = "Especifique los acabados")]
        [DataType(DataType.MultilineText)]
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