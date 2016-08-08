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
    public class NotasController : Controller
    {
        private CRMINMOBILIARIOEntities7 db = new CRMINMOBILIARIOEntities7();

        // GET: Notas
        public ActionResult Index()
        {
            var notas = db.Notas.Include(n => n.Personas);
            return View(notas.ToList());
        }

        // GET: Notas/Details/5
        public ActionResult Details(int? id, int? categoriap)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notas notas = db.Notas.Find(id);
            if (notas == null)
            {
                return HttpNotFound();
            }

            ViewBag.categoriap = categoriap;
            return View(notas);
        }

        public void Excel()
        {
            var model = db.Notas.ToList();

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
                Response.AddHeader("content-disposition", "attachment; filename=Notas.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }

        // GET: Notas/Create
        public ActionResult Create(int ? idPersona, int? categoriap)
        {
            var personasList = new List<string>();
            var notaPersonaQry = from d in db.Personas where d.IdPersona == idPersona select d.Nombre+" "+d.Paterno+" "+d.Materno;
            personasList.AddRange(notaPersonaQry);
            ViewBag.Persona = personasList[0];
            Notas notas = new Notas();
            ViewBag.idPersona = idPersona;
            ViewBag.categoriap = categoriap;
            return View(notas);
        }

        // POST: Notas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNota,Persona,Nota,Fecha,Usuario")] Notas notas, int idPersona)
        {
            if (ModelState.IsValid)
            {
                notas.Persona = idPersona;
                notas.Fecha = DateTime.Now;
                db.Notas.Add(notas);
                db.SaveChanges();
                //return RedirectToAction("Index", "Personas");
                return Redirect("/Personas/Details/" + idPersona.ToString());
            }

            //ViewBag.Persona = db.Personas.Find(idPersona);
            return View(notas);
        }

        // GET: Notas/Edit/5
        public ActionResult Edit(int? id, int idPersona, int? categoriap)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notas notas = db.Notas.Find(id);
            if (notas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Persona = idPersona;
            ViewBag.categoriap = categoriap;
            return View(notas);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNota,Persona,Nota,Fecha,Usuario")] Notas notas, int idPersona)
        {
            if (ModelState.IsValid)
            {
                notas.Fecha = DateTime.Now;
                notas.Persona = idPersona;
                db.Entry(notas).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Personas/Details/" + idPersona.ToString());
            }
            ViewBag.Persona = idPersona;
            return View(notas);
        }

        // GET: Notas/Delete/5
        public ActionResult Delete(int? id, int? categoriap)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notas notas = db.Notas.Find(id);
            if (notas == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoriap = categoriap;
            return View(notas);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notas notas = db.Notas.Find(id);
            var persona = notas.Persona;
            db.Notas.Remove(notas);
            db.SaveChanges();
            return Redirect("/Personas/Details/" + persona.ToString());
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
