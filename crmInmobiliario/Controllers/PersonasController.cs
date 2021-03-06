﻿using System;
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
using crmInmobiliario.Utilidades;

namespace crmInmobiliario.Controllers
{
    [Authorize]
    public class PersonasController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }

        // GET: Personas
        public ActionResult Index(string categoria, string nombre, string codigo)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
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

                if (!string.IsNullOrEmpty(codigo))
                {
                    personas = personas.Where(p => p.CodigoPersona == codigo);
                }


                //var usuario = getUser();
                if (usuario.UserRoles == "VENTAS") //para que los vendedores solo vean las cotizaciones registradas por ellos
                {
                    personas = personas.Where(p => p.Usuario == usuario.Id);
                }

                //var personas = db.Personas.Include(p => p.Estados).Include(p => p.MediosContacto).Include(p => p.Municipios).Include(p => p.Paises).Include(p => p.PersonasGenero).Include(p => p.PersonasTipo);
                ViewBag.codigo = new SelectList(db.Personas, "CodigoPersona", "CodigoPersona");

                return View(personas.OrderByDescending(p => p.IdPersona).ToList());
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        public void Excel(string nombreArchivo)
        {
            var model = db.Personas.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "Personas");
        }

        //helper class

        public ActionResult ListaProspectos(string nombre)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "VENTAS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
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

                //var usuario = getUser();
                if (usuario.UserRoles == "VENTAS") //para que los vendedores solo vean las personas registradas por ellos
                {
                    personas = personas.Where(p => p.Usuario == usuario.Id);
                }

                return View(personas.OrderByDescending(p => p.IdPersona).ToList());
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        public ActionResult ListaClientes(string nombre, string codigo)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "VENTAS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
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

                if (!string.IsNullOrEmpty(codigo))
                {
                    personas = personas.Where(p => p.CodigoPersona == codigo);
                }

                //var usuario = getUser();
                if (usuario.UserRoles == "VENTAS") //para que los vendedores solo vean las personas registradas por ellos
                {
                    personas = personas.Where(p => p.Usuario == usuario.Id);
                }
                //Categoría 1 es prospecto y 2 es cliente
                ViewBag.codigo = new SelectList(db.Personas.Where(p => p.Categoria == 2), "CodigoPersona", "CodigoPersona");
                return View(personas.OrderByDescending(p => p.IdPersona).ToList());
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }

        }


        public ActionResult ListaProspectosValidar(string nombre)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "LEGAL" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                var nombreCompleto = new List<string>();
                var nombreQry = from d in db.Personas.Where(p => p.Categoria == 1)
                                orderby d.Paterno
                                select d.Nombre + " " + d.Paterno + " " + d.Materno;
                nombreCompleto.AddRange(nombreQry);
                ViewBag.nombre = new SelectList(nombreCompleto);

                var personas = from p in db.Personas.Where(p => p.Categoria == 1).Where(p => p.Validado != true)
                               select p;

                if (!string.IsNullOrEmpty(nombre))
                {
                    personas = from p in db.Personas select p;
                    personas = personas.Where(s => s.Nombre + " " + s.Paterno + " " + s.Materno == nombre && s.Categoria == 1);
                }

                //var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
                if (usuario.UserRoles == "VENTAS") //para que los vendedores solo vean las personas registradas por ellos
                {
                    personas = personas.Where(p => p.Usuario == usuario.Id);
                }

                return View(personas.OrderByDescending(p => p.IdPersona).ToList());
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }

        }


        public ActionResult Asignar(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL")
            {
                Personas persona = db.Personas.Find(id);
                ViewBag.Usuario = new SelectList(db.AspNetUsers.Where(u => u.UserRoles == "VENTAS"), "Id", "UserName", persona.Usuario);
                //ViewBag.CategoriaInteres = new SelectList(db.PropiedadesCategoria, "IdTipoPropiedad", "CategoriaPropiedad");

                return View(persona);
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Asignar(int? id, string Usuario)
        {
            try
            {
                Personas persona = new Personas();
                persona = db.Personas.Find(id);
                persona.Usuario = Usuario;
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

            }
            return RedirectToAction("Index", "Personas");
        }


        // GET: Personas/Details/5
        public ActionResult Details(int? id, int? categoriap)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "VENTAS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
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
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // GET: Personas/Details/5
        public ActionResult DetailsProspectoValidar(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "LEGAL" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                vmPersonaValidacion validacionmodelos = new vmPersonaValidacion();
                Personas personas = db.Personas.Find(id);
                if (personas == null)
                {
                    return HttpNotFound();
                }
                validacionmodelos.personas = personas;

                var validaciones = db.PersonasValidacion.Where(d => d.Persona == id);
                validacionmodelos.validaciones = validaciones;

                return View(validacionmodelos);
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }


        public ActionResult ValidacionRealizada()
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "LEGAL" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                return View();
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        public ActionResult Duplicado(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "VENTAS" || usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
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
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // GET: Personas/Create
        public ActionResult Create(int? categoria)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "VENTAS" || usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL")
            {
                ViewBag.MedioContacto = new SelectList(db.MediosContacto, "IdMedioContacto", "MedioContacto");
                ViewBag.Referencia = new SelectList(db.Personas, "IdPersona", "NombreCompleto");
                ViewBag.MediosEnterarse = new SelectList(db.MediosEnterarse, "IdMedio", "Medio");
                ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero");
                ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo");
                ViewBag.Interes = new SelectList(db.PersonasIntereses, "IdInteres", "Interes");
                ViewBag.EstadoCivil = new SelectList(db.EstadoCivil, "IdEstadoCivil", "EstadoCivil1");
                ViewBag.CategoriaInteres = new SelectList(db.PropiedadesCategoria, "IdTipoPropiedad", "CategoriaPropiedad");
                ViewBag.Categoriap = categoria;
                return View();
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPersona,Tipo,Categoria,Nombre,Paterno,Materno,Genero,FechaNacimiento,Email,Email2,Telefono,Celular,MedioContacto,MediosEnterarse,Interes,CategoriaInteres,Usuario,FechaRegistro,InteresEspecifique,Giro,RFC,Referencia,CURP,LugarNacimiento,Ocupacion,EstadoCivil")] Personas personas, int categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var duplicado = new Personas();
                    if (personas.Email != null)
                    {
                        duplicado = db.Personas.AsNoTracking().Where(p => p.Email == personas.Email || p.Email2 == personas.Email).FirstOrDefault();
                    }
                    if (personas.Email2 != null && duplicado == null)
                    {
                        duplicado = db.Personas.AsNoTracking().Where(p => p.Email == personas.Email2 || p.Email2 == personas.Email2).FirstOrDefault();
                    }


                    if (duplicado != null)
                    {
                        ProspectosIncidencias incidencia = new ProspectosIncidencias();
                        incidencia.Prospecto = duplicado.IdPersona;
                        incidencia.UsuarioRegistro = duplicado.Usuario;
                        incidencia.UsuarioIncidencia = User.Identity.GetUserId().ToString();
                        incidencia.FechaRegistro = DateTime.Now;
                        db.ProspectosIncidencias.Add(incidencia);
                        db.SaveChanges();

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
            ViewBag.MediosEnterarse = new SelectList(db.MediosEnterarse, "IdMedio", "Medio");
            ViewBag.Referencia = new SelectList(db.Personas, "IdPersona", "NombreCompleto");
            ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero", personas.Genero);
            ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo", personas.Tipo);
            ViewBag.Interes = new SelectList(db.PersonasIntereses, "IdInteres", "Interes");
            ViewBag.EstadoCivil = new SelectList(db.EstadoCivil, "IdEstadoCivil", "EstadoCivil1");
            ViewBag.CategoriaInteres = new SelectList(db.PropiedadesCategoria, "IdTipoPropiedad", "CategoriaPropiedad");
            ViewBag.Categoriap = categoria;
            return View(personas);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id, int? categoriap)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "VENTAS" || usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL")
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
                ViewBag.MediosEnterarse = new SelectList(db.MediosEnterarse, "IdMedio", "Medio", personas.MediosEnterarse);
                ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero", personas.Genero);
                ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo", personas.Tipo);
                ViewBag.Interes = new SelectList(db.PersonasIntereses, "IdInteres", "Interes", personas.Interes);
                ViewBag.EstadoCivil = new SelectList(db.EstadoCivil, "IdEstadoCivil", "EstadoCivil1");
                ViewBag.CategoriaInteres = new SelectList(db.PropiedadesCategoria, "IdTipoPropiedad", "CategoriaPropiedad");
                ViewBag.categoriap = categoriap;
                return View(personas);
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPersona,Tipo,Categoria,Nombre,Paterno,Materno,Genero,FechaNacimiento,Email,Email2,Telefono,Celular,MedioContacto,MediosEnterarse,Interes,CategoriaInteres,Usuario,FechaRegistro,InteresEspecifique,Giro,RFC,CURP,LugarNacimiento,Ocupacion,EstadoCivil")] Personas personas, int? categoriap)
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

            ViewBag.Categoria = new SelectList(db.PersonasCategoria, "IdCategoria", "Categoria", personas.Categoria);
            ViewBag.MedioContacto = new SelectList(db.MediosContacto, "IdMedioContacto", "MedioContacto", personas.MedioContacto);
            ViewBag.MediosEnterarse = new SelectList(db.MediosEnterarse, "IdMedio", "Medio");
            ViewBag.Genero = new SelectList(db.PersonasGenero, "IdGenero", "Genero", personas.Genero);
            ViewBag.Tipo = new SelectList(db.PersonasTipo, "IdTipoPersona", "Tipo", personas.Tipo);
            ViewBag.Interes = new SelectList(db.PersonasIntereses, "IdInteres", "Interes");
            ViewBag.EstadoCivil = new SelectList(db.EstadoCivil, "IdEstadoCivil", "EstadoCivil1");
            ViewBag.CategoriaInteres = new SelectList(db.PropiedadesCategoria, "IdTipoPropiedad", "CategoriaPropiedad");
            ViewBag.categoriap = categoriap;
            return View(personas);
        }

        // GET: Personas/Edit/5
        public ActionResult ValidarProspecto(int? id, int? categoriap)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "LEGAL")
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
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }


        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidarProspecto([Bind(Include = "IdPersona,Tipo,Categoria,Nombre,Paterno,Materno,Genero,FechaNacimiento,Email,Email2,Telefono,Celular,MedioContacto,UsuarioUA,FechaUA,Usuario,FechaRegistro,InteresEspecifique,Giro,RFC,CURP,LugarNacimiento,Ocupacion,EstadoCivil")] Personas personas, int? categoriap)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    personas.UsuarioUA = User.Identity.GetUserId().ToString();
                    personas.FechaUA = DateTime.Now;
                    personas.Validado = true;
                    personas.Categoria = 2;
                    db.Entry(personas).State = EntityState.Modified;
                    db.SaveChanges();

                    /*
                     Generar una TAM oficial por cada TAM del prospecto al momento de ser validado
                     */
                    var amortizaciones = db.Amortizaciones.Include(a => a.TiposPago).Where(a => a.Persona == personas.IdPersona);
                    if (amortizaciones!=null)
                    {
                        foreach (var amortizacion in amortizaciones)
                        {
                            Amortizaciones oficial = new Amortizaciones();
                            oficial = amortizacion;
                            oficial.Tipo = "O";
                            db.Amortizaciones.Add(oficial);
                        }
                        db.SaveChanges();
                    }
                    /*-------------------------------------*/



                    return RedirectToAction("ValidacionRealizada");

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
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "VENTAS" || usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL")
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
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
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
                eliminarIncidencias(id);
                eliminarDomicilios(id, 1);

                db.Personas.Remove(personas);
                db.SaveChanges();
            }
            catch (Exception)
            {

            }
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
                return RedirectToAction("PermisoDenegado", "Account");
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

        public void eliminarIncidencias(int id)
        {
            IEnumerable<ProspectosIncidencias> listaIncidencias = db.ProspectosIncidencias.Where(i => i.Prospecto == id);
            foreach (var item in listaIncidencias)
            {
                db.ProspectosIncidencias.Remove(item);
            }
            db.SaveChanges();
        }


        /*-------------------------------*/
    }
}
