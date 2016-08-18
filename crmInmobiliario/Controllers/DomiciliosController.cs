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
    public class DomiciliosController : Controller
    {
        private CRMINMOBILIARIOEntities8 db = new CRMINMOBILIARIOEntities8();

        // GET: Domicilios
        public ActionResult Index()
        {
            var domicilios = db.Domicilios.Include(d => d.DomiciliosTipo).Include(d => d.Estados).Include(d => d.Municipios).Include(d => d.Paises).Include(d => d.Personas).Include(d => d.Propiedades).OrderByDescending(d => d.IdDomicilio);
            return View(domicilios.ToList());
        }

        // GET: Domicilios/Details/5
        public ActionResult Details(int? id, int idPersona, int tipo, int? categoriap)
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

            ViewBag.tipo = tipo;

            if (categoriap!=null)
            {
                ViewBag.categoriap = categoriap;
            }
            return View(domicilios);
        }

        public void Excel()
        {
            var model = db.Domicilios.ToList();

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
                Response.AddHeader("content-disposition", "attachment; filename=Domicilios.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }

        // GET: Domicilios/Create
        public ActionResult Create(int? idPersona, int tipo, int? categoriap)
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
            
            ViewBag.TipoDomicilio = tipoDom;
            ViewBag.IdPropiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo");
            Domicilios domicilios = new Domicilios();
            
            domicilios.TipoDomicilio = tipoDom;
            if (idPersona != 3)
            {
                domicilios.IdPersona = idPersona;
                ViewBag.IdPersona = idPersona;
            }
            {
                domicilios.IdPropiedad = idPersona;
                ViewBag.idPropiedad = idPersona;
            }

            if (categoriap!=null)
            {
                ViewBag.categoriap = categoriap;
            }
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
                if (tipo != 3)
                {
                    domicilios.IdPropiedad = null;
                    domicilios.IdPersona = idPersona;
                    domicilios.TipoDomicilio = tipo;
                }
                else
                {
                    domicilios.IdPersona = null;
                    domicilios.IdPropiedad = idPersona;
                    domicilios.TipoDomicilio = tipo;
                }
                
            
                db.Domicilios.Add(domicilios);
                db.SaveChanges();
                if (tipo!=3)
                {
                    return Redirect("/Personas/Details/" + idPersona.ToString());
                }else
                {
                    return Redirect("/Propiedades/Details/" + idPersona.ToString());
                }
                
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
        public ActionResult Edit(int? id, int idPersona, int tipo, int? categoriap)
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
            ViewBag.tipo = tipo;
            if (categoriap!=null)
            {
                ViewBag.categoriap = categoriap;
            }
            return View(domicilios);
        }

        // POST: Domicilios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDomicilio,TipoDomicilio,IdPersona,IdPropiedad,Calle,NumExterior,NumInterior,EntreEsquina,YCalle,Colonia,CP,Localidad,Municipio,Estado,Pais,Longitud,Latitud")] Domicilios domicilios, int idPersona, int tipo)
        {
            if (ModelState.IsValid)
            {
                if (tipo != 3)
                {
                    domicilios.IdPropiedad = null;
                    domicilios.IdPersona = idPersona;
                    domicilios.TipoDomicilio = tipo;
                }
                else
                {
                    domicilios.IdPersona = null;
                    domicilios.IdPropiedad = idPersona;
                    domicilios.TipoDomicilio = tipo;
                }


                db.Entry(domicilios).State = EntityState.Modified;
                db.SaveChanges();
                if (tipo != 3)
                {
                    return Redirect("/Personas/Details/" + idPersona.ToString());
                }
                else
                {
                    return Redirect("/Propiedades/Details/" + idPersona.ToString());
                }
                
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
        public ActionResult Delete(int? id, int idPersona, int tipo, int? categoriap)
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
            ViewBag.tipo = tipo;
            if (categoriap != null)
            {
                ViewBag.categoriap = categoriap;
            }
            return View(domicilios);
        }

        // POST: Domicilios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int idPersona, int tipo)
        {
            Domicilios domicilios = db.Domicilios.Find(id);
            db.Domicilios.Remove(domicilios);
            db.SaveChanges();
            if (tipo != 3)
            {
                return Redirect("/Personas/Details/" + idPersona.ToString());
            }
            else
            {
                return Redirect("/Propiedades/Details/" + idPersona.ToString());
            }

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
