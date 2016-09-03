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
    public class PersonasValidacionsController : Controller
    {
        private CRMINMOBILIARIOEntities2 db = new CRMINMOBILIARIOEntities2();

        // GET: PersonasValidacions
        public ActionResult Index()
        {
            var personasValidacion = db.PersonasValidacion.Include(p => p.DocumentosCategoria).Include(p => p.Personas);
            return View(personasValidacion.ToList());
        }


        // GET: PersonasValidacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonasValidacion personasValidacion = db.PersonasValidacion.Find(id);
            if (personasValidacion == null)
            {
                return HttpNotFound();
            }
            return View(personasValidacion);
        }

        // GET: PersonasValidacions/Create
        public ActionResult Create()
        {
            var personas = (from p in db.Personas
                           select new SelectListItem
                           {
                               Text = p.Paterno + " " + p.Materno + " " + p.Nombre,
                               Value = p.IdPersona.ToString()
                           }).ToList();
            ViewBag.Persona = new SelectList(personas, "Value", "Text");

            ViewBag.CategoriaDocumento = new SelectList(db.DocumentosCategoria, "IdCategoria", "Categoria");
            return View();
        }

        // POST: PersonasValidacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdValidacion,Persona,CategoriaDocumento,Validacion")] PersonasValidacion personasValidacion)
        {
            if (ModelState.IsValid)
            {
                db.PersonasValidacion.Add(personasValidacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaDocumento = new SelectList(db.DocumentosCategoria, "IdCategoria", "Categoria", personasValidacion.CategoriaDocumento);
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre", personasValidacion.Persona);
            return View(personasValidacion);
        }

        // GET: PersonasValidacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonasValidacion personasValidacion = db.PersonasValidacion.Find(id);
            if (personasValidacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaDocumento = new SelectList(db.DocumentosCategoria, "IdCategoria", "Categoria", personasValidacion.CategoriaDocumento);
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre", personasValidacion.Persona);
            return View(personasValidacion);
        }

        // POST: PersonasValidacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdValidacion,Persona,CategoriaDocumento,Validacion")] PersonasValidacion personasValidacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personasValidacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaDocumento = new SelectList(db.DocumentosCategoria, "IdCategoria", "Categoria", personasValidacion.CategoriaDocumento);
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre", personasValidacion.Persona);
            return View(personasValidacion);
        }

        // GET: PersonasValidacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonasValidacion personasValidacion = db.PersonasValidacion.Find(id);
            if (personasValidacion == null)
            {
                return HttpNotFound();
            }
            return View(personasValidacion);
        }

        // POST: PersonasValidacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonasValidacion personasValidacion = db.PersonasValidacion.Find(id);
            db.PersonasValidacion.Remove(personasValidacion);
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
