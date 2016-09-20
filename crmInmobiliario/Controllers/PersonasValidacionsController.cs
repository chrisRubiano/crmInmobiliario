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
    public class PersonasValidacionsController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }

        // GET: PersonasValidacions
        public ActionResult Index()
        {
            var usuario = getUser();
            if (usuario.UserRoles == "LEGAL" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                var personasValidacion = db.PersonasValidacion.Include(p => p.DocumentosCategoria).Include(p => p.Personas);
                return View(personasValidacion.ToList());
            }else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: PersonasValidacions/Details/5
        public ActionResult Details(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "LEGAL" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: PersonasValidacions/Create
        public ActionResult Create(int? IdPersona)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "LEGAL" || usuario.UserRoles == "DIR-GENERAL")
            {
                var personas = (from p in db.Personas
                                select new SelectListItem
                                {
                                    Text = p.Paterno + " " + p.Materno + " " + p.Nombre,
                                    Value = p.IdPersona.ToString()
                                }).ToList();

                ViewBag.Persona = new SelectList(personas, "Value", "Text", IdPersona);
                ViewBag.IdPersona = IdPersona;

                ViewBag.CategoriaDocumento = new SelectList(db.DocumentosCategoria, "IdCategoria", "Categoria");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
                personasValidacion.Validacion = true;
                db.PersonasValidacion.Add(personasValidacion);
                db.SaveChanges();
                return RedirectToAction("DetailsProspectoValidar", "Personas", new { id = personasValidacion.Persona });
                    //RedirectToAction("Index");
            }

            ViewBag.CategoriaDocumento = new SelectList(db.DocumentosCategoria, "IdCategoria", "Categoria", personasValidacion.CategoriaDocumento);
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre", personasValidacion.Persona);
            return View(personasValidacion);
        }

        // GET: PersonasValidacions/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "LEGAL" || usuario.UserRoles == "DIR-GENERAL")
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
                ViewBag.IdPersona = personasValidacion.Persona;
                return View(personasValidacion);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
                personasValidacion.Validacion = true;
                db.Entry(personasValidacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DetailsProspectoValidar", "Personas", new { id = personasValidacion.Persona });
            }
            ViewBag.CategoriaDocumento = new SelectList(db.DocumentosCategoria, "IdCategoria", "Categoria", personasValidacion.CategoriaDocumento);
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre", personasValidacion.Persona);
            return View(personasValidacion);
        }

        // GET: PersonasValidacions/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "LEGAL" || usuario.UserRoles == "DIR-GENERAL")
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

                ViewBag.IdPersona = personasValidacion.Persona;

                return View(personasValidacion);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PersonasValidacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonasValidacion personasValidacion = db.PersonasValidacion.Find(id);
            int idPersona = personasValidacion.Persona.Value;
            db.PersonasValidacion.Remove(personasValidacion);
            db.SaveChanges();
            return RedirectToAction("DetailsProspectoValidar", "Personas", new { id = idPersona });
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
