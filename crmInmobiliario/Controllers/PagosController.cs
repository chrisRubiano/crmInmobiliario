using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using crmInmobiliario.Models;
using crmInmobiliario.Utilidades;

namespace crmInmobiliario.Controllers
{
    [Authorize]
    public class PagosController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        // GET: Pagos
        public ActionResult Index()
        {
            var pagos = db.Pagos.Include(p => p.Cotizaciones).Include(p => p.Monedas).Include(p => p.Personas).Include(p => p.Propiedades).Include(p => p.TiposPago);
            return View(pagos.ToList());
        }

        // GET: Pagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = db.Pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // GET: Pagos/Create
        public ActionResult Create(int id)
        {
            ViewBag.Amortizacion = new SelectList(db.Amortizaciones, "IdAmortizacion", "IdAmortizacion");
            ViewBag.Cotizacion = new SelectList(db.Cotizaciones, "IdCotizacion", "Vendedor");
            ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda");
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre");
            ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo");
            ViewBag.Tipo = new SelectList(db.TiposPago, "IdTipoPago", "Tipo");

            Amortizaciones amortizacion = db.Amortizaciones.Find(id);
            ViewBag.importePago = amortizacion.Importe;
            return View();
        }

        // POST: Pagos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPago,Tipo,Propiedad,Persona,Cotizacion,Amortizacion,FechaPago,Moneda,TipoCambio,Importe")] Pagos pagos, int id)
        {
            if (ModelState.IsValid)
            {
                Amortizaciones amortizacion = db.Amortizaciones.Find(id);
                pagos.Amortizacion = amortizacion.IdAmortizacion;
                pagos.Persona = amortizacion.Persona;
                pagos.Propiedad = amortizacion.Propiedad;

                db.Pagos.Add(pagos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Amortizacion = new SelectList(db.Amortizaciones, "IdAmortizacion", "IdAmortizacion", pagos.Amortizacion);
            ViewBag.Cotizacion = new SelectList(db.Cotizaciones, "IdCotizacion", "Vendedor", pagos.Cotizacion);
            ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda", pagos.Moneda);
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre", pagos.Persona);
            ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", pagos.Propiedad);
            ViewBag.Tipo = new SelectList(db.TiposPago, "IdTipoPago", "Tipo", pagos.Tipo);
            return View(pagos);
        }

        public void Excel()
        {
            var model = db.MediosEnterarse.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "Pagos");
        }

        // GET: Pagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = db.Pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Amortizacion = new SelectList(db.Amortizaciones, "IdAmortizacion", "IdAmortizacion", pagos.Amortizacion);
            ViewBag.Cotizacion = new SelectList(db.Cotizaciones, "IdCotizacion", "Vendedor", pagos.Cotizacion);
            ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda", pagos.Moneda);
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre", pagos.Persona);
            ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", pagos.Propiedad);
            ViewBag.Tipo = new SelectList(db.TiposPago, "IdTipoPago", "Tipo", pagos.Tipo);
            return View(pagos);
        }

        // POST: Pagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPago,Tipo,Propiedad,Persona,Cotizacion,Amortizacion,FechaPago,Moneda,TipoCambio,Importe")] Pagos pagos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Amortizacion = new SelectList(db.Amortizaciones, "IdAmortizacion", "IdAmortizacion", pagos.Amortizacion);
            ViewBag.Cotizacion = new SelectList(db.Cotizaciones, "IdCotizacion", "Vendedor", pagos.Cotizacion);
            ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda", pagos.Moneda);
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre", pagos.Persona);
            ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", pagos.Propiedad);
            ViewBag.Tipo = new SelectList(db.TiposPago, "IdTipoPago", "Tipo", pagos.Tipo);
            return View(pagos);
        }

        // GET: Pagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = db.Pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pagos pagos = db.Pagos.Find(id);
            db.Pagos.Remove(pagos);
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
