using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using crmInmobiliario.Models;
using System.Data.Entity.Core.Objects;

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

            if (persona != 0)
            {
                amortizaciones = amortizaciones.Where(a => a.Persona == persona);
            }
            if (propiedad != 0)
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
        public ActionResult Create(int? idCotizacion)
        {
            if (idCotizacion.HasValue && idCotizacion != 0)
            {
                ViewBag.Cotizacion = idCotizacion;
            }
            ViewBag.TipoPago = new SelectList(db.TiposPago, "IdTipoPago", "Tipo");
            return View();
        }

        // POST: Amortizaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAmortizacion,TipoPago,Persona,Propiedad,Cotizacion,FechaProgramado,Importe,EstaPagado")] Amortizaciones amortizaciones, int? idCotizacion)
        {
            if (ModelState.IsValid)
            {
                if (idCotizacion.HasValue)
                {
                    amortizaciones.Cotizacion = idCotizacion;
                }
                db.Amortizaciones.Add(amortizaciones);
                db.SaveChanges();
                return RedirectToAction("Filtro", new { cotizacion = amortizaciones.Cotizacion });
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
            ViewBag.Tipo = amortizaciones.TiposPago.Tipo;
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
                if (amortizaciones.TipoPago == 1)
                {
                    var listaEnganches = from e in db.Amortizaciones.AsNoTracking() where e.TipoPago == 1 select e;
                    listaEnganches = listaEnganches.Where(e => e.Cotizacion == amortizaciones.Cotizacion);
                    decimal totalEnganche;
                    decimal otrosEnganches;
                    var cotizacion = db.Cotizaciones.Find(amortizaciones.Cotizacion);
                    totalEnganche = cotizacion.Enganche.Value;
                    otrosEnganches = (totalEnganche - amortizaciones.Importe.Value)/ (listaEnganches.Count()-1);

                    foreach (var item in listaEnganches)
                    {
                        if (item.IdAmortizacion != amortizaciones.IdAmortizacion)
                        {
                            item.Importe = otrosEnganches;
                            db.Entry(item).State = EntityState.Modified;
                        }
                    }
                }



                DateTime nuevaFecha = amortizaciones.FechaProgramado.Value;
                var listaAmortizaciones = from a in db.Amortizaciones.AsNoTracking() where a.Cotizacion == amortizaciones.Cotizacion select a;
                listaAmortizaciones = listaAmortizaciones.Where(a => a.IdAmortizacion > amortizaciones.IdAmortizacion);

                foreach (var item in listaAmortizaciones)
                {
                    if (item.TipoPago == 2)
                    {
                        nuevaFecha = nuevaFecha.AddMonths(1);
                        item.FechaProgramado = nuevaFecha;
                        db.Entry(item).State = EntityState.Modified;
                    }
                }
                db.Entry(amortizaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Filtro");
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
            return RedirectToAction("Filtro");
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
