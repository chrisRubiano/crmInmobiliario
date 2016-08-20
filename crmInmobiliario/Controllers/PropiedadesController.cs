using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using crmInmobiliario.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web.UI;

namespace crmInmobiliario.Controllers
{
    [Authorize]
    public class PropiedadesController : Controller
    {
        private CRMINMOBILIARIOEntities10 db = new CRMINMOBILIARIOEntities10();

        // GET: Propiedades
        public ActionResult Index()
        {
            var propiedades = db.Propiedades.Include(p => p.Desarrollos).Include(p => p.Monedas).Include(p => p.PropiedadesAcabados).Include(p => p.PropiedadesCategoria).OrderByDescending(p => p.IdPropiedad).Include(p => p.PropiedadesTipoBanios);
            return View(propiedades.ToList());
        }

        public ActionResult Filtro(Boolean? Terraza, Boolean? Bodega, Boolean? Estacionamiento, string titulo, string nivel,int? pMenor, int? pMayor, int desarrollo = 0, int categoria = 0)
        {

            var propiedades = db.Propiedades.Include(p => p.Desarrollos).Include(p => p.Edificios).Include(p => p.Monedas).Include(p => p.PropiedadesAcabados).Include(p => p.PropiedadesCategoria).Include(p => p.PropiedadesTipoBanios);

            if (Terraza.HasValue && Bodega.HasValue && Estacionamiento.HasValue)
            { 
                if (Terraza.Value || Bodega.Value || Estacionamiento.Value )
                {
                    if (Terraza != null)
                    {
                        if (Terraza==true)
                        {
                            propiedades = propiedades.Where(p => p.Terraza == Terraza);
                        }
                    }

                    if (Bodega != null)
                    {
                        if (Bodega==true)
                        {
                            propiedades = propiedades.Where(p => p.Bodega == Bodega);
                        }
                    }

                    if (Estacionamiento != null)
                    {
                        if (Estacionamiento==true)
                        {
                            propiedades = propiedades.Where(p => p.Estacionamiento == Estacionamiento);
                        }
                    }
                }
            }

            /*--------------*/
            if (pMenor != null)
            {
                propiedades = propiedades.Where(p => p.VentaPrecio > pMenor);
            }
            if (pMayor != null)
            {
                propiedades = propiedades.Where(p => p.VentaPrecio < pMayor);
            }
            /*--------------*/

            if (desarrollo != 0)
            {
                propiedades = propiedades.Where(p => p.Desarrollo == desarrollo);
            }

            if (categoria != 0)
            {
                propiedades = propiedades.Where(p => p.Categoria == categoria);
            }

            if (!string.IsNullOrEmpty(titulo))
            {
                propiedades = propiedades.Where(p => p.Titulo.Contains( titulo));
            }

            if (!string.IsNullOrEmpty(nivel))
            {
                propiedades = propiedades.Where(p => p.Nivel.Contains(nivel));
            }
           
            propiedades = propiedades.Where(p => p.Activa == true).Where(p => p.Estatus.Value < 3);
            ViewBag.Desarrollo = new SelectList(db.Desarrollos, "IdDesarrollo", "Desarrollo");
            ViewBag.Categoria = new SelectList(db.PropiedadesCategoria, "IdCategoria", "Categoria");
            return View(propiedades.OrderByDescending(p => p.IdPropiedad).ToList());
        }


        public void Excel()
        {
            var model = db.Propiedades.ToList();

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
                Response.AddHeader("content-disposition", "attachment; filename=Propiedades.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }


        // GET: Propiedades/Details/5
        public ActionResult Details(int? id, bool filtro)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VariosModelos vModelos = new VariosModelos();
            Propiedades propiedades = db.Propiedades.Find(id);
            if (propiedades == null)
            {
                return HttpNotFound();
            }
            vModelos.propiedades = propiedades;

            var domicilios = db.Domicilios.Where(d => d.IdPropiedad == id).OrderByDescending(d => d.IdDomicilio);
            vModelos.domicilios = domicilios;

            ViewBag.filtro = filtro;
            ViewBag.Estatus = new SelectList(db.PropiedadesEstatus, "IdEstatus", "Estatus");

            return View(vModelos);
        }

        public ActionResult DetailsFiltro(int? id, bool filtro)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VariosModelos vModelos = new VariosModelos();
            Propiedades propiedades = db.Propiedades.Find(id);
            if (propiedades == null)
            {
                return HttpNotFound();
            }
            vModelos.propiedades = propiedades;

            var domicilios = db.Domicilios.Where(d => d.IdPropiedad == id).OrderByDescending(d => d.IdDomicilio);
            vModelos.domicilios = domicilios;

            ViewBag.filtro = filtro;
            return View(vModelos);
        }

        // GET: Propiedades/Create
        public ActionResult Create()
        {
            ViewBag.Desarrollo = new SelectList(db.Desarrollos, "IdDesarrollo", "Desarrollo");
            ViewBag.Edificio = new SelectList(db.Edificios, "IdEdificio", "Edificio");
            ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda");
            ViewBag.Acabados = new SelectList(db.PropiedadesAcabados, "IdAcabado", "Acabado");
            ViewBag.Categoria = new SelectList(db.PropiedadesCategoria, "IdCategoria", "Categoria");
            ViewBag.TipoBanio = new SelectList(db.PropiedadesTipoBanios, "IdTipoBanio", "TipoBanio");
            return View();
        }

