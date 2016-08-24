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

        [Display(Name = "Precio de venta")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> VentaPrecio { get; set; }

        public string Titulo { get; set; }

        public Nullable<bool> Estacionamiento { get; set; }

        [Display(Name = "Precio de estacionamiento")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> PrecioEstacionamiento { get; set; }

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


    }
}