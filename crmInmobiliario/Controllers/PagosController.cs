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

        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }


        // GET: Pagos
        public ActionResult Index()
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "TESORERIA" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                var pagos = db.Pagos.Include(p => p.Cotizaciones).Include(p => p.Monedas).Include(p => p.Personas).Include(p => p.Propiedades).Include(p => p.TiposPago);
                return View(pagos.ToList());
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // GET: Pagos/Details/5
        public ActionResult Details(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "TESORERIA" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
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
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // GET: Pagos/Create
        public ActionResult Create(int id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "TESORERIA" || usuario.UserRoles == "DIR-GENERAL")
            {
                ViewBag.Amortizacion = new SelectList(db.Amortizaciones, "IdAmortizacion", "IdAmortizacion");
                ViewBag.Cotizacion = new SelectList(db.Cotizaciones, "IdCotizacion", "Vendedor");
                ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda");
                ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre");
                ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo");
                ViewBag.Tipo = new SelectList(db.TiposPago, "IdTipoPago", "Tipo");
                ViewBag.FormaPago = new SelectList(db.FormasPago, "IdFormaPago", "FormaPago");

                Amortizaciones amortizacion = db.Amortizaciones.Find(id);
                ViewBag.importePago = String.Format("{0:C}", amortizacion.Importe);
                ViewBag.idCotizacion = amortizacion.Cotizacion;
                return View();
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // POST: Pagos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPago,Tipo,Propiedad,Persona,Cotizacion,Amortizacion,FechaPago,Moneda,TipoCambio,Importe,FormaPago,Efectivo,Transferencia,Cheque")] Pagos pagos, int id)
        {
            if (!pagos.Transferencia.HasValue)
            {
                pagos.Transferencia = 0;
            }
            if (!pagos.Efectivo.HasValue)
            {
                pagos.Efectivo = 0;
            }
            if (!pagos.Cheque.HasValue)
            {
                pagos.Cheque = 0;
            }
            Amortizaciones amortizacion = db.Amortizaciones.Find(id);
            if (ModelState.IsValid && pagos.Efectivo.Value+pagos.Transferencia.Value+pagos.Cheque.Value == amortizacion.Importe.Value)
            {
                //Amortizaciones amortizacion = db.Amortizaciones.Find(id);
                pagos.Amortizacion = amortizacion.IdAmortizacion;
                pagos.Persona = amortizacion.Persona;
                pagos.Propiedad = amortizacion.Propiedad;
                pagos.Tipo = amortizacion.TipoPago;
                pagos.Importe = amortizacion.Importe;
                pagos.FechaPago = DateTime.Today;

                db.Pagos.Add(pagos);
                db.SaveChanges();
                return RedirectToAction("Oficial", "Amortizaciones", new { cotizacion = amortizacion.Cotizacion });
            }

            ViewBag.Amortizacion = new SelectList(db.Amortizaciones, "IdAmortizacion", "IdAmortizacion", pagos.Amortizacion);
            ViewBag.Cotizacion = new SelectList(db.Cotizaciones, "IdCotizacion", "Vendedor", pagos.Cotizacion);
            ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda", pagos.Moneda);
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre", pagos.Persona);
            ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", pagos.Propiedad);
            ViewBag.Tipo = new SelectList(db.TiposPago, "IdTipoPago", "Tipo", pagos.Tipo);

            ViewBag.importePago = String.Format("{0:C}", amortizacion.Importe);
            ViewBag.idCotizacion = amortizacion.Cotizacion;
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
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "TESORERIA" || usuario.UserRoles == "DIR-GENERAL")
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
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // POST: Pagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPago,Tipo,Propiedad,Persona,Cotizacion,Amortizacion,FechaPago,Moneda,TipoCambio,Importe,FormaPago")] Pagos pagos)
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
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "TESORERIA" || usuario.UserRoles == "DIR-GENERAL")
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
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
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
