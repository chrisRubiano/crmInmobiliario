using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using crmInmobiliario.Models;
using System.IO;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace crmInmobiliario.Controllers
{
    [Authorize]
    public class CotizacionesController : Controller
    {
        private CRMINMOBILIARIOEntities10 db = new CRMINMOBILIARIOEntities10();

        // GET: Cotizaciones
        public ActionResult Index()
        {
            var cotizaciones = db.Cotizaciones.Include(c => c.Personas).Include(c => c.Propiedades).OrderByDescending(c => c.IdCotizacion);
            return View(cotizaciones.ToList());
        }

        // GET: Cotizaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cotizaciones cotizaciones = db.Cotizaciones.Find(id);
            if (cotizaciones == null)
            {
                return HttpNotFound();
            }
            return View(cotizaciones);
        }

        public void Excel()
        {
            var model = db.Cotizaciones.ToList();

            Export export = new Export();
            export.ToExcel(Response, model);
        }

        //helper class
        public class Export
        {
            public void ToExcel(HttpResponseBase Response, object clientsList)
            {
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = clientsList;
                grid.DataBind();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=Cotizaciones.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }

        // GET: Cotizaciones/Create
        public ActionResult Create(int? idPropiedad)
        {
            Cotizaciones cotizacion = new Cotizaciones();
            if (idPropiedad.HasValue)
            {
                var propiedad = db.Propiedades.Where(p => p.IdPropiedad == idPropiedad).FirstOrDefault();
                cotizacion.Propiedad = idPropiedad.Value;
                cotizacion.PrecioFinalVenta = propiedad.VentaPrecio;
                ViewBag.codigo = propiedad.Codigo;
            }

         ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo");

            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre");
            return View(cotizacion);
        }

        // POST: Cotizaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCotizacion,Propiedad,Persona,FechaCotizacion,PrecioFinalVenta,PorcentajeEnganche,Enganche,Parcialidades,PorcentajeMensualidades,PagoMensual,Vendedor")] Cotizaciones cotizaciones, int? idPropiedad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (idPropiedad.HasValue)
                    {
                        cotizaciones.Propiedad = idPropiedad.Value;
                    }
                    cotizaciones.Vendedor = User.Identity.GetUserId().ToString();
                    cotizaciones.FechaCotizacion = DateTime.Now;
                    db.Cotizaciones.Add(cotizaciones);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No es posible guardar los cambios, intente mas tarde. Si los cambios persisten favor de contactarse con un adminsitrador");
            }

            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre", cotizaciones.Persona);
            if (!string.IsNullOrEmpty(ViewBag.codigo))
            {

            }
            else
            {
                ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", cotizaciones.Propiedad);
            }
            return View(cotizaciones);
        }

        // GET: Cotizaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cotizaciones cotizaciones = db.Cotizaciones.Find(id);
            if (cotizaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre", cotizaciones.Persona);
            ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", cotizaciones.Propiedad);
            return View(cotizaciones);
        }

        // POST: Cotizaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCotizacion,Propiedad,Persona,FechaCotizacion,PrecioFinalVenta,PorcentajeEnganche,Enganche,Parcialidades,PorcentajeMensualidades,PagoMensual")] Cotizaciones cotizaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cotizaciones).State = EntityState.Modified;
                cotizaciones.FechaCotizacion = DateTime.Now;
                cotizaciones.Vendedor = User.Identity.GetUserId().ToString();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre", cotizaciones.Persona);
            ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", cotizaciones.Propiedad);
            return View(cotizaciones);
        }

        // GET: Cotizaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cotizaciones cotizaciones = db.Cotizaciones.Find(id);
            if (cotizaciones == null)
            {
                return HttpNotFound();
            }
            return View(cotizaciones);
        }

        // POST: Cotizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cotizaciones cotizaciones = db.Cotizaciones.Find(id);
            db.Cotizaciones.Remove(cotizaciones);
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
