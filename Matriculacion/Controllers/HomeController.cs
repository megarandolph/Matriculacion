using Matriculacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Matriculacion.Controllers
{
    public class HomeController : Controller
    {
        private MatriculacionEntities db = new MatriculacionEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            Session["CuatrimestreId"] = null;
            Session["EstudianteId"] = null;
            Session["CarreraId"] = null;
            Session["Nombre"] = null;
            Session["Admin"] = null;
            Session["Login"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            var estudiante = db.Estudiantes.Where(a => a.Correo == Email && a.Contrasena == Password).SingleOrDefault();
            var admin = db.Usuarios.Where(a => a.Correo == Email && a.Contrasena == Password).SingleOrDefault();

            if (estudiante != null) {
                Session["CuatrimestreId"] = estudiante.CuatrimestreId;
                Session["EstudianteId"] = estudiante.EstudianteId;
                Session["CarreraId"] = estudiante.CarreraId;
                Session["Nombre"] = estudiante.Nombre +' '+ estudiante.Apellido;
                Session["Admin"] = null;
                Session["Login"] = true;

                return RedirectToAction("Index", "Home");

            } else if (admin != null) {
                Session["Nombre"] = admin.Nombre +' '+admin.Apellido;
                Session["Admin"] = true;
                Session["Login"] = true;

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public ActionResult Register()
        {
            ViewBag.CarreraId = new SelectList(db.Carreras, "CarreraId", "Descripcion");
            ViewBag.CuatrimestreId = new SelectList(db.Cuatrimestres, "CuatrimestreId", "Descripcion");
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
    }
}