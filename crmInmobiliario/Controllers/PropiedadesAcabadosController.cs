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
    public class PropiedadesAcabadosController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }


        // GET: PropiedadesAcabados
        public ActionResult Index()
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                return View(db.PropiedadesAcabados.OrderByDescending(p => p.IdAcabado).ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: PropiedadesAcabados/Details/5
        public ActionResult Details(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PropiedadesAcabados propiedadesAcabados = db.PropiedadesAcabados.Find(id);
                if (propiedadesAcabados == null)
                {
                    return HttpNotFound();
                }
                return View(propiedadesAcabados);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public void Excel()
        {
            var model = db.PropiedadesAcabados.ToList();

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
                Response.AddHeader("content-disposition", "attachment; filename=Acabados.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }

        // GET: PropiedadesAcabados/Create
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

        // POST: PropiedadesAcabados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAcabado,Acabado")] PropiedadesAcabados propiedadesAcabados)
        {
            if (ModelState.IsValid)
            {
                db.PropiedadesAcabados.Add(propiedadesAcabados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(propiedadesAcabados);
        }

        // GET: PropiedadesAcabados/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PropiedadesAcabados propiedadesAcabados = db.PropiedadesAcabados.Find(id);
                if (propiedadesAcabados == null)
                {
                    return HttpNotFound();
                }
                return View(propiedadesAcabados);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PropiedadesAcabados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAcabado,Acabado")] PropiedadesAcabados propiedadesAcabados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propiedadesAcabados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propiedadesAcabados);
        }

        // GET: PropiedadesAcabados/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuario = getUser();
            if (usuario.UserRoles == "ARQUITECTOS" || usuario.UserRoles == "DIR-GENERAL")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PropiedadesAcabados propiedadesAcabados = db.PropiedadesAcabados.Find(id);
                if (propiedadesAcabados == null)
                {
                    return HttpNotFound();
                }
                return View(propiedadesAcabados);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PropiedadesAcabados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PropiedadesAcabados propiedadesAcabados = db.PropiedadesAcabados.Find(id);
            db.PropiedadesAcabados.Remove(propiedadesAcabados);
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
