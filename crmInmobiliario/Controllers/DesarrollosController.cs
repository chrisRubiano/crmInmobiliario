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
    public class DesarrollosController : Controller
    {
        private CRMINMOBILIARIOEntities10 db = new CRMINMOBILIARIOEntities10();

        // GET: Desarrollos
        public ActionResult Index()
        {
            return View(db.Desarrollos.OrderByDescending(d => d.IdDesarrollo).ToList());
        }

        // GET: Desarrollos/Details/5
        public ActionResult Details(int? id)
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

        // GET: Desarrollos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Desarrollos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDesarrollo,Desarrollo,Clave,Descuento,CajonesEstacionamiento")] Desarrollos desarrollos)
        {
            if (ModelState.IsValid)
            {
                db.Desarrollos.Add(desarrollos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(desarrollos);
        }



        //public ActionResult ListaDesarrollos(string desarrollo)
        //{
        //    var desarrolloList = new List<string>();
        //    var desarrolloQry = from d in db.Desarrollos.Where(d => d.Activo == true)
        //                        orderby d.Desarrollo
        //                        select d.Desarrollo;

        //    desarrolloList.AddRange(desarrolloQry);

        //    ViewBag.desarrollo = new SelectList(desarrolloList);

        //    var desarrollos = from d in db.Desarrollos
        //                      select d;

        //    if (!string.IsNullOrEmpty(desarrollo))
        //    {
        //        desarrollos = from d in db.Desarrollos
        //                      select d;
        //        desarrollos = desarrollos.Where(s => s.Desarrollo == desarrollo);
        //    }

        //    return View(desarrollos.ToList());
        //}

        //controller Action
        public void Excel()
        {
            var model = db.Desarrollos.ToList();

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
                Response.AddHeader("content-disposition", "attachment; filename=Desarrollos.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }


        // GET: Desarrollos/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Desarrollos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDesarrollo,Desarrollo,Clave,Activo,Descuento,CajonesEstacionamiento")] Desarrollos desarrollos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(desarrollos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(desarrollos);
        }

        // GET: Desarrollos/Delete/5
        public ActionResult Delete(int? id)
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
