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

        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Display(Name = "Código Corto")]
        public string CodigoCorto { get; set; }

        [Display(Name = "Precio de venta")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> VentaPrecio { get; set; }

        public string Titulo { get; set; }

        public Nullable<bool> Estacionamiento { get; set; }

        [Display(Name = "Precio de estacionamiento")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> PrecioEstacionamiento { get; set; }

        [Display(Name = "Cajones de Estacionamiento")]        
        public Nullable<int> CajonesEstacionamiento { get; set; }

        [Required]
        [Display(Name = "Interiores (m²)")]
        public Nullable<decimal> M2Interiores { get; set; }

        [Required]
        [Display(Name = "Precio m² de interiores")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> PrecioM2Interiores { get; set; }
        public Nullable<bool> Terraza { get; set; }

        [Display(Name = "Terraza (m²)")]
        public Nullable<decimal> M2Terraza { get; set; }

        [Display(Name = "Precio m² de Terraza")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> PrecioM2Terraza { get; set; }
        public Nullable<bool> Bodega { get; set; }

        [Display(Name = "Bodega (m²)")]
        public Nullable<decimal> M2Bodega { get; set; }

        [Display(Name = "Precio m² de bodega")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> PrecioM2Bodega { get; set; }

        [Required]
        [Display(Name = "Frente del local")]
        public Nullable<decimal> FrenteLocal { get; set; }

        [Required]
        [Display(Name = "Largo del local")]
        public Nullable<decimal> LargoLocal { get; set; }
        public Nullable<int> Acabados { get; set; }

        [Display(Name = "Especifique los acabados")]
        [DataType(DataType.MultilineText)]
        public string AcabadosEspecifique { get; set; }

        [Required]
        public string Nivel { get; set; }

        [Display(Name = "Tipo de Baños")]
        public Nullable<int> TipoBanio { get; set; }

        [Display(Name = "Num de Baños")]
        public Nullable<double> NumBanios { get; set; }


    }
}