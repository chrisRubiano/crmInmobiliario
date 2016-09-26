namespace crmInmobiliario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PagosMeta
    {
        public int IdPago { get; set; }
        public Nullable<int> Tipo { get; set; }
        public Nullable<int> Propiedad { get; set; }
        public Nullable<int> Persona { get; set; }
        public Nullable<int> Cotizacion { get; set; }
        public Nullable<int> Amortizacion { get; set; }
        public Nullable<System.DateTime> FechaPago { get; set; }
        public Nullable<int> Moneda { get; set; }
        public Nullable<decimal> TipoCambio { get; set; }
        [DataType(DataType.Currency)]
        [Range(0, float.MaxValue, ErrorMessage = "Por favor escriba un numero valido")]
        public Nullable<decimal> Importe { get; set; }
        public string ImporteConLetra { get; set; }

        public virtual Cotizaciones Cotizaciones { get; set; }
        public virtual Monedas Monedas { get; set; }
        public virtual Personas Personas { get; set; }
        public virtual Propiedades Propiedades { get; set; }
        public virtual TiposPago TiposPago { get; set; }
    }
}
