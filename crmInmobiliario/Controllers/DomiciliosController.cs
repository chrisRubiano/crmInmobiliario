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
    public class DomiciliosController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        // GET: Domicilios
        public ActionResult Index()
        {
            var domicilios = db.Domicilios.Include(d => d.DomiciliosTipo).Include(d => d.Estados).Include(d => d.Municipios).Include(d => d.Paises).Include(d => d.Personas).Include(d => d.Propiedades);
            return View(domicilios.ToList());
        }

        // GET: Domicilios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domicilios domicilios = db.Domicilios.Find(id);
            if (domicilios == null)
            {
                return HttpNotFound();
            }
            return View(domicilios);
        }

        // GET: Domicilios/Create
        public ActionResult Create(int? idPersona, int tipo)
        {

            /*
             Tipos de domicilio:            Categorias de persona:
             1 prospecto                    1 Prospecto
             2 cliente                      2 Cliente
             3 propiedad                    3 Vendedor
             4 vendedor                     4 Manager
             5 manager
             */
            var tipoDom = 0;

            if(tipo <= 2)
            {
                tipoDom = tipo;
            }else if (tipo > 2 && tipo < 5 )
            {
                tipoDom = tipo + 1;
            }else
            {
                tipoDom = 3; 
            }

            
            ViewBag.Estado = new SelectList(db.Estados, "IdEstado", "Estado");
            ViewBag.Municipio = new SelectList(db.Municipios, "IdMunicipio", "Municipio");
            ViewBag.Pais = new SelectList(db.Paises, "IdPais", "Pais");
            //ViewBag.IdPersona = new SelectList(db.Personas, "IdPersona", "Nombre");
            ViewBag.IdPersona = idPersona;
            ViewBag.TipoDomicilio = tipoDom;
            ViewBag.IdPropiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo");
            Domicilios domicilios = new Domicilios();
            domicilios.IdPersona = idPersona;
            domicilios.TipoDomicilio = tipoDom;
            return View(domicilios);
        }

        // POST: Domicilios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDomicilio,TipoDomicilio,IdPersona,IdPropiedad,Calle,NumExterior,NumInterior,EntreEsquina,YCalle,Colonia,CP,Localidad,Municipio,Estado,Pais,Longitud,Latitud")] Domicilios domicilios, int idPersona, int tipo)
        {
            if (ModelState.IsValid)
            {
                domicilios.IdPersona = idPersona;
                domicilios.TipoDomicilio = tipo;
            
                db.Domicilios.Add(domicilios);
                db.SaveChanges();
                return Redirect("/Personas/Details/" + idPersona.ToString());
            }

            ViewBag.IdPersona = idPersona;
            ViewBag.TipoDomicilio = tipo;
            ViewBag.Estado = new SelectList(db.Estados, "IdEstado", "Estado", domicilios.Estado);
            ViewBag.Municipio = new SelectList(db.Municipios, "IdMunicipio", "Municipio", domicilios.Municipio);
            ViewBag.Pais = new SelectList(db.Paises, "IdPais", "Pais", domicilios.Pais);
            //ViewBag.IdPersona = new SelectList(db.Personas, "IdPersona", "Nombre", domicilios.IdPersona);
            ViewBag.IdPropiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", domicilios.IdPropiedad);
            return View(domicilios);
        }

        // GET: Domicilios/Edit/5
        public ActionResult Edit(int? id, int idPersona)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domicilios domicilios = db.Domicilios.Find(id);
            if (domicilios == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoDomicilio = new SelectList(db.DomiciliosTipo, "IdTipoDomicilio", "TipoDomicilio", domicilios.TipoDomicilio);
            ViewBag.Estado = new SelectList(db.Estados, "IdEstado", "Estado", domicilios.Estado);
            ViewBag.Municipio = new SelectList(db.Municipios, "IdMunicipio", "Municipio", domicilios.Municipio);
            ViewBag.Pais = new SelectList(db.Paises, "IdPais", "Pais", domicilios.Pais);
            ViewBag.IdPersona = new SelectList(db.Personas, "IdPersona", "Nombre", domicilios.IdPersona);
            ViewBag.IdPropiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", domicilios.IdPropiedad);
            return View(domicilios);
        }

        // POST: Domicilios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDomicilio,TipoDomicilio,IdPersona,IdPropiedad,Calle,NumExterior,NumInterior,EntreEsquina,YCalle,Colonia,CP,Localidad,Municipio,Estado,Pais,Longitud,Latitud")] Domicilios domicilios, int idPersona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(domicilios).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Personas/Details/" + idPersona.ToString());
            }
            ViewBag.TipoDomicilio = new SelectList(db.DomiciliosTipo, "IdTipoDomicilio", "TipoDomicilio", domicilios.TipoDomicilio);
            ViewBag.Estado = new SelectList(db.Estados, "IdEstado", "Estado", domicilios.Estado);
            ViewBag.Municipio = new SelectList(db.Municipios, "IdMunicipio", "Municipio", domicilios.Municipio);
            ViewBag.Pais = new SelectList(db.Paises, "IdPais", "Pais", domicilios.Pais);
            ViewBag.IdPersona = new SelectList(db.Personas, "IdPersona", "Nombre", domicilios.IdPersona);
            ViewBag.IdPropiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", domicilios.IdPropiedad);
            return View(domicilios);
        }

        // GET: Domicilios/Delete/5
        public ActionResult Delete(int? id, int idPersona)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domicilios domicilios = db.Domicilios.Find(id);
            if (domicilios == null)
            {
                return HttpNotFound();
            }
            return View(domicilios);
        }

        // POST: Domicilios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int idPersona)
        {
            Domicilios domicilios = db.Domicilios.Find(id);
            db.Domicilios.Remove(domicilios);
            db.SaveChanges();
            return Redirect("/Personas/Details/" + idPersona.ToString());
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
