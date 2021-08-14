using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Matriculacion.Models;

namespace Matriculacion.Controllers
{
    public class EstudianteController : Controller
    {
        private MatriculacionEntities db = new MatriculacionEntities();

        // GET: Estudiante
        public ActionResult Index()
        {
            var estudiantes = db.Estudiantes.Include(e => e.Carrera).Include(e => e.Cuatrimestre);
            return View(estudiantes.ToList());
        }

        // GET: Estudiante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // GET: Estudiante/Create
        public ActionResult Create()
        {
            ViewBag.CarreraId = new SelectList(db.Carreras, "CarreraId", "Descripcion");
            ViewBag.CuatrimestreId = new SelectList(db.Cuatrimestres, "CuatrimestreId", "Descripcion");
            return View();
        }

        // POST: Estudiante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstudianteId,CuatrimestreId,CarreraId,Matricula,Nombre,Apellido,Cedula,Correo,Telefono,Direccion,Contrasena,ContrasenaC")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                db.Estudiantes.Add(estudiante);
                db.SaveChanges();
                return RedirectToAction("Login","Home");
            }

            ViewBag.CarreraId = new SelectList(db.Carreras, "CarreraId", "Descripcion", estudiante.CarreraId);
            ViewBag.CuatrimestreId = new SelectList(db.Cuatrimestres, "CuatrimestreId", "Descripcion", estudiante.CuatrimestreId);
            return View(estudiante);
        }

        // GET: Estudiante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarreraId = new SelectList(db.Carreras, "CarreraId", "Descripcion", estudiante.CarreraId);
            ViewBag.CuatrimestreId = new SelectList(db.Cuatrimestres, "CuatrimestreId", "Descripcion", estudiante.CuatrimestreId);
            return View(estudiante);
        }

        // POST: Estudiante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstudianteId,CuatrimestreId,CarreraId,Matricula,Nombre,Apellido,Cedula,Correo,Telefono,Direccion,Contrasena,ContrasenaC")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudiante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarreraId = new SelectList(db.Carreras, "CarreraId", "Descripcion", estudiante.CarreraId);
            ViewBag.CuatrimestreId = new SelectList(db.Cuatrimestres, "CuatrimestreId", "Descripcion", estudiante.CuatrimestreId);
            return View(estudiante);
        }

        // GET: Estudiante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estudiante estudiante = db.Estudiantes.Find(id);
            db.Estudiantes.Remove(estudiante);
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
