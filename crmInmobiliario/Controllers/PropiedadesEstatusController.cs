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
    public class PropiedadesEstatusController : Controller
    {
        private CRMINMOBILIARIOEntities7 db = new CRMINMOBILIARIOEntities7();

        // GET: PropiedadesEstatus
        public ActionResult Index()
        {
            return View(db.PropiedadesEstatus.ToList());
        }

        // GET: PropiedadesEstatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropiedadesEstatus propiedadesEstatus = db.PropiedadesEstatus.Find(id);
            if (propiedadesEstatus == null)
            {
                return HttpNotFound();
            }
            return View(propiedadesEstatus);
        }

        // GET: PropiedadesEstatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropiedadesEstatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEstatus,Estatus")] PropiedadesEstatus propiedadesEstatus)
        {
            if (ModelState.IsValid)
            {
                db.PropiedadesEstatus.Add(propiedadesEstatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(propiedadesEstatus);
        }

        // GET: PropiedadesEstatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropiedadesEstatus propiedadesEstatus = db.PropiedadesEstatus.Find(id);
            if (propiedadesEstatus == null)
            {
                return HttpNotFound();
            }
            return View(propiedadesEstatus);
        }

        // POST: PropiedadesEstatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEstatus,Estatus")] PropiedadesEstatus propiedadesEstatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propiedadesEstatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propiedadesEstatus);
        }

        // GET: PropiedadesEstatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropiedadesEstatus propiedadesEstatus = db.PropiedadesEstatus.Find(id);
            if (propiedadesEstatus == null)
            {
                return HttpNotFound();
            }
            return View(propiedadesEstatus);
        }

        // POST: PropiedadesEstatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PropiedadesEstatus propiedadesEstatus = db.PropiedadesEstatus.Find(id);
            db.PropiedadesEstatus.Remove(propiedadesEstatus);
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
