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
    public class CotizacionesController : Controller
    {
        private CRMINMOBILIARIOEntities7 db = new CRMINMOBILIARIOEntities7();

        // GET: Cotizaciones
        public ActionResult Index()
        {
            var cotizaciones = db.Cotizaciones.Include(c => c.Personas).Include(c => c.Propiedades);
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
        public ActionResult Create(int? id)
        {
            if (id.HasValue)
            {
                ViewBag.Propiedad = id;
            }
            else
            {
                ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo");
            }
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre");
            return View();
        }

        // POST: Cotizaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCotizacion,Propiedad,Persona,FechaCotizacion,PrecioFinalVenta,PorcentajeEnganche,Enganche,Parcialidades,PorcentajeMensualidades,PagoMensual,Vendedor")] Cotizaciones cotizaciones)
        {
            try
            {
                if (ModelState.IsValid)
                {
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
            ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", cotizaciones.Propiedad);
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
