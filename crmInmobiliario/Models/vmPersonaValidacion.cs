using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crmInmobiliario.Models
{
    public class vmPersonaValidacion
    {
        public Personas personas { get; set; }

        public IEnumerable<crmInmobiliario.Models.PersonasValidacion> validaciones { get; set; }
    }
}