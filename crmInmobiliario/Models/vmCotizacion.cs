using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class vmCotizacion
    {
        public Personas personas { get; set; }
        public Propiedades propiedades { get; set; }
        public Cotizaciones cotizaciones { get; set; }
        public Amortizaciones amortizaciones { get; set; }
    }
}