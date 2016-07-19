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
    public class PersonasController : Controller
    {
        private CRMINMOBILIARIOEntities db = new CRMINMOBILIARIOEntities();

        // GET: Personas
        public ActionResult Index()
        {
            var personas = db.Personas.Include(p => p.Estados).Include(p => p.MediosContacto).Include(p => p.Municipios).Include(p => p.Paises).Include(p => p.PersonasGenero).Include(p => p.PersonasTipo);
            return View(personas.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = db.Personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            ViewBag.Estado = new SelectList(db.Estados, "IdEstado", "Estado");
            ViewBag.MedioContacto = new SelectList(db.MediosContacto, "IdMedioContacto", "MedioContacto");
            ViewBag.Municipio = new SelectList(db.Municipios, "IdMunicipio", "Municipio");
            ViewBag.Pais = new SelectList(db.Paises, "IdPais", "Pais");
            ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero");
            ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo");
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPersona,Tipo,Nombre,Paterno,Materno,Genero,FechaNacimiento,Email,Email2,Telefono,Celular,Calle,NumExterior,NumInterior,EntreEsquina,YCalle,Colonia,CP,Localidad,Municipio,Estado,Pais,MedioContacto")] Personas personas)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(personas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Estado = new SelectList(db.Estados, "IdEstado", "Estado", personas.Estado);
            ViewBag.MedioContacto = new SelectList(db.MediosContacto, "IdMedioContacto", "MedioContacto", personas.MedioContacto);
            ViewBag.Municipio = new SelectList(db.Municipios, "IdMunicipio", "Municipio", personas.Municipio);
            ViewBag.Pais = new SelectList(db.Paises, "IdPais", "Pais", personas.Pais);
            ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero", personas.Genero);
            ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo", personas.Tipo);
            return View(personas);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = db.Personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estado = new SelectList(db.Estados, "IdEstado", "Estado", personas.Estado);
            ViewBag.MedioContacto = new SelectList(db.MediosContacto, "IdMedioContacto", "MedioContacto", personas.MedioContacto);
            ViewBag.Municipio = new SelectList(db.Municipios, "IdMunicipio", "Municipio", personas.Municipio);
            ViewBag.Pais = new SelectList(db.Paises, "IdPais", "Pais", personas.Pais);
            ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero", personas.Genero);
            ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo", personas.Tipo);
            return View(personas);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPersona,Tipo,Nombre,Paterno,Materno,Genero,FechaNacimiento,Email,Email2,Telefono,Celular,Calle,NumExterior,NumInterior,EntreEsquina,YCalle,Colonia,CP,Localidad,Municipio,Estado,Pais,MedioContacto")] Personas personas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Estado = new SelectList(db.Estados, "IdEstado", "Estado", personas.Estado);
            ViewBag.MedioContacto = new SelectList(db.MediosContacto, "IdMedioContacto", "MedioContacto", personas.MedioContacto);
            ViewBag.Municipio = new SelectList(db.Municipios, "IdMunicipio", "Municipio", personas.Municipio);
            ViewBag.Pais = new SelectList(db.Paises, "IdPais", "Pais", personas.Pais);
            ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero", personas.Genero);
            ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo", personas.Tipo);
            return View(personas);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = db.Personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personas personas = db.Personas.Find(id);
            db.Personas.Remove(personas);
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
