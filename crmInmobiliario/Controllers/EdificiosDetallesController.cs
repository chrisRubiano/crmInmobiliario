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
    [Authorize]
    public class EdificiosDetallesController : Controller
    {
        private CRMINMOBILIARIOEntities2 db = new CRMINMOBILIARIOEntities2();

        // GET: EdificiosDetalles
        public ActionResult Index()
        {
            var edificiosDetalle = db.EdificiosDetalle.Include(e => e.Edificios).OrderByDescending(e => e.IdEdificioDetalle);
            return View(edificiosDetalle.ToList());
        }

        // GET: EdificiosDetalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EdificiosDetalle edificiosDetalle = db.EdificiosDetalle.Find(id);
            if (edificiosDetalle == null)
            {
                return HttpNotFound();
            }
            return View(edificiosDetalle);
        }

        // GET: EdificiosDetalles/Create
        public ActionResult Create(int? idEdificio)
        {

            var edificiosList = new List<string>();
            var detalleEdificioQry = from d in db.Edificios where d.IdEdificio == idEdificio select d.Edificio;
            edificiosList.AddRange(detalleEdificioQry);
            ViewBag.Edificio = edificiosList[0];
            EdificiosDetalle detalles = new EdificiosDetalle();
            ViewBag.idEdificio = idEdificio;
            return View(detalles);

            //ViewBag.Edificio = new SelectList(db.Edificios, "IdEdificio", "Edificio");
            //return View();
        }

        // POST: EdificiosDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEdificioDetalle,Edificio,Nivel,M2")] EdificiosDetalle edificiosDetalle, int idEdificio)
        {
            //if (ModelState.IsValid)
            //{
            //    db.EdificiosDetalle.Add(edificiosDetalle);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.Edificio = new SelectList(db.Edificios, "IdEdificio", "Edificio", edificiosDetalle.Edificio);
            //return View(edificiosDetalle);

            if (ModelState.IsValid)
            {
                edificiosDetalle.Edificio = idEdificio;
                db.EdificiosDetalle.Add(edificiosDetalle);
                db.SaveChanges();
                
                return Redirect("/Edificios/Details/" + idEdificio.ToString());
            }

            return View(edificiosDetalle);

        }

        // GET: EdificiosDetalles/Edit/5
        public ActionResult Edit(int? id, int idEdificio)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //EdificiosDetalle edificiosDetalle = db.EdificiosDetalle.Find(id);
            //if (edificiosDetalle == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.Edificio = new SelectList(db.Edificios, "IdEdificio", "Edificio", edificiosDetalle.Edificio);
            //return View(edificiosDetalle);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EdificiosDetalle edificiosDetalle = db.EdificiosDetalle.Find(id);
            if (edificiosDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.Edificio = idEdificio;
            return View(edificiosDetalle);

        }

        // POST: EdificiosDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEdificioDetalle,Edificio,Nivel,M2")] EdificiosDetalle edificiosDetalle, int idEdificio)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(edificiosDetalle).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.Edificio = new SelectList(db.Edificios, "IdEdificio", "Edificio", edificiosDetalle.Edificio);
            //return View(edificiosDetalle);

            if (ModelState.IsValid)
            {
                edificiosDetalle.Edificio = idEdificio;
                db.Entry(edificiosDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Edificios/Details/" + idEdificio.ToString());
            }
            ViewBag.Edificio = idEdificio;
            return View(edificiosDetalle);
        }

        // GET: EdificiosDetalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EdificiosDetalle edificiosDetalle = db.EdificiosDetalle.Find(id);
            if (edificiosDetalle == null)
            {
                return HttpNotFound();
            }
            return View(edificiosDetalle);
        }

        // POST: EdificiosDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //EdificiosDetalle edificiosDetalle = db.EdificiosDetalle.Find(id);
            //db.EdificiosDetalle.Remove(edificiosDetalle);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            EdificiosDetalle edificiosDetalle = db.EdificiosDetalle.Find(id);
            var edificio = edificiosDetalle.Edificio;
            db.EdificiosDetalle.Remove(edificiosDetalle);
            db.SaveChanges();
            return Redirect("/Edificios/Details/" + edificio.ToString());

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
