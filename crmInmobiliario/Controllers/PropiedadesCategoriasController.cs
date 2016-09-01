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
    public class PropiedadesCategoriasController : Controller
    {
        private CRMINMOBILIARIOEntities2 db = new CRMINMOBILIARIOEntities2();

        // GET: PropiedadesCategorias
        public ActionResult Index()
        {
            var categorias = from c in db.PropiedadesCategoria
                         select c;
            return View(categorias.OrderBy(c => c.Categoria).ToList());
            
        }

        // GET: PropiedadesCategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropiedadesCategoria propiedadesCategoria = db.PropiedadesCategoria.Find(id);
            if (propiedadesCategoria == null)
            {
                return HttpNotFound();
            }
            return View(propiedadesCategoria);
        }

        // GET: PropiedadesCategorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropiedadesCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCategoria,Categoria,Clave")] PropiedadesCategoria propiedadesCategoria)
        {
            if (ModelState.IsValid)
            {
                db.PropiedadesCategoria.Add(propiedadesCategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(propiedadesCategoria);
        }

        public void Excel()
        {
            var model = db.PropiedadesCategoria.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "PropiedadesCategorias");
        }


        // GET: PropiedadesCategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropiedadesCategoria propiedadesCategoria = db.PropiedadesCategoria.Find(id);
            if (propiedadesCategoria == null)
            {
                return HttpNotFound();
            }
            return View(propiedadesCategoria);
        }

        // POST: PropiedadesCategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCategoria,Categoria,Clave,Activo")] PropiedadesCategoria propiedadesCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propiedadesCategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propiedadesCategoria);
        }

        // GET: PropiedadesCategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropiedadesCategoria propiedadesCategoria = db.PropiedadesCategoria.Find(id);
            if (propiedadesCategoria == null)
            {
                return HttpNotFound();
            }
            return View(propiedadesCategoria);
        }

        // POST: PropiedadesCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PropiedadesCategoria propiedadesCategoria = db.PropiedadesCategoria.Find(id);
            db.PropiedadesCategoria.Remove(propiedadesCategoria);
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
