using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using crmInmobiliario.Models;
using System.IO;
using System.Web.UI;
using crmInmobiliario.Utilidades;

namespace crmInmobiliario.Controllers
{
    [Authorize]
    public class DesarrollosController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }


        // GET: Desarrollos
        public ActionResult Index()
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                return View(db.Desarrollos.OrderByDescending(d => d.IdDesarrollo).ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Desarrollos/Details/5
        public ActionResult Details(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Desarrollos desarrollos = db.Desarrollos.Find(id);
                if (desarrollos == null)
                {
                    return HttpNotFound();
                }
                return View(desarrollos);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Desarrollos/Create
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

        // POST: Desarrollos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDesarrollo,Desarrollo,Clave,Activo,Descuento,CajonesEstacionamiento,ERP,FechaEntrega")] Desarrollos desarrollos, HttpPostedFileBase imgLogo)
        {
            if (ModelState.IsValid)
            {
                if (imgLogo != null && imgLogo.ContentLength > 0)
                {
                    desarrollos.Logo = new byte[imgLogo.ContentLength];
                    imgLogo.InputStream.Read(desarrollos.Logo, 0, imgLogo.ContentLength);
                }

                desarrollos.Activo = true;
                db.Desarrollos.Add(desarrollos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(desarrollos);
        }

       

        //controller Action
        public void Excel()
        {
            var model = db.Desarrollos.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "Desarrollos");
        }

      


        // GET: Desarrollos/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Desarrollos desarrollos = db.Desarrollos.Find(id);
                if (desarrollos == null)
                {
                    return HttpNotFound();
                }
                return View(desarrollos);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Desarrollos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDesarrollo,Desarrollo,Clave,Activo,Descuento,CajonesEstacionamiento,ERP,FechaEntrega")] Desarrollos desarrollos, HttpPostedFileBase imgLogo)
        {
            if (ModelState.IsValid)
            {
                if (imgLogo != null && imgLogo.ContentLength > 0)
                {
                    desarrollos.Logo = new byte[imgLogo.ContentLength];
                    imgLogo.InputStream.Read(desarrollos.Logo, 0, imgLogo.ContentLength);
                }

                db.Entry(desarrollos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(desarrollos);
        }

        // GET: Desarrollos/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Desarrollos desarrollos = db.Desarrollos.Find(id);
                if (desarrollos == null)
                {
                    return HttpNotFound();
                }
                return View(desarrollos);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Desarrollos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Desarrollos desarrollos = db.Desarrollos.Find(id);
            db.Desarrollos.Remove(desarrollos);
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
