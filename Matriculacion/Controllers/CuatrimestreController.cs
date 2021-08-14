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
    public class CuatrimestreController : Controller
    {
        private MatriculacionEntities db = new MatriculacionEntities();

        // GET: Cuatrimestre
        public ActionResult Index()
        {
            return View(db.Cuatrimestres.ToList());
        }

        // GET: Cuatrimestre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuatrimestre cuatrimestre = db.Cuatrimestres.Find(id);
            if (cuatrimestre == null)
            {
                return HttpNotFound();
            }
            return View(cuatrimestre);
        }

        // GET: Cuatrimestre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cuatrimestre/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CuatrimestreId,Descripcion,FechaInicio,FechaFin")] Cuatrimestre cuatrimestre)
        {
            if (ModelState.IsValid)
            {
                db.Cuatrimestres.Add(cuatrimestre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cuatrimestre);
        }

        // GET: Cuatrimestre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuatrimestre cuatrimestre = db.Cuatrimestres.Find(id);
            if (cuatrimestre == null)
            {
                return HttpNotFound();
            }
            return View(cuatrimestre);
        }

        // POST: Cuatrimestre/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CuatrimestreId,Descripcion,FechaInicio,FechaFin")] Cuatrimestre cuatrimestre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuatrimestre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cuatrimestre);
        }

        // GET: Cuatrimestre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuatrimestre cuatrimestre = db.Cuatrimestres.Find(id);
            if (cuatrimestre == null)
            {
                return HttpNotFound();
            }
            return View(cuatrimestre);
        }

        // POST: Cuatrimestre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuatrimestre cuatrimestre = db.Cuatrimestres.Find(id);
            db.Cuatrimestres.Remove(cuatrimestre);
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