        // POST: Propiedades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Bodega,Terraza,IdPropiedad,Desarrollo,Edificio,TipoPropiedad,TipoOperacion,VentaPrecio,RentaPrecio,RentaTarifaDiaria,RentaTarifaSemanal,RentaTarifaMensual,RentaEstadiaMinima,Titulo,Descripcion,Moneda,Recamaras,PreparacionBanio,Banios,MedioBanios,Construccion,Terreno,LargoTerreno,FrenteTerreno,Acabados,AcabadosEspecifique,Antiguedad,MantenimientoMensual,Codigo,Observaciones,Usuario,FechaRegistro,UsuarioUA,FechaUA,Estacionamiento,CajonesEstacionamiento,M2Interiores,M2Terraza,M2Bodega,FrenteLocal,LargoLocal,Nivel,Niveles,Titulo,Categoria")] Propiedades propiedades)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    propiedades.Estatus = 1;
                    propiedades.Activa = true;
                    propiedades.Usuario = User.Identity.GetUserId().ToString();
                    propiedades.FechaRegistro = DateTime.Now;
                    db.Propiedades.Add(propiedades);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No es posible guardar los cambios, intente mas tarde. Si los cambios persisten favor de contactarse con un adminsitrador");
            }

            ViewBag.Desarrollo = new SelectList(db.Desarrollos, "IdDesarrollo", "Desarrollo", propiedades.Desarrollo);
            ViewBag.Edificio = new SelectList(db.Edificios, "IdEdificio", "Edificio", propiedades.Edificio);
            ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda", propiedades.Moneda);
            ViewBag.Acabados = new SelectList(db.PropiedadesAcabados, "IdAcabado", "Acabado", propiedades.Acabados);
            ViewBag.Categoria = new SelectList(db.PropiedadesCategoria, "IdCategoriaPropiedad", "Categoria", propiedades.Categoria);
            ViewBag.TipoBanio = new SelectList(db.PropiedadesTipoBanios, "IdTipoBanio", "TipoBanio");
            return View(propiedades);
        }

        // GET: Propiedades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propiedades propiedades = db.Propiedades.Find(id);
            if (propiedades == null)
            {
                return HttpNotFound();
            }
            ViewBag.Desarrollo = new SelectList(db.Desarrollos, "IdDesarrollo", "Desarrollo", propiedades.Desarrollo);
            ViewBag.Edificio = new SelectList(db.Edificios, "Idedificio", "Edificio", propiedades.Edificio);
            ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda", propiedades.Moneda);
            ViewBag.Acabados = new SelectList(db.PropiedadesAcabados, "IdAcabado", "Acabado", propiedades.Acabados);
            ViewBag.Categoria = new SelectList(db.PropiedadesCategoria, "IdCategoria", "Categoria", propiedades.Categoria);
            ViewBag.TipoBanio = new SelectList(db.PropiedadesTipoBanios, "IdTipoBanio", "TipoBanio");
            return View(propiedades);
        }

        // POST: Propiedades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPropiedad,Desarrollo,Edificio,TipoPropiedad,TipoOperacion,VentaPrecio,RentaPrecio,RentaTarifaDiaria,RentaTarifaSemanal,RentaTarifaMensual,RentaEstadiaMinima,Titulo,Descripcion,Moneda,Recamaras,PreparacionBanio,Banios,MedioBanios,Construccion,Terreno,LargoTerreno,FrenteTerreno,Acabados,AcabadosEspecifique,Antiguedad,MantenimientoMensual,Codigo,Observaciones,Usuario,FechaRegistro,UsuarioUA,FechaUA,Estacionamiento,CajonesEstacionamiento,M2Interiores,M2Terraza,M2Bodega,FrenteLocal,LargoLocal,Nivel,Niveles,SistemaAC,Reglamento,URLReglamento, PrecioM2Interiores, Bodega, M2Bodega, PrecioM2Bodega, Terraza, M2Terraza, PrecioM2Terraza, PrecioEstacionamiento, M2Estacionamiento, PrecioM2Estacionamiento, Titulo")] Propiedades propiedades)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(propiedades).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No es posible guardar los cambios, intente mas tarde. Si los cambios persisten favor de contactarse con un adminsitrador");
            }


            ViewBag.Desarrollo = new SelectList(db.Desarrollos, "IdDesarrollo", "Desarrollo", propiedades.Desarrollo);
            ViewBag.Edificios = new SelectList(db.Edificios, "Idedificio", "Edificio", propiedades.Edificio);
            ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda", propiedades.Moneda);
            ViewBag.Acabados = new SelectList(db.PropiedadesAcabados, "IdAcabado", "Acabado", propiedades.Acabados);
            ViewBag.Categoria = new SelectList(db.PropiedadesCategoria, "IdCategoriaPropiedad", "Categoria", propiedades.Categoria);
            ViewBag.TipoBanio = new SelectList(db.PropiedadesTipoBanios, "IdTipoBanio", "TipoBanio");
            return View(propiedades);
        }

        // GET: Propiedades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propiedades propiedades = db.Propiedades.Find(id);
            if (propiedades == null)
            {
                return HttpNotFound();
            }
            return View(propiedades);
        }

        // POST: Propiedades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Propiedades propiedades = db.Propiedades.Find(id);
            db.Propiedades.Remove(propiedades);
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
