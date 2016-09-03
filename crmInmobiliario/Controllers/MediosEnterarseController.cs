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
    public class MediosEnterarseController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        // GET: MediosEnterarse
        public ActionResult Index()
        {
            var medios = from m in db.MediosEnterarse
                         select m;
            return View(medios.OrderBy(m => m.Medio).ToList());
        }

        // GET: MediosEnterarse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediosEnterarse mediosEnterarse = db.MediosEnterarse.Find(id);
            if (mediosEnterarse == null)
            {
                return HttpNotFound();
            }
            return View(mediosEnterarse);
        }

        // GET: MediosEnterarse/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MediosEnterarse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMedio,Medio")] MediosEnterarse mediosEnterarse)
        {
            if (ModelState.IsValid)
            {
                db.MediosEnterarse.Add(mediosEnterarse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mediosEnterarse);
        }

        public void Excel()
        {
            var model = db.MediosEnterarse.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "MediosEnterarse");
        }


        // GET: MediosEnterarse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediosEnterarse mediosEnterarse = db.MediosEnterarse.Find(id);
            if (mediosEnterarse == null)
            {
                return HttpNotFound();
            }
            return View(mediosEnterarse);
        }

        // POST: MediosEnterarse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMedio,Medio")] MediosEnterarse mediosEnterarse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mediosEnterarse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mediosEnterarse);
        }

        // GET: MediosEnterarse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediosEnterarse mediosEnterarse = db.MediosEnterarse.Find(id);
            if (mediosEnterarse == null)
            {
                return HttpNotFound();
            }
            return View(mediosEnterarse);
        }

        // POST: MediosEnterarse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MediosEnterarse mediosEnterarse = db.MediosEnterarse.Find(id);
            db.MediosEnterarse.Remove(mediosEnterarse);
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
