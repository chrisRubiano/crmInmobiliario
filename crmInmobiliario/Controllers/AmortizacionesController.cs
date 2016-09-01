using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using crmInmobiliario.Models;

namespace crmInmobiliario.Controllers
{
    public class AmortizacionesController : Controller
    {
        private CRMINMOBILIARIOEntities2 db = new CRMINMOBILIARIOEntities2();

        // GET: Amortizaciones
        public ActionResult Index()
        {
            var amortizaciones = db.Amortizaciones.Include(a => a.TiposPago);
            return View(amortizaciones.ToList());
        }

        public ActionResult Filtro(int persona = 0, int propiedad = 0, int cotizacion = 0)
        {
            var amortizaciones = db.Amortizaciones.Include(a => a.TiposPago).Where(a => a.EstaPagado.Value == false);

            if (persona!=0)
            {
                amortizaciones = amortizaciones.Where(a => a.Persona == persona);
            }
            if (propiedad!=0)
            {
                amortizaciones = amortizaciones.Where(a => a.Propiedad == propiedad);
            }
            if (cotizacion != 0)
            {
                amortizaciones = amortizaciones.Where(a => a.Cotizacion == cotizacion);
            }

            ViewBag.persona = new SelectList(db.Personas, "IdPersona", "Nombre");
            ViewBag.propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo");
            return View(amortizaciones.ToList());
        }

        public ActionResult ConfPago(int? idAmortizacion)
        {
            if (idAmortizacion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vmCotizacion vmcotizacion = new vmCotizacion();
            Amortizaciones amortizaciones = db.Amortizaciones.Find(idAmortizacion);
            vmcotizacion.personas = db.Personas.Find(amortizaciones.Persona);
            vmcotizacion.propiedades = db.Propiedades.Find(amortizaciones.Propiedad);
            vmcotizacion.amortizaciones = amortizaciones;
            if (amortizaciones == null)
            {
                return HttpNotFound();
            }
            return View(vmcotizacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfPago([Bind(Include = "EstaPagado")] Amortizaciones amortizacion ,int idAmortizacion)
        {
            try
            {
                amortizacion = db.Amortizaciones.Where(a => a.IdAmortizacion == idAmortizacion).FirstOrDefault();
                db.Amortizaciones.Attach(amortizacion);
                db.Entry(amortizacion).State = EntityState.Modified;
                amortizacion.EstaPagado = true;
                db.SaveChanges();
                return RedirectToAction("Filtro", "Amortizaciones", new { cotizacion = amortizacion.Cotizacion.Value });
            }
            catch (Exception)
            {
                    
            }
            return RedirectToAction("Filtro", "Amortizaciones");
        }


        // GET: Amortizaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amortizaciones amortizaciones = db.Amortizaciones.Find(id);
            if (amortizaciones == null)
            {
                return HttpNotFound();
            }
            return View(amortizaciones);
        }

        // GET: Amortizaciones/Create
        public ActionResult Create()
        {
            ViewBag.TipoPago = new SelectList(db.TiposPago, "IdTipoPago", "Tipo");
            return View();
        }

        // POST: Amortizaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAmortizacion,TipoPago,Persona,Propiedad,Cotizacion,FechaProgramado,Importe,EstaPagado")] Amortizaciones amortizaciones)
        {
            if (ModelState.IsValid)
            {
                db.Amortizaciones.Add(amortizaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoPago = new SelectList(db.TiposPago, "IdTipoPago", "Tipo", amortizaciones.TipoPago);
            return View(amortizaciones);
        }

        // GET: Amortizaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amortizaciones amortizaciones = db.Amortizaciones.Find(id);
            if (amortizaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoPago = new SelectList(db.TiposPago, "IdTipoPago", "Tipo", amortizaciones.TipoPago);
            return View(amortizaciones);
        }

        // POST: Amortizaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAmortizacion,TipoPago,Persona,Propiedad,Cotizacion,FechaProgramado,Importe,EstaPagado")] Amortizaciones amortizaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amortizaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoPago = new SelectList(db.TiposPago, "IdTipoPago", "Tipo", amortizaciones.TipoPago);
            return View(amortizaciones);
        }

        // GET: Amortizaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amortizaciones amortizaciones = db.Amortizaciones.Find(id);
            if (amortizaciones == null)
            {
                return HttpNotFound();
            }
            return View(amortizaciones);
        }

        // POST: Amortizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Amortizaciones amortizaciones = db.Amortizaciones.Find(id);
            db.Amortizaciones.Remove(amortizaciones);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
