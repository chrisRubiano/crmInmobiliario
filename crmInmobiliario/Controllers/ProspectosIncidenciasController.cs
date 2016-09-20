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
    public class ProspectosIncidenciasController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }


        // GET: ProspectosIncidencias
        public ActionResult Index()
        {
            var usuario = getUser();
            if (usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                return View(db.ProspectosIncidencias.Include(p => p.Personas).Include(p => p.AspNetUsers).ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //// GET: ProspectosIncidencias/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProspectosIncidencias prospectosIncidencias = db.ProspectosIncidencias.Find(id);
        //    if (prospectosIncidencias == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(prospectosIncidencias);
        //}

        // GET: ProspectosIncidencias/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ProspectosIncidencias/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Prospecto,UsuarioRegistro,UsuarioIncidencia,FechaRegistro")] ProspectosIncidencias prospectosIncidencias)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ProspectosIncidencias.Add(prospectosIncidencias);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(prospectosIncidencias);
        //}

        // GET: ProspectosIncidencias/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProspectosIncidencias prospectosIncidencias = db.ProspectosIncidencias.Find(id);
        //    if (prospectosIncidencias == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(prospectosIncidencias);
        //}

        //// POST: ProspectosIncidencias/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Prospecto,UsuarioRegistro,UsuarioIncidencia,FechaRegistro")] ProspectosIncidencias prospectosIncidencias)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(prospectosIncidencias).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(prospectosIncidencias);
        //}

        // GET: ProspectosIncidencias/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ProspectosIncidencias prospectosIncidencias = db.ProspectosIncidencias.Find(id);
                if (prospectosIncidencias == null)
                {
                    return HttpNotFound();
                }
                return View(prospectosIncidencias);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: ProspectosIncidencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProspectosIncidencias prospectosIncidencias = db.ProspectosIncidencias.Find(id);
            db.ProspectosIncidencias.Remove(prospectosIncidencias);
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
