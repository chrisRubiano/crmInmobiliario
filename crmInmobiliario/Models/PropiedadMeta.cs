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
        //[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public Nullable<decimal> VentaPrecio { get; set; }

        public string Titulo { get; set; }

        public Nullable<bool> Estacionamiento { get; set; }

        [Display(Name = "Precio de estacionamiento")]
        [DataType(DataType.Currency)]
        //[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        [Range(0, float.MaxValue, ErrorMessage = "Por favor escriba un numero valido")]
        public Nullable<decimal> PrecioEstacionamiento { get; set; }

        [Display(Name = "Cajones de Estacionamiento")]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor escriba un numero valido")]
        public Nullable<int> CajonesEstacionamiento { get; set; }

        [Required]
        [Display(Name = "Interiores (m²)")]
        [Range(0, float.MaxValue, ErrorMessage = "Por favor escriba un numero valido")]
        public Nullable<decimal> M2Interiores { get; set; }

        [Required]
        [Display(Name = "Precio m² de interiores")]
        [DataType(DataType.Currency)]
        //[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        [Range(0, float.MaxValue, ErrorMessage = "Por favor escriba un numero valido")]
        public Nullable<decimal> PrecioM2Interiores { get; set; }
        public Nullable<bool> Terraza { get; set; }

        [Display(Name = "Terraza (m²)")]
        [Range(0, float.MaxValue, ErrorMessage = "Por favor escriba un numero valido")]
        public Nullable<decimal> M2Terraza { get; set; }

        [Display(Name = "Precio m² de Terraza")]
        [DataType(DataType.Currency)]
        //[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        [Range(0, float.MaxValue, ErrorMessage = "Por favor escriba un numero valido")]
        public Nullable<decimal> PrecioM2Terraza { get; set; }
        public Nullable<bool> Bodega { get; set; }

        [Display(Name = "Bodega (m²)")]
        public Nullable<decimal> M2Bodega { get; set; }

        [Display(Name = "Precio m² de bodega")]
        [DataType(DataType.Currency)]
        [Range(0, float.MaxValue, ErrorMessage = "Por favor escriba un numero valido")]
        //[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public Nullable<decimal> PrecioM2Bodega { get; set; }

        [Required]
        [Display(Name = "Frente del local")]
        [Range(0, float.MaxValue, ErrorMessage = "Por favor escriba un numero valido")]
        public Nullable<decimal> FrenteLocal { get; set; }

        [Required]
        [Display(Name = "Largo del local")]
        [Range(0, float.MaxValue, ErrorMessage = "Por favor escriba un numero valido")]
        public Nullable<decimal> LargoLocal { get; set; }
        public Nullable<int> Acabados { get; set; }

        [Display(Name = "Especifique los acabados")]
        [DataType(DataType.MultilineText)]
        public string AcabadosEspecifique { get; set; }

        [Required]
        public string Nivel { get; set; }

        [Display(Name = "Tipo de Baños")]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor escriba un numero valido")]
        public Nullable<int> TipoBanio { get; set; }

        [Display(Name = "Num de Baños")]
        [Range(0, float.MaxValue, ErrorMessage = "Por favor escriba un numero valido")]
        public Nullable<double> NumBanios { get; set; }


    }
}