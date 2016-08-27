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
    public class MediosContactoController : Controller
    {
        private CRMINMOBILIARIOEntities db = new CRMINMOBILIARIOEntities();

        // GET: MediosContacto
        public ActionResult Index()
        {
            var medios = from m in db.MediosContacto
                         select m;

            return View(medios.OrderBy(m => m.MedioContacto).ToList());
        }

        // GET: MediosContacto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediosContacto mediosContacto = db.MediosContacto.Find(id);
            if (mediosContacto == null)
            {
                return HttpNotFound();
            }
            return View(mediosContacto);
        }

        // GET: MediosContacto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MediosContacto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMedioContacto,MedioContacto")] MediosContacto mediosContacto)
        {
            if (ModelState.IsValid)
            {
                db.MediosContacto.Add(mediosContacto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mediosContacto);
        }
        public void Excel()
        {
            var model = db.MediosContacto.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "MediosContacto");
        }

        

        // GET: MediosContacto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediosContacto mediosContacto = db.MediosContacto.Find(id);
            if (mediosContacto == null)
            {
                return HttpNotFound();
            }
            return View(mediosContacto);
        }

        // POST: MediosContacto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMedioContacto,MedioContacto")] MediosContacto mediosContacto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mediosContacto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mediosContacto);
        }

        // GET: MediosContacto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediosContacto mediosContacto = db.MediosContacto.Find(id);
            if (mediosContacto == null)
            {
                return HttpNotFound();
            }
            return View(mediosContacto);
        }

        // POST: MediosContacto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MediosContacto mediosContacto = db.MediosContacto.Find(id);
            db.MediosContacto.Remove(mediosContacto);
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
