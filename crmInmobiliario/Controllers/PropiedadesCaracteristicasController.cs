﻿using System;
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

namespace crmInmobiliario.Controllers
{
    [Authorize]
    public class PropiedadesCaracteristicasController : Controller
    {
        private CRMINMOBILIARIOEntities7 db = new CRMINMOBILIARIOEntities7();

        // GET: PropiedadesCaracteristicas
        public ActionResult Index()
        {
            var propiedadesCaracteristicas = db.PropiedadesCaracteristicas.Include(p => p.PropiedadesCaracteristicasCategorias);
            return View(propiedadesCaracteristicas.ToList());
        }

        // GET: PropiedadesCaracteristicas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropiedadesCaracteristicas propiedadesCaracteristicas = db.PropiedadesCaracteristicas.Find(id);
            if (propiedadesCaracteristicas == null)
            {
                return HttpNotFound();
            }
            return View(propiedadesCaracteristicas);
        }

        public void Excel()
        {
            var model = db.PropiedadesCaracteristicas.ToList();

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
                Response.AddHeader("content-disposition", "attachment; filename=Amenidades.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }

        // GET: PropiedadesCaracteristicas/Create
        public ActionResult Create()
        {
            ViewBag.Categoria = new SelectList(db.PropiedadesCaracteristicasCategorias, "IdCategoria", "Categoria");
            return View();
        }

        // POST: PropiedadesCaracteristicas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCaracteristica,Categoria,Caracteristica,Clave")] PropiedadesCaracteristicas propiedadesCaracteristicas)
        {
            if (ModelState.IsValid)
            {
                db.PropiedadesCaracteristicas.Add(propiedadesCaracteristicas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categoria = new SelectList(db.PropiedadesCaracteristicasCategorias, "IdCategoria", "Categoria", propiedadesCaracteristicas.Categoria);
            return View(propiedadesCaracteristicas);
        }

        // GET: PropiedadesCaracteristicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropiedadesCaracteristicas propiedadesCaracteristicas = db.PropiedadesCaracteristicas.Find(id);
            if (propiedadesCaracteristicas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categoria = new SelectList(db.PropiedadesCaracteristicasCategorias, "IdCategoria", "Categoria", propiedadesCaracteristicas.Categoria);
            return View(propiedadesCaracteristicas);
        }

        // POST: PropiedadesCaracteristicas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCaracteristica,Categoria,Caracteristica,Clave")] PropiedadesCaracteristicas propiedadesCaracteristicas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propiedadesCaracteristicas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categoria = new SelectList(db.PropiedadesCaracteristicasCategorias, "IdCategoria", "Categoria", propiedadesCaracteristicas.Categoria);
            return View(propiedadesCaracteristicas);
        }

        // GET: PropiedadesCaracteristicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropiedadesCaracteristicas propiedadesCaracteristicas = db.PropiedadesCaracteristicas.Find(id);
            if (propiedadesCaracteristicas == null)
            {
                return HttpNotFound();
            }
            return View(propiedadesCaracteristicas);
        }

        // POST: PropiedadesCaracteristicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PropiedadesCaracteristicas propiedadesCaracteristicas = db.PropiedadesCaracteristicas.Find(id);
            db.PropiedadesCaracteristicas.Remove(propiedadesCaracteristicas);
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
