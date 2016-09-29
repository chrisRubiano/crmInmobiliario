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
    public class DocumentosCategoriasController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }


        // GET: DocumentosCategorias
        public ActionResult Index()
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "LEGAL" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                return View(db.DocumentosCategoria.ToList());
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // GET: DocumentosCategorias/Details/5
        public ActionResult Details(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "LEGAL" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DocumentosCategoria documentosCategoria = db.DocumentosCategoria.Find(id);
                if (documentosCategoria == null)
                {
                    return HttpNotFound();
                }
                return View(documentosCategoria);
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // GET: DocumentosCategorias/Create
        public ActionResult Create()
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "LEGAL" || usuario.UserRoles == "DIR-GENERAL")
            {
                return View();
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // POST: DocumentosCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCategoria,Categoria")] DocumentosCategoria documentosCategoria)
        {
            if (ModelState.IsValid)
            {
                db.DocumentosCategoria.Add(documentosCategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documentosCategoria);
        }

        //controller Action
        public void Excel()
        {
            var model = db.DocumentosCategoria.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "Categoria de Documentos");
        }


        // GET: DocumentosCategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "LEGAL" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DocumentosCategoria documentosCategoria = db.DocumentosCategoria.Find(id);
                if (documentosCategoria == null)
                {
                    return HttpNotFound();
                }
                return View(documentosCategoria);
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // POST: DocumentosCategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCategoria,Categoria")] DocumentosCategoria documentosCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentosCategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentosCategoria);
        }

        // GET: DocumentosCategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "LEGAL" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DocumentosCategoria documentosCategoria = db.DocumentosCategoria.Find(id);
                if (documentosCategoria == null)
                {
                    return HttpNotFound();
                }
                return View(documentosCategoria);
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // POST: DocumentosCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentosCategoria documentosCategoria = db.DocumentosCategoria.Find(id);
            db.DocumentosCategoria.Remove(documentosCategoria);
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
