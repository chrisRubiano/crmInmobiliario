using crmInmobiliario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crmInmobiliario.Controllers
{
    public class ConsultaClienteController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();
        // GET: ConsultaCliente
        public ActionResult Index(int? id, string rfc = "")
        {
            if (!string.IsNullOrWhiteSpace(rfc) && id.HasValue)
            {
                var persona = db.Personas.Where(p => p.IdPersona == id).FirstOrDefault();
                if (persona.RFC == rfc)
                {
                    var amortizaciones = db.Amortizaciones.Where(a => a.Tipo.Equals("O")).Where(a => a.Persona == id);

                    ViewBag.nombre = persona.NombreCompleto;
                    ViewBag.rfc = persona.RFC;
                    ViewBag.codigo = persona.CodigoPersona;

                    return View(amortizaciones);
                }else
                {
                    var amortizaciones = db.Amortizaciones.Where(a => a.Tipo.Equals("O")).Where(a => a.Persona == id).GroupBy(a => a.Cotizacion, (key, g) => g.OrderBy(a => a.FechaProgramado).FirstOrDefault());
                    return View(amortizaciones);
                }
            }
            else
            {
                var amortizaciones = db.Amortizaciones.Where(a => a.Tipo.Equals("O")).Where(a => a.Persona == id).GroupBy(a => a.Cotizacion, (key, g) => g.OrderBy(a => a.FechaProgramado).FirstOrDefault());
                return View(amortizaciones);
            }
        }

        // GET: ConsultaCliente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ConsultaCliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConsultaCliente/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ConsultaCliente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ConsultaCliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ConsultaCliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ConsultaCliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
