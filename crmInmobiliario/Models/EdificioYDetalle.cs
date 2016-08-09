using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class EdificioYDetalle
    {
        public Edificios edificios { get; set; }
        public Desarrollos desarrollos { get; set; }
        public IEnumerable<crmInmobiliario.Models.EdificiosDetalle> edificiosdetalle { get; set; }

    }
}