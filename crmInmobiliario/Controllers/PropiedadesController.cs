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
    public class PropiedadesController : Controller
    {
        private CRMINMOBILIARIOEntities4 db = new CRMINMOBILIARIOEntities4();

        // GET: Propiedades
        public ActionResult Index()
        {
            var propiedades = db.Propiedades.Include(p => p.Desarrollos).Include(p => p.Monedas).Include(p => p.PropiedadesAcabados).Include(p => p.PropiedadesAntiguedad).Include(p => p.PropiedadesTipo).Include(p => p.PropiedadesTiposOperacion);
            return View(propiedades.ToList());
        }

        // GET: Propiedades/Details/5
        public ActionResult Details(int? id)
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

        // GET: Propiedades/Create
        public ActionResult Create()
        {
            ViewBag.Desarrollo = new SelectList(db.Desarrollos, "IdDesarrollo", "Desarrollo");
            ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda");
            ViewBag.Acabados = new SelectList(db.PropiedadesAcabados, "IdAcabado", "Acabado");
            ViewBag.Antiguedad = new SelectList(db.PropiedadesAntiguedad, "IdAntiguedad", "Antiguedad");
            ViewBag.TipoPropiedad = new SelectList(db.PropiedadesTipo, "IdTipoPropiedad", "TipoPropiedad");
            ViewBag.TipoOperacion = new SelectList(db.PropiedadesTiposOperacion, "IdTipoOperacion", "TipoOperacion");
            return View();
        }

        // POST: Propiedades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPropiedad,Desarrollo,TipoPropiedad,TipoOperacion,VentaPrecio,RentaPrecio,RentaTarifaDiaria,RentaTarifaSemanal,RentaTarifaMensual,RentaEstadiaMinima,Titulo,Descripcion,Moneda,Recamaras,PreparacionBanio,IncluyeInstalacionBanio,Banios,MedioBanios,Estacionamientos,Construccion,Terreno,LargoTerreno,FrenteTerreno,Acabados,AcabadosEspecifique,Antiguedad,PisoEnQueSeEncuentra,CantidadPisos,MantenimientoMensual,Codigo,Observaciones,Usuario,FechaRegistro,UsuarioUA,FechaUA")] Propiedades propiedades)
        {
            if (ModelState.IsValid)
            {
                db.Propiedades.Add(propiedades);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Desarrollo = new SelectList(db.Desarrollos, "IdDesarrollo", "Desarrollo", propiedades.Desarrollo);
            ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda", propiedades.Moneda);
            ViewBag.Acabados = new SelectList(db.PropiedadesAcabados, "IdAcabado", "Acabado", propiedades.Acabados);
            ViewBag.Antiguedad = new SelectList(db.PropiedadesAntiguedad, "IdAntiguedad", "Antiguedad", propiedades.Antiguedad);
            ViewBag.TipoPropiedad = new SelectList(db.PropiedadesTipo, "IdTipoPropiedad", "TipoPropiedad", propiedades.TipoPropiedad);
            ViewBag.TipoOperacion = new SelectList(db.PropiedadesTiposOperacion, "IdTipoOperacion", "TipoOperacion", propiedades.TipoOperacion);
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
            ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda", propiedades.Moneda);
            ViewBag.Acabados = new SelectList(db.PropiedadesAcabados, "IdAcabado", "Acabado", propiedades.Acabados);
            ViewBag.Antiguedad = new SelectList(db.PropiedadesAntiguedad, "IdAntiguedad", "Antiguedad", propiedades.Antiguedad);
            ViewBag.TipoPropiedad = new SelectList(db.PropiedadesTipo, "IdTipoPropiedad", "TipoPropiedad", propiedades.TipoPropiedad);
            ViewBag.TipoOperacion = new SelectList(db.PropiedadesTiposOperacion, "IdTipoOperacion", "TipoOperacion", propiedades.TipoOperacion);
            return View(propiedades);
        }

        // POST: Propiedades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPropiedad,Desarrollo,TipoPropiedad,TipoOperacion,VentaPrecio,RentaPrecio,RentaTarifaDiaria,RentaTarifaSemanal,RentaTarifaMensual,RentaEstadiaMinima,Titulo,Descripcion,Moneda,Recamaras,PreparacionBanio,IncluyeInstalacionBanio,Banios,MedioBanios,Estacionamientos,Construccion,Terreno,LargoTerreno,FrenteTerreno,Acabados,AcabadosEspecifique,Antiguedad,PisoEnQueSeEncuentra,CantidadPisos,MantenimientoMensual,Codigo,Observaciones,Usuario,FechaRegistro,UsuarioUA,FechaUA")] Propiedades propiedades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propiedades).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Desarrollo = new SelectList(db.Desarrollos, "IdDesarrollo", "Desarrollo", propiedades.Desarrollo);
            ViewBag.Moneda = new SelectList(db.Monedas, "IdMoneda", "Moneda", propiedades.Moneda);
            ViewBag.Acabados = new SelectList(db.PropiedadesAcabados, "IdAcabado", "Acabado", propiedades.Acabados);
            ViewBag.Antiguedad = new SelectList(db.PropiedadesAntiguedad, "IdAntiguedad", "Antiguedad", propiedades.Antiguedad);
            ViewBag.TipoPropiedad = new SelectList(db.PropiedadesTipo, "IdTipoPropiedad", "TipoPropiedad", propiedades.TipoPropiedad);
            ViewBag.TipoOperacion = new SelectList(db.PropiedadesTiposOperacion, "IdTipoOperacion", "TipoOperacion", propiedades.TipoOperacion);
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
