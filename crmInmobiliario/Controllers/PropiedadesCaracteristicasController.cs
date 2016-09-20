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

namespace crmInmobiliario.Controllers
{
    [Authorize]
    public class PropiedadesCaracteristicasController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }


        // GET: PropiedadesCaracteristicas
        public ActionResult Index()
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT")
            {
                var propiedadesCaracteristicas = db.PropiedadesCaracteristicas.Include(p => p.PropiedadesCaracteristicasCategorias).OrderByDescending(p => p.IdCaracteristica);
                return View(propiedadesCaracteristicas.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: PropiedadesCaracteristicas/Details/5
        public ActionResult Details(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PropiedadesCaracteristicas propiedadesCaracteristicas = db.PropiedadesCaracteristicas.Find(id);
                if (propiedadesCaracteristicas == null)
                {
                    return HttpNotFound();
                }
                return View(propiedadesCaracteristicas);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public void Excel()
        {
            var model = db.PropiedadesCaracteristicas.ToList();

            Export export = new Export();
            export.ToExcel(Response, model);
        }

        //helper class
        public class Export
        {
            public void ToExcel(HttpResponseBase Response, object clientsList)
            {
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = clientsList;
                grid.DataBind();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=Amenidades.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }

        // GET: PropiedadesCaracteristicas/Create
        public ActionResult Create()
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                ViewBag.Categoria = new SelectList(db.PropiedadesCaracteristicasCategorias, "IdCategoria", "Categoria");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PropiedadesCaracteristicas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCaracteristica,Categoria,Caracteristica,Clave")] PropiedadesCaracteristicas propiedadesCaracteristicas)
        {
            if (ModelState.IsValid)
            {
                db.PropiedadesCaracteristicas.Add(propiedadesCaracteristicas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categoria = new SelectList(db.PropiedadesCaracteristicasCategorias, "IdCategoria", "Categoria", propiedadesCaracteristicas.Categoria);
            return View(propiedadesCaracteristicas);
        }

        // GET: PropiedadesCaracteristicas/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PropiedadesCaracteristicas propiedadesCaracteristicas = db.PropiedadesCaracteristicas.Find(id);
                if (propiedadesCaracteristicas == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Categoria = new SelectList(db.PropiedadesCaracteristicasCategorias, "IdCategoria", "Categoria", propiedadesCaracteristicas.Categoria);
                return View(propiedadesCaracteristicas);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PropiedadesCaracteristicas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCaracteristica,Categoria,Caracteristica,Clave")] PropiedadesCaracteristicas propiedadesCaracteristicas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propiedadesCaracteristicas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categoria = new SelectList(db.PropiedadesCaracteristicasCategorias, "IdCategoria", "Categoria", propiedadesCaracteristicas.Categoria);
            return View(propiedadesCaracteristicas);
        }

        // GET: PropiedadesCaracteristicas/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PropiedadesCaracteristicas propiedadesCaracteristicas = db.PropiedadesCaracteristicas.Find(id);
                if (propiedadesCaracteristicas == null)
                {
                    return HttpNotFound();
                }
                return View(propiedadesCaracteristicas);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PropiedadesCaracteristicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PropiedadesCaracteristicas propiedadesCaracteristicas = db.PropiedadesCaracteristicas.Find(id);
            db.PropiedadesCaracteristicas.Remove(propiedadesCaracteristicas);
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
