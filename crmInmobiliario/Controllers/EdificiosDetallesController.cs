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
    public class EdificiosDetallesController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();
        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }


        // GET: EdificiosDetalles
        public ActionResult Index()
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                var edificiosDetalle = db.EdificiosDetalle.Include(e => e.Edificios);
                return View(edificiosDetalle.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: EdificiosDetalles/Details/5
        public ActionResult Details(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: EdificiosDetalles/Create
        public ActionResult Create()
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                ViewBag.Edificio = new SelectList(db.Edificios, "IdEdificio", "Edificio");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: EdificiosDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEdificioDetalle,Edificio,Nivel,M2")] EdificiosDetalle edificiosDetalle)
        {
            if (ModelState.IsValid)
            {
                db.EdificiosDetalle.Add(edificiosDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Edificio = new SelectList(db.Edificios, "IdEdificio", "Edificio", edificiosDetalle.Edificio);
            return View(edificiosDetalle);
        }

        public void Excel()
        {
            var model = db.EdificiosDetalle.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "ConfiguracionesEdificios");
        }

        // GET: EdificiosDetalles/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
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
                ViewBag.Edificio = new SelectList(db.Edificios, "IdEdificio", "Edificio", edificiosDetalle.Edificio);
                return View(edificiosDetalle);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: EdificiosDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEdificioDetalle,Edificio,Nivel,M2")] EdificiosDetalle edificiosDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(edificiosDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Edificio = new SelectList(db.Edificios, "IdEdificio", "Edificio", edificiosDetalle.Edificio);
            return View(edificiosDetalle);
        }

        // GET: EdificiosDetalles/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: EdificiosDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EdificiosDetalle edificiosDetalle = db.EdificiosDetalle.Find(id);
            db.EdificiosDetalle.Remove(edificiosDetalle);
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
