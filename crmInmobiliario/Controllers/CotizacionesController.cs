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

        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }

        // GET: Cotizaciones
        public ActionResult Index(int? idPersona, int? persona, int? propiedad, string idVendedor = "", string fecha = "")
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "VENTAS" || usuario.UserRoles == "TESORERIA" || usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
            {
                var cotizaciones = db.Cotizaciones.Include(c => c.Personas).Include(c => c.Propiedades);

                if (usuario.UserRoles == "VENTAS") //para que los vendedores solo vean las cotizaciones registradas por ellos
                {
                    cotizaciones = cotizaciones.Where(c => c.Vendedor == usuario.Id);
                }

                if (idPersona.HasValue)
                {
                    cotizaciones = cotizaciones.Where(c => c.Persona == idPersona.Value);
                }

                foreach (var cotizacion in cotizaciones)
                {
                    var vendedor = new AspNetUsers();
                    vendedor = db.AspNetUsers.Where(u => u.Id == cotizacion.Vendedor).FirstOrDefault();
                    if (vendedor != null)
                    {
                        cotizacion.Vendedor = vendedor.UserName;
                    }else
                    {
                        cotizacion.Vendedor = "vendedor eliminado";
                    }
                    
                }

                /*------------Espacio para filtros--------------*/

                if (persona!=null) //por nombre de persona
                {
                    cotizaciones = cotizaciones.Where(c => c.Persona == persona);
                }

                if (propiedad!=null)//por codigo de propiedad
                {
                    cotizaciones = cotizaciones.Where(c => c.Propiedad == propiedad);
                }

                if (idVendedor.Length > 1)
                {
                    cotizaciones = cotizaciones.Where(c => c.Vendedor == idVendedor);
                }

                if (fecha.Length > 0)
                {
                    DateTime fechaCotizacion;
                    DateTime.TryParse(fecha, out fechaCotizacion);
                    cotizaciones = cotizaciones.Where(c => 
                        c.FechaCotizacion.Value.Year == fechaCotizacion.Year &&
                        c.FechaCotizacion.Value.Month == fechaCotizacion.Month &&
                        c.FechaCotizacion.Value.Day == fechaCotizacion.Day);
                }

                /*------------Espacio para filtros--------------*/

                if (usuario.UserRoles == "VENTAS")
                {

                    ViewBag.persona = new SelectList(db.Personas.Where(p => p.Usuario == usuario.Id), "IdPersona", "NombreCompleto");

                    var cotizacionesFiltro = db.Cotizaciones.AsNoTracking().Where(c => c.Vendedor == usuario.Id);
                    List<Propiedades> propiedades = new List<Propiedades>();
                    foreach (var cotizacion in cotizacionesFiltro)
                    {
                        var propiedadCot = db.Propiedades.Where(p => p.IdPropiedad == cotizacion.Propiedad).FirstOrDefault();
                        if (!propiedades.Contains(propiedadCot))
                        {
                            propiedades.Add(propiedadCot);
                        }
                    }

                    ViewBag.propiedad = new SelectList(propiedades, "IdPropiedad", "Codigo");
                    //ViewBag.idVendedor = new SelectList(db.AspNetUsers, "Id", "UserName");
                }
                else
                {
                    ViewBag.persona = new SelectList(db.Personas, "IdPersona", "NombreCompleto");
                    ViewBag.propiedad = new SelectList(db.Propiedades, "IdPropiedad", "Codigo");
                    ViewBag.idVendedor = new SelectList(db.AspNetUsers, "Id", "UserName");
                }

                return View(cotizaciones.OrderByDescending(c => c.IdCotizacion).ToList());
            }
            else
            {
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        // GET: Cotizaciones/Details/5
        public ActionResult Details(int? id)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "VENTAS" || usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL" || usuario.UserRoles == "COORDINADOR-DIVISION-SOFT" || usuario.UserRoles == "CONTRALOR")
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
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        public void Excel()
        {
            var model = db.Cotizaciones.ToList();

            Export export = new Export();
            export.ToExcel(Response, model, "Cotizaciones");
        }

        public ActionResult Pagos(int idPersona, int idPropiedad)
        {
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "VENTAS" || usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL")
            {

                if (usuario.UserRoles == "VENTAS")
                {
                    ViewBag.rolUsuario = "VENTAS";
                }

                vmCotizacion vmcotizacion = new vmCotizacion();
                vmcotizacion.propiedades = db.Propiedades.Where(p => p.IdPropiedad == idPropiedad).FirstOrDefault();
                vmcotizacion.personas = db.Personas.Where(p => p.IdPersona == idPersona).FirstOrDefault();

                var desarrollo = db.Desarrollos.Where(d => d.IdDesarrollo == vmcotizacion.propiedades.Desarrollo).FirstOrDefault();
                ViewBag.descuento = decimal.ToInt32(desarrollo.Descuento.Value);
                return View(vmcotizacion);
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pagos(vmCotizacion vmcotizacion, int idPersona, int idPropiedad, string fechaInicial, int pagosEnganche = 1, int numPagosAnuales = 1, decimal pagosAnuales = 0, int descuento = 0)
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
                    decimal precioFinalDescto = vmcotizacion.propiedades.VentaPrecio.Value - vmcotizacion.propiedades.VentaPrecio.Value*(((decimal)descuento) / 100);
                    vmcotizacion.cotizaciones.PrecioFinalVenta = precioFinalDescto;

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
                        amortizacion.Tipo = "C";

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
                        amortizacion.Tipo = "C";

                        db.Amortizaciones.Add(amortizacion);
                        db.SaveChanges();
                        //Anualidades
                        pago = pago.AddMonths(1);
                        if (pago.Month == 12 && numPagosAnuales != 0)
                        {
                            Amortizaciones amortizacionAnual = new Amortizaciones();
                            amortizacionAnual.Cotizacion = cotizaciones.IdCotizacion;
                            amortizacionAnual.Persona = cotizaciones.Persona;
                            amortizacionAnual.Propiedad = cotizaciones.Propiedad;
                            amortizacionAnual.Importe = pagosAnuales;
                            amortizacionAnual.TipoPago = 3;
                            amortizacionAnual.FechaProgramado = pago;
                            amortizacionAnual.Tipo = "C";

                            db.Amortizaciones.Add(amortizacionAnual);
                            numPagosAnuales--;
                            db.SaveChanges();
                        }
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
                            amortizacionAnual.Tipo = "C";
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
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "VENTAS" || usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL")
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
                if (usuario.UserRoles == "VENTAS") //para que los vendedores solo vean las cotizaciones registradas por ellos
                {
                    personas = personas.Where(p => p.Usuario == usuario.Id);
                }

                ViewBag.personas = personas;
                /*      Personas           */

                //ViewBag.Persona = new SelectList(db.Personas, "IdPersona", "Nombre");
                return View(vmcotizacion);
            }
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
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
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "VENTAS" || usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL")
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
            }else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
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
            var usuario = getUser();
            ViewBag.rol = usuario.UserRoles;
            if (usuario.UserRoles == "VENTAS" || usuario.UserRoles == "GERENTE-VENTAS" || usuario.UserRoles == "DIR-GENERAL")
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
            else
            {
                return RedirectToAction("PermisoDenegado", "Account");
            }
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
