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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using crmInmobiliario.Utilidades;

namespace crmInmobiliario.Controllers
{
    [Authorize]
    public class CotizacionesController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        // GET: Cotizaciones
        public ActionResult Index()
        {
            var cotizaciones = db.Cotizaciones.Include(c => c.Personas).Include(c => c.Propiedades).OrderByDescending(c => c.IdCotizacion);
            return View(cotizaciones.ToList());
        }

        // GET: Cotizaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cotizaciones cotizaciones = db.Cotizaciones.Find(id);
            if (cotizaciones == null)
            {
                return HttpNotFound();
            }
            return View(cotizaciones);
        }

        public void Excel()
        {
            var model = db.Cotizaciones.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "Cotizaciones");
        }

        public ActionResult Pagos(int idPersona, int idPropiedad)
        {
            vmCotizacion vmcotizacion = new vmCotizacion();
            vmcotizacion.propiedades = db.Propiedades.Where(p => p.IdPropiedad == idPropiedad).FirstOrDefault();
            vmcotizacion.personas = db.Personas.Where(p => p.IdPersona == idPersona).FirstOrDefault();
            return View(vmcotizacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pagos(vmCotizacion vmcotizacion, int idPersona, int idPropiedad, string fechaInicial, int pagosEnganche = 1, int numPagosAnuales = 1, int pagosAnuales = 0)
        {

            if (string.IsNullOrEmpty(fechaInicial))
            {
                ModelState.AddModelError("", "Favor de verificar la fecha");
            }
            else{ 
            //vmCotizacion vmcotizacion = new vmCotizacion();
            vmcotizacion.propiedades = db.Propiedades.Where(p => p.IdPropiedad == idPropiedad).FirstOrDefault();
            vmcotizacion.personas = db.Personas.Where(p => p.IdPersona == idPersona).FirstOrDefault();
            vmcotizacion.cotizaciones.Persona = vmcotizacion.personas.IdPersona;
            vmcotizacion.cotizaciones.Propiedad = vmcotizacion.propiedades.IdPropiedad;
            
                try
                {
                    Cotizaciones cotizaciones = new Cotizaciones();
                    vmcotizacion.cotizaciones.Vendedor = User.Identity.GetUserId().ToString();
                    vmcotizacion.cotizaciones.FechaCotizacion = DateTime.Now;
                    vmcotizacion.cotizaciones.PrecioFinalVenta = vmcotizacion.propiedades.VentaPrecio.Value;

                    cotizaciones = vmcotizacion.cotizaciones;
                    db.Cotizaciones.Add(cotizaciones);
                    db.SaveChanges();


                    /*---- Amortizaciones ----*/
                    decimal enganche = vmcotizacion.cotizaciones.Enganche.Value;
                    decimal pagoMensual = vmcotizacion.cotizaciones.PagoMensual.Value;
                    int pagosMensuales = vmcotizacion.cotizaciones.Parcialidades.Value;

                    DateTime pago; //cambia para cada mes
                    DateTime.TryParse(fechaInicial, out pago);



                    //Enganches
                    for (int i = 0; i < pagosEnganche; i++)
                    {
                        Amortizaciones amortizacion = new Amortizaciones();
                        amortizacion.Cotizacion = cotizaciones.IdCotizacion;
                        amortizacion.Persona = cotizaciones.Persona;
                        amortizacion.Propiedad = cotizaciones.Propiedad;
                        amortizacion.Importe = enganche / pagosEnganche;
                        amortizacion.TipoPago = 1;
                        amortizacion.FechaProgramado = pago;

                        db.Amortizaciones.Add(amortizacion);
                        db.SaveChanges();
                        pago = pago.AddMonths(1);
                    }

                    //Parcialidades
                    for (int i = 0; i < pagosMensuales; i++)
                    {
                        Amortizaciones amortizacion = new Amortizaciones();
                        amortizacion.Cotizacion = cotizaciones.IdCotizacion;
                        amortizacion.Persona = cotizaciones.Persona;
                        amortizacion.Propiedad = cotizaciones.Propiedad;
                        amortizacion.Importe = pagoMensual;
                        amortizacion.TipoPago = 2;
                        amortizacion.FechaProgramado = pago;

                        db.Amortizaciones.Add(amortizacion);
                        db.SaveChanges();
                        //Anualidades
                        if (pago.Month == 12 && numPagosAnuales != 0)
                        {
                            Amortizaciones amortizacionAnual = new Amortizaciones();
                            amortizacionAnual.Cotizacion = cotizaciones.IdCotizacion;
                            amortizacionAnual.Persona = cotizaciones.Persona;
                            amortizacionAnual.Propiedad = cotizaciones.Propiedad;
                            amortizacionAnual.Importe = pagosAnuales;
                            amortizacionAnual.TipoPago = 3;
                            amortizacionAnual.FechaProgramado = pago;

                            db.Amortizaciones.Add(amortizacionAnual);
                            numPagosAnuales--;
                            db.SaveChanges();
                        }
                        pago = pago.AddMonths(1);
                    }

                    if (numPagosAnuales != 0)
                    {
                        for (int i = numPagosAnuales; i > 0; i--)
                        {
                            Amortizaciones amortizacionAnual = new Amortizaciones();
                            amortizacionAnual.Cotizacion = cotizaciones.IdCotizacion;
                            amortizacionAnual.Persona = cotizaciones.Persona;
                            amortizacionAnual.Propiedad = cotizaciones.Propiedad;
                            amortizacionAnual.Importe = pagosAnuales;
                            amortizacionAnual.TipoPago = 3;
                            amortizacionAnual.FechaProgramado = pago;
                            db.Amortizaciones.Add(amortizacionAnual);
                            db.SaveChanges();
                        }
                        //numPagosAnuales--;
                    }

                    /*--- Amortizaciones ----*/

                    //db.SaveChanges();

                    return RedirectToAction("Filtro", "Amortizaciones", new { cotizacion = cotizaciones.IdCotizacion });
                    // return Redirect("http://184.107.136.186/crminmobiliario/Reportes/RptCotizacion.aspx?IdCotizacion=" + cotizaciones.IdCotizacion);
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "No es posible guardar los cambios, intente mas tarde. Si los problemas persisten favor de contactarse con un adminsitrador");
                }
            }

            return View(vmcotizacion);
        }



        // GET: Cotizaciones/Create
        public ActionResult Create(int? idPropiedad, bool? filtro, string nombre, string categoria)
        {
            vmCotizacion vmcotizacion = new vmCotizacion();
            Cotizaciones cotizacion = new Cotizaciones();
            if (idPropiedad.HasValue)
            {
                var propiedad = db.Propiedades.Where(p => p.IdPropiedad == idPropiedad).FirstOrDefault();
                cotizacion.Propiedad = idPropiedad.Value;
                cotizacion.PrecioFinalVenta = propiedad.VentaPrecio;
                ViewBag.codigo = propiedad.Codigo;
                vmcotizacion.propiedades = propiedad;
            }

            if (!filtro.HasValue)
            {
                ViewBag.filtro = false;
            }

            ViewBag.filtro = filtro;
            vmcotizacion.cotizaciones = cotizacion;

            /*        Personas       */
            var nombreCompleto = new List<string>();
            var nombreQry = from d in db.Personas
                            orderby d.Paterno
                            select d.Nombre + " " + d.Paterno + " " + d.Materno;
            nombreCompleto.AddRange(nombreQry);
            ViewBag.nombre = new SelectList(nombreCompleto);


            var categorias = new List<string>();
            var categoriaQry = from d in db.PersonasCategoria
                               orderby d.Categoria
                               select d.Categoria.ToString();

            categorias.AddRange(categoriaQry.Distinct());
            ViewBag.categoria = new SelectList(categorias);

            var personas = from p in db.Personas
                           select p;

            if (!string.IsNullOrEmpty(categoria))
            {
                personas = personas.Where(s => s.PersonasCategoria.Categoria.ToString().Contains(categoria));
            }

            if (!string.IsNullOrEmpty(nombre))
            {
                personas = from p in db.Personas select p;
                personas = personas.Where(s => s.Nombre + " " + s.Paterno + " " + s.Materno == nombre);
            }

            ViewBag.personas = personas;
            /*      Personas           */

            //ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre");
            return View(vmcotizacion);
        }

        // POST: Cotizaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCotizacion,Propiedad,Persona,FechaCotizacion,PrecioFinalVenta,PorcentajeEnganche,Enganche,Parcialidades,PorcentajeMensualidades,PagoMensual,Vendedor")] Cotizaciones cotizaciones, int? idPropiedad)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    cotizaciones.Enganche = cotizaciones.PrecioFinalVenta.Value * (cotizaciones.PorcentajeEnganche.Value / 100);
                    cotizaciones.PagoMensual = (cotizaciones.PrecioFinalVenta.Value * (cotizaciones.PorcentajeMensualidades / 100)) / cotizaciones.Parcialidades.Value;

                    if (idPropiedad.HasValue)
                    {
                        cotizaciones.Propiedad = idPropiedad.Value;
                    }
                    cotizaciones.Vendedor = User.Identity.GetUserId().ToString();
                    cotizaciones.FechaCotizacion = DateTime.Now;
                    db.Cotizaciones.Add(cotizaciones);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No es posible guardar los cambios, intente mas tarde. Si los problemas persisten favor de contactarse con un adminsitrador");
            }

            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "NombreCompleto", cotizaciones.Persona);
            if (!string.IsNullOrEmpty(ViewBag.codigo))
            {

            }
            else
            {
                ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", cotizaciones.Propiedad);
            }
            return View(cotizaciones);
        }

        // GET: Cotizaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cotizaciones cotizaciones = db.Cotizaciones.Find(id);
            if (cotizaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "NombreCompleto", cotizaciones.Persona);
            ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", cotizaciones.Propiedad);
            return View(cotizaciones);
        }

        // POST: Cotizaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCotizacion,Propiedad,Persona,FechaCotizacion,PrecioFinalVenta,PorcentajeEnganche,Enganche,Parcialidades,PorcentajeMensualidades,PagoMensual")] Cotizaciones cotizaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cotizaciones).State = EntityState.Modified;
                cotizaciones.FechaCotizacion = DateTime.Now;
                cotizaciones.Vendedor = User.Identity.GetUserId().ToString();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "NombreCompleto", cotizaciones.Persona);
            ViewBag.Propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Titulo", cotizaciones.Propiedad);
            return View(cotizaciones);
        }

        // GET: Cotizaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cotizaciones cotizaciones = db.Cotizaciones.Find(id);
            if (cotizaciones == null)
            {
                return HttpNotFound();
            }
            return View(cotizaciones);
        }

        // POST: Cotizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cotizaciones cotizaciones = db.Cotizaciones.Find(id);
            db.Cotizaciones.Remove(cotizaciones);
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
