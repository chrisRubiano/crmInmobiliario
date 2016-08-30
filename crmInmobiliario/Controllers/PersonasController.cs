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
using System.IO;
using System.Web.UI;

namespace crmInmobiliario.Controllers
{
    [Authorize]
    public class PersonasController : Controller
    {
        private CRMINMOBILIARIOEntities db = new CRMINMOBILIARIOEntities();


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
            return View(personas.OrderByDescending(p => p.IdPersona).ToList());
        }

        public void Excel(string nombreArchivo)
        {
            var model = db.Personas.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, nombreArchivo);
        }

        //helper class
        public class Export
        {
            public void ToExcel(HttpResponseBase Response, object clientsList, string nombreArchivo)
            {
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = clientsList;
                grid.DataBind();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=" + nombreArchivo + ".xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
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

            return View(personas.OrderByDescending(p => p.IdPersona).ToList());
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

            return View(personas.OrderByDescending(p => p.IdPersona).ToList());
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


        public ActionResult Duplicado(int? id)
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

            return View(vModelos);
        }

        // GET: Personas/Create
        public ActionResult Create(int? categoria)
        {
            ViewBag.MedioContacto = new SelectList(db.MediosContacto, "IdMedioContacto", "MedioContacto");
            ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero");
            ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo");
            ViewBag.Interes = new SelectList(db.PersonasIntereses, "IdInteres", "Interes");
            ViewBag.CategoriaInteres = new SelectList(db.PropiedadesCategoria, "IdTipoPropiedad", "CategoriaPropiedad");
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
                    var duplicado = new Personas();
                    if (personas.Email != null)
                    {
                        duplicado = db.Personas.Where(p => p.Email == personas.Email || p.Email2 == personas.Email).FirstOrDefault();
                    }
                    if (personas.Email2 != null && duplicado.IdPersona == 0)
                    {
                        duplicado = db.Personas.Where(p => p.Email == personas.Email2 || p.Email2 == personas.Email2).FirstOrDefault();
                    }


                    if (duplicado != null)
                    {
                        return RedirectToAction("Duplicado", new { id = duplicado.IdPersona });
                    }
                    else
                    {
                        personas.Usuario = User.Identity.GetUserId().ToString();
                        personas.FechaRegistro = DateTime.Now;
                        personas.Categoria = categoria;
                        db.Personas.Add(personas);
                        db.SaveChanges();
                        if (personas.Categoria == 1)
                        {
                            return RedirectToAction("ListaProspectos");
                        }
                        else if (personas.Categoria == 2)
                        {
                            return RedirectToAction("ListaClientes");
                        }
                    }

                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No es posible guardar los cambios, intente mas tarde. Si los problemas persisten favor de contactarse con un adminsitrador");
            }

            ViewBag.MedioContacto = new SelectList(db.MediosContacto, "IdMedioContacto", "MedioContacto", personas.MedioContacto);
            ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero", personas.Genero);
            ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo", personas.Tipo);
            ViewBag.Interes = new SelectList(db.PersonasIntereses, "IdInteres", "Interes");
            ViewBag.CategoriaInteres = new SelectList(db.PropiedadesCategoria, "IdTipoPropiedad", "CategoriaPropiedad");
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
        public ActionResult Edit([Bind(Include = "IdPersona,Tipo,Categoria,Nombre,Paterno,Materno,Genero,FechaNacimiento,Email,Email2,Telefono,Celular,Calle,NumExterior,NumInterior,EntreEsquina,YCalle,Colonia,CP,Localidad,Municipio,Estado,Pais,MedioContacto,UsuarioUA,FechaUA,Usuario,FechaRegistro")] Personas personas, int? categoriap)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    personas.UsuarioUA = User.Identity.GetUserId().ToString();
                    personas.FechaUA = DateTime.Now;
                    db.Entry(personas).State = EntityState.Modified;
                    db.SaveChanges();
                    if (categoriap.Value == 1)
                    {
                        return RedirectToAction("ListaProspectos");
                    }
                    else if (categoriap.Value == 2)
                    {
                        return RedirectToAction("ListaClientes");
                    }

                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No es posible guardar los cambios, intente mas tarde. Si los problemas persisten favor de contactarse con un adminsitrador");
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
        public ActionResult DeleteConfirmed(int id, int Categoriap)
        {
            Personas personas = db.Personas.Find(id);
            try
            {
                eliminarNotas(id);
                eliminarDomicilios(id, 1);
            }
            catch (Exception)
            {

            }
            db.Personas.Remove(personas);
            db.SaveChanges();
            if (Categoriap == 1)
            {
                return RedirectToAction("ListaProspectos");
            }
            else if (Categoriap == 2)
            {
                return RedirectToAction("ListaClientes");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /*---------------------------------*/

        public void eliminarNotas(int id)
        {
            IEnumerable<Notas> listaNotas = db.Notas.Where(i => i.Persona == id);
            foreach (var item in listaNotas)
            {
                db.Notas.Remove(item);
            }
            db.SaveChanges();
        }

        /*Tipo 1 = personas, Tipo 2 = Propiedades*/
        public void eliminarDomicilios(int id, int tipo)
        {
            if (tipo == 1)
            {
                IEnumerable<Domicilios> listaDomicilios = db.Domicilios.Where(i => i.IdPersona == id);
                foreach (var item in listaDomicilios)
                {
                    db.Domicilios.Remove(item);
                }
            }
            else if (tipo == 2)
            {
                IEnumerable<Domicilios> listaDomicilios = db.Domicilios.Where(i => i.IdPropiedad == id);
                foreach (var item in listaDomicilios)
                {
                    db.Domicilios.Remove(item);
                }
            }
            db.SaveChanges();

        }


        /*-------------------------------*/
    }
}
