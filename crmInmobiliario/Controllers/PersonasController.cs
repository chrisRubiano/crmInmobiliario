using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using crmInmobiliario.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace crmInmobiliario.Controllers
{
    [Authorize]
    public class PersonasController : Controller
    {
        private CRMINMOBILIARIOEntities4 db = new CRMINMOBILIARIOEntities4();


        // GET: Personas
        public ActionResult Index(string categoria, string nombre)
        {
            var nombreCompleto = new List<string>();
            var nombreQry = from d in db.Personas
                            orderby d.Paterno
                            select d.Nombre + " " + d.Paterno + " " + d.Materno;
            nombreCompleto.AddRange(nombreQry);
            ViewBag.nombre = new SelectList(nombreCompleto);


            var categorias = new List<string>();
            var categoriaQry = from d in db.PersonasCategoria
                               orderby d.Categoria
                               select d.Categoria.ToString();

            categorias.AddRange(categoriaQry.Distinct());
            ViewBag.categoria = new SelectList(categorias);

            var personas = from p in db.Personas
                           select p;

            if (!string.IsNullOrEmpty(categoria))
            {
                personas = personas.Where(s => s.PersonasCategoria.Categoria.ToString().Contains(categoria));
            }

            if (!string.IsNullOrEmpty(nombre))
            {
                personas = from p in db.Personas select p;
                personas = personas.Where(s => s.Nombre + " " + s.Paterno + " " + s.Materno == nombre);
            }


            //var personas = db.Personas.Include(p => p.Estados).Include(p => p.MediosContacto).Include(p => p.Municipios).Include(p => p.Paises).Include(p => p.PersonasGenero).Include(p => p.PersonasTipo);
            return View(personas.ToList());
        }


        public ActionResult ListaProspectos(string nombre)
        {
            var nombreCompleto = new List<string>();
            var nombreQry = from d in db.Personas.Where(p => p.Categoria == 1)
                            orderby d.Paterno
                            select d.Nombre + " " + d.Paterno + " " + d.Materno;
            nombreCompleto.AddRange(nombreQry);
            ViewBag.nombre = new SelectList(nombreCompleto);

            var personas = from p in db.Personas.Where(p => p.Categoria == 1)
                           select p;

            if (!string.IsNullOrEmpty(nombre))
            {
                personas = from p in db.Personas select p;
                personas = personas.Where(s => s.Nombre + " " + s.Paterno + " " + s.Materno == nombre && s.Categoria == 1);
            }

            return View(personas.ToList());
        }

        public ActionResult ListaClientes(string nombre)
        {
            var nombreCompleto = new List<string>();
            var nombreQry = from d in db.Personas.Where(p => p.Categoria == 2)
                            orderby d.Paterno
                            select d.Nombre + " " + d.Paterno + " " + d.Materno;
            nombreCompleto.AddRange(nombreQry);
            ViewBag.nombre = new SelectList(nombreCompleto);

            var personas = from p in db.Personas.Where(p => p.Categoria == 2)
                           select p;

            if (!string.IsNullOrEmpty(nombre))
            {
                personas = from p in db.Personas select p;
                personas = personas.Where(s => s.Nombre + " " + s.Paterno + " " + s.Materno == nombre && s.Categoria == 2);
            }

            return View(personas.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id, int? categoriap)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VariosModelos vModelos = new VariosModelos();
            Personas personas = db.Personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            vModelos.personas = personas;

            var notas = db.Notas.Where(d => d.Persona == id).OrderByDescending(d => d.Fecha);
            vModelos.notas = notas;

            var domicilios = db.Domicilios.Where(d => d.IdPersona == id).OrderByDescending(d => d.IdDomicilio);
            vModelos.domicilios = domicilios;

            ViewBag.Categoriap = categoriap;
            return View(vModelos);
        }

        // GET: Personas/Create
        public ActionResult Create(int? categoria)
        {
            ViewBag.MedioContacto = new SelectList(db.MediosContacto, "IdMedioContacto", "MedioContacto");
            ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero");
            ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo");
            ViewBag.Interes = new SelectList(db.PersonasIntereses, "IdInteres", "Interes");
            ViewBag.CategoriaInteres = new SelectList(db.PropiedadesTipo, "IdTipoPropiedad", "TipoPropiedad");
            ViewBag.Categoriap = categoria;
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPersona,Tipo,Categoria,Nombre,Paterno,Materno,Genero,FechaNacimiento,Email,Email2,Telefono,Celular,MedioContacto,Interes,CategoriaInteres,Usuario,FechaRegistro")] Personas personas, int categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    personas.Usuario = User.Identity.GetUserId().ToString();
                    personas.FechaRegistro = DateTime.Now;
                    personas.Categoria = categoria;
                    db.Personas.Add(personas);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No es posible guardar los cambios, intente mas tarde. Si los cambios persisten favor de contactarse con un adminsitrador");
            }

            ViewBag.MedioContacto = new SelectList(db.MediosContacto, "IdMedioContacto", "MedioContacto", personas.MedioContacto);
            ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero", personas.Genero);
            ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo", personas.Tipo);
            ViewBag.Interes = new SelectList(db.PersonasIntereses, "IdInteres", "Interes");
            ViewBag.CategoriaInteres = new SelectList(db.PropiedadesTipo, "IdTipoPropiedad", "TipoPropiedad");
            return View(personas);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id, int categoriap)
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

            ViewBag.Categoria = new SelectList(db.PersonasCategoria, "IdCategoria", "Categoria", personas.Categoria);
            ViewBag.MedioContacto = new SelectList(db.MediosContacto, "IdMedioContacto", "MedioContacto", personas.MedioContacto);
            ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero", personas.Genero);
            ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo", personas.Tipo);
            ViewBag.categoriap = categoriap;
            return View(personas);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPersona,Tipo,Categoria,Nombre,Paterno,Materno,Genero,FechaNacimiento,Email,Email2,Telefono,Celular,Calle,NumExterior,NumInterior,EntreEsquina,YCalle,Colonia,CP,Localidad,Municipio,Estado,Pais,MedioContacto,UsuarioUA,FechaUA,Usuario,FechaRegistro")] Personas personas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    personas.UsuarioUA = User.Identity.GetUserId().ToString();
                    personas.FechaUA = DateTime.Now;
                    db.Entry(personas).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No es posible guardar los cambios, intente mas tarde. Si los cambios persisten favor de contactarse con un adminsitrador");
            }

            ViewBag.MedioContacto = new SelectList(db.MediosContacto, "IdMedioContacto", "MedioContacto", personas.MedioContacto);
            ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero", personas.Genero);
            ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo", personas.Tipo);
            return View(personas);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id, int categoriap)
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
            ViewBag.categoriap = categoriap;
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
