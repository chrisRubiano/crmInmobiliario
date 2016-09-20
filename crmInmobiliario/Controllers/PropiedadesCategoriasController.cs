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
    public class PropiedadesCategoriasController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }


        // GET: PropiedadesCategorias
        public ActionResult Index()
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                var categorias = from c in db.PropiedadesCategoria
                                 select c;
                return View(categorias.OrderBy(c => c.Categoria).ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        // GET: PropiedadesCategorias/Details/5
        public ActionResult Details(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PropiedadesCategoria propiedadesCategoria = db.PropiedadesCategoria.Find(id);
                if (propiedadesCategoria == null)
                {
                    return HttpNotFound();
                }
                return View(propiedadesCategoria);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: PropiedadesCategorias/Create
        public ActionResult Create()
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PropiedadesCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCategoria,Categoria,Clave")] PropiedadesCategoria propiedadesCategoria)
        {
            if (ModelState.IsValid)
            {
                propiedadesCategoria.Activo = true;
                db.PropiedadesCategoria.Add(propiedadesCategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(propiedadesCategoria);
        }

        public void Excel()
        {
            var model = db.PropiedadesCategoria.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "PropiedadesCategorias");
        }


        // GET: PropiedadesCategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PropiedadesCategoria propiedadesCategoria = db.PropiedadesCategoria.Find(id);
                if (propiedadesCategoria == null)
                {
                    return HttpNotFound();
                }
                return View(propiedadesCategoria);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PropiedadesCategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCategoria,Categoria,Clave,Activo")] PropiedadesCategoria propiedadesCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propiedadesCategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propiedadesCategoria);
        }

        // GET: PropiedadesCategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PropiedadesCategoria propiedadesCategoria = db.PropiedadesCategoria.Find(id);
                if (propiedadesCategoria == null)
                {
                    return HttpNotFound();
                }
                return View(propiedadesCategoria);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PropiedadesCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PropiedadesCategoria propiedadesCategoria = db.PropiedadesCategoria.Find(id);
            db.PropiedadesCategoria.Remove(propiedadesCategoria);
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
