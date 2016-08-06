﻿using System;
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
    public class PropiedadesAcabadosController : Controller
    {
        private CRMINMOBILIARIOEntities5 db = new CRMINMOBILIARIOEntities5();

        // GET: PropiedadesAcabados
        public ActionResult Index()
        {
            return View(db.PropiedadesAcabados.ToList());
        }

        // GET: PropiedadesAcabados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropiedadesAcabados propiedadesAcabados = db.PropiedadesAcabados.Find(id);
            if (propiedadesAcabados == null)
            {
                return HttpNotFound();
            }
            return View(propiedadesAcabados);
        }

        // GET: PropiedadesAcabados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropiedadesAcabados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAcabado,Acabado")] PropiedadesAcabados propiedadesAcabados)
        {
            if (ModelState.IsValid)
            {
                db.PropiedadesAcabados.Add(propiedadesAcabados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(propiedadesAcabados);
        }

        // GET: PropiedadesAcabados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropiedadesAcabados propiedadesAcabados = db.PropiedadesAcabados.Find(id);
            if (propiedadesAcabados == null)
            {
                return HttpNotFound();
            }
            return View(propiedadesAcabados);
        }

        // POST: PropiedadesAcabados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAcabado,Acabado")] PropiedadesAcabados propiedadesAcabados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propiedadesAcabados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propiedadesAcabados);
        }

        // GET: PropiedadesAcabados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropiedadesAcabados propiedadesAcabados = db.PropiedadesAcabados.Find(id);
            if (propiedadesAcabados == null)
            {
                return HttpNotFound();
            }
            return View(propiedadesAcabados);
        }

        // POST: PropiedadesAcabados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PropiedadesAcabados propiedadesAcabados = db.PropiedadesAcabados.Find(id);
            db.PropiedadesAcabados.Remove(propiedadesAcabados);
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
