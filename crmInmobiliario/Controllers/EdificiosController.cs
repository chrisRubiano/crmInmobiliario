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
using crmInmobiliario.Utilidades;

namespace crmInmobiliario.Controllers
{
    [Authorize]
    public class EdificiosController : Controller
    {
        private CRMINMOBILIARIOEntities2 db = new CRMINMOBILIARIOEntities2();

        // GET: Edificios
        public ActionResult Index()
        {
            var edificios = db.Edificios.Include(e => e.Desarrollos).OrderByDescending(e => e.IdEdificio);
            return View(edificios.ToList());
        }

        // GET: Edificios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edificios edificios = db.Edificios.Find(id);
            if (edificios == null)
            {
                return HttpNotFound();
            }
            return View(edificios);
        }

        public void Excel()
        {
            var model = db.Edificios.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "Edificios");
        }

       

        // GET: Edificios/Create
        public ActionResult Create()
        {
            ViewBag.Desarrollo = new SelectList(db.Desarrollos, "IdDesarrollo", "Desarrollo");
            return View();
        }

        // POST: Edificios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEdificio,Desarrollo,Edificio,Descuento,Niveles,CajonesEstacionamiento")] Edificios edificios)
        {
            if (ModelState.IsValid)
            {
                db.Edificios.Add(edificios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Desarrollo = new SelectList(db.Desarrollos, "IdDesarrollo", "Desarrollo", edificios.Desarrollo);
            return View(edificios);
        }

        // GET: Edificios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edificios edificios = db.Edificios.Find(id);
            if (edificios == null)
            {
                return HttpNotFound();
            }
            ViewBag.Desarrollo = new SelectList(db.Desarrollos, "IdDesarrollo", "Desarrollo", edificios.Desarrollo);
            return View(edificios);
        }

        // POST: Edificios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEdificio,Desarrollo,Edificio,Descuento,Niveles,CajonesEstacionamiento")] Edificios edificios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(edificios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Desarrollo = new SelectList(db.Desarrollos, "IdDesarrollo", "Desarrollo", edificios.Desarrollo);
            return View(edificios);
        }

        // GET: Edificios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edificios edificios = db.Edificios.Find(id);
            if (edificios == null)
            {
                return HttpNotFound();
            }
            return View(edificios);
        }

        // POST: Edificios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Edificios edificios = db.Edificios.Find(id);
            db.Edificios.Remove(edificios);
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
