using crmInmobiliario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crmInmobiliario.Controllers
{
    public class HomeController : Controller
    {
        private CRMINMOBILIARIOEntities3 db = new CRMINMOBILIARIOEntities3();

        public AspNetUsers getUser()
        {
            var usuario = db.AspNetUsers.Where(a => a.UserName == this.User.Identity.Name).FirstOrDefault();
            return usuario;
        }

        public ActionResult Index()
        {
            var usuario = getUser();
            if (usuario!=null)
            {
                ViewBag.rol = usuario.UserRoles;
            }else
            {
                ViewBag.rol = "SIN-ROL";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /*--------------------------------------------------*/
        public ActionResult Pruebas()
        {
            return View();
        }
        /*-------------------------------------------------*/
    }
}