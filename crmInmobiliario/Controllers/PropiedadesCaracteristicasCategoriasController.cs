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
    public class PropiedadesCaracteristicasCategoriasController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }


        // GET: PropiedadesCaracteristicasCategorias
        public ActionResult Index()
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                return View(db.PropiedadesCaracteristicasCategorias.OrderByDescending(p => p.IdCategoria).ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: PropiedadesCaracteristicasCategorias/Details/5
        public ActionResult Details(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PropiedadesCaracteristicasCategorias propiedadesCaracteristicasCategorias = db.PropiedadesCaracteristicasCategorias.Find(id);
                if (propiedadesCaracteristicasCategorias == null)
                {
                    return HttpNotFound();
                }
                return View(propiedadesCaracteristicasCategorias);
            }else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: PropiedadesCaracteristicasCategorias/Create
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

        // POST: PropiedadesCaracteristicasCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCategoria,Categoria")] PropiedadesCaracteristicasCategorias propiedadesCaracteristicasCategorias)
        {
            if (ModelState.IsValid)
            {
                db.PropiedadesCaracteristicasCategorias.Add(propiedadesCaracteristicasCategorias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(propiedadesCaracteristicasCategorias);
        }

        // GET: PropiedadesCaracteristicasCategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PropiedadesCaracteristicasCategorias propiedadesCaracteristicasCategorias = db.PropiedadesCaracteristicasCategorias.Find(id);
                if (propiedadesCaracteristicasCategorias == null)
                {
                    return HttpNotFound();
                }
                return View(propiedadesCaracteristicasCategorias);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PropiedadesCaracteristicasCategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCategoria,Categoria")] PropiedadesCaracteristicasCategorias propiedadesCaracteristicasCategorias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propiedadesCaracteristicasCategorias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propiedadesCaracteristicasCategorias);
        }

        // GET: PropiedadesCaracteristicasCategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PropiedadesCaracteristicasCategorias propiedadesCaracteristicasCategorias = db.PropiedadesCaracteristicasCategorias.Find(id);
                if (propiedadesCaracteristicasCategorias == null)
                {
                    return HttpNotFound();
                }
                return View(propiedadesCaracteristicasCategorias);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PropiedadesCaracteristicasCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PropiedadesCaracteristicasCategorias propiedadesCaracteristicasCategorias = db.PropiedadesCaracteristicasCategorias.Find(id);
            db.PropiedadesCaracteristicasCategorias.Remove(propiedadesCaracteristicasCategorias);
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
