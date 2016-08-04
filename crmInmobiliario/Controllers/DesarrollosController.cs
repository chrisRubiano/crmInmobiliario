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
    public class DesarrollosController : Controller
    {
        private CRMINMOBILIARIOEntities4 db = new CRMINMOBILIARIOEntities4();

        // GET: Desarrollos
        public ActionResult Index()
        {
            return View(db.Desarrollos.ToList());
        }

        // GET: Desarrollos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desarrollos desarrollos = db.Desarrollos.Find(id);
            if (desarrollos == null)
            {
                return HttpNotFound();
            }
            return View(desarrollos);
        }

        // GET: Desarrollos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Desarrollos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDesarrollo,Desarrollo,Clave,Activo")] Desarrollos desarrollos)
        {
            if (ModelState.IsValid)
            {
                db.Desarrollos.Add(desarrollos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(desarrollos);
        }



        public ActionResult ListaDesarrollos(string desarrollo)
        {
            var desarrolloList = new List<string>();
            var desarrolloQry = from d in db.Desarrollos
                                orderby d.Desarrollo
                                select d.Desarrollo;

            desarrolloList.AddRange(desarrolloQry);

            ViewBag.desarrollo = new SelectList(desarrolloList);

            var desarrollos = from d in db.Desarrollos
                              select d;

            if (!string.IsNullOrEmpty(desarrollo))
            {
                desarrollos = from d in db.Desarrollos
                              select d;
                desarrollos = desarrollos.Where(s => s.Desarrollo == desarrollo);
            }

            return View(desarrollos.ToList());
        }


        // GET: Desarrollos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desarrollos desarrollos = db.Desarrollos.Find(id);
            if (desarrollos == null)
            {
                return HttpNotFound();
            }
            return View(desarrollos);
        }

        // POST: Desarrollos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDesarrollo,Desarrollo,Clave,Activo")] Desarrollos desarrollos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(desarrollos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(desarrollos);
        }

        // GET: Desarrollos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desarrollos desarrollos = db.Desarrollos.Find(id);
            if (desarrollos == null)
            {
                return HttpNotFound();
            }
            return View(desarrollos);
        }

        // POST: Desarrollos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Desarrollos desarrollos = db.Desarrollos.Find(id);
            db.Desarrollos.Remove(desarrollos);
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