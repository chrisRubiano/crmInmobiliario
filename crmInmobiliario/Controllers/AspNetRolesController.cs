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
    public class AspNetRolesController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }


        // GET: AspNetRoles
        public ActionResult Index()
        {
            var usuario = getUser();
            if (usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                var roles = from r in db.AspNetRoles
                            select r;
                return View(roles.OrderBy(r => r.Name).ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        // GET: AspNetRoles/Details/5
        public ActionResult Details(string id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetRoles aspNetRoles = db.AspNetRoles.Find(id);
                if (aspNetRoles == null)
                {
                    return HttpNotFound();
                }
                return View(aspNetRoles);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: AspNetRoles/Create
        public ActionResult Create()
        {
            var usuario = getUser();
            if (usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: AspNetRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] AspNetRoles aspNetRoles)
        {
            if (ModelState.IsValid)
            {
                db.AspNetRoles.Add(aspNetRoles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetRoles);
        }

        public void Excel()
        {
            var model = db.MediosEnterarse.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "Roles");
        }


        // GET: AspNetRoles/Edit/5
        public ActionResult Edit(string id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetRoles aspNetRoles = db.AspNetRoles.Find(id);
                if (aspNetRoles == null)
                {
                    return HttpNotFound();
                }
                return View(aspNetRoles);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: AspNetRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] AspNetRoles aspNetRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetRoles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetRoles);
        }

        // GET: AspNetRoles/Delete/5
        public ActionResult Delete(string id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AspNetRoles aspNetRoles = db.AspNetRoles.Find(id);
                if (aspNetRoles == null)
                {
                    return HttpNotFound();
                }
                return View(aspNetRoles);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: AspNetRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetRoles aspNetRoles = db.AspNetRoles.Find(id);
            db.AspNetRoles.Remove(aspNetRoles);
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
