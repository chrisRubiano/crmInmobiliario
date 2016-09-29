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
    public class FormasPagoController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();


        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }


        // GET: FormasPago
        public ActionResult Index()
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" )
            {
                return View(db.FormasPago.ToList());
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // GET: FormasPago/Details/5
        public ActionResult Details(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" )
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                FormasPago formasPago = db.FormasPago.Find(id);
                if (formasPago == null)
                {
                    return HttpNotFound();
                }
                return View(formasPago);
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // GET: FormasPago/Create
        public ActionResult Create()
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" )
            {
              return View();
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // POST: FormasPago/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFormaPago,FormaPago,ERP,ERP2")] FormasPago formasPago)
        {
            if (ModelState.IsValid)
            {
                db.FormasPago.Add(formasPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(formasPago);
        }


        public void Excel()
        {
            var model = db.FormasPago.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "Formas de Pago");
        }

        // GET: FormasPago/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" )
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                FormasPago formasPago = db.FormasPago.Find(id);
                if (formasPago == null)
                {
                    return HttpNotFound();
                }
                return View(formasPago);
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // POST: FormasPago/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFormaPago,FormaPago,ERP,ERP2")] FormasPago formasPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formasPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formasPago);
        }

        // GET: FormasPago/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" )
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                FormasPago formasPago = db.FormasPago.Find(id);
                if (formasPago == null)
                {
                    return HttpNotFound();
                }
                return View(formasPago);
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // POST: FormasPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FormasPago formasPago = db.FormasPago.Find(id);
            db.FormasPago.Remove(formasPago);
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
