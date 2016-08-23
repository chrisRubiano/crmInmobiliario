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
    public class PersonasInteresesController : Controller
    {

        private CRMINMOBILIARIOEntities10 db = new CRMINMOBILIARIOEntities10();

        // GET: PersonasIntereses
        public ActionResult Index()
        {
            var intereses = from i in db.PersonasIntereses
                         select i;
            return View(intereses.OrderBy(i => i.Interes).ToList());
            
        }

        // GET: PersonasIntereses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonasIntereses personasIntereses = db.PersonasIntereses.Find(id);
            if (personasIntereses == null)
            {
                return HttpNotFound();
            }
            return View(personasIntereses);
        }

        // GET: PersonasIntereses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonasIntereses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdInteres,Interes")] PersonasIntereses personasIntereses)
        {
            if (ModelState.IsValid)
            {
                db.PersonasIntereses.Add(personasIntereses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personasIntereses);
        }

        public void Excel()
        {
            var model = db.MediosEnterarse.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "Intereses");
        }



        // GET: PersonasIntereses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonasIntereses personasIntereses = db.PersonasIntereses.Find(id);
            if (personasIntereses == null)
            {
                return HttpNotFound();
            }
            return View(personasIntereses);
        }

        // POST: PersonasIntereses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdInteres,Interes")] PersonasIntereses personasIntereses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personasIntereses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personasIntereses);
        }

        // GET: PersonasIntereses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonasIntereses personasIntereses = db.PersonasIntereses.Find(id);
            if (personasIntereses == null)
            {
                return HttpNotFound();
            }
            return View(personasIntereses);
        }

        // POST: PersonasIntereses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonasIntereses personasIntereses = db.PersonasIntereses.Find(id);
            db.PersonasIntereses.Remove(personasIntereses);
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
