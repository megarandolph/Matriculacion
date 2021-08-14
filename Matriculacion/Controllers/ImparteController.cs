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
    public class ImparteController : Controller
    {
        private MatriculacionEntities db = new MatriculacionEntities();

        // GET: Imparte
        public ActionResult Index()
        {
            var impartes = db.Impartes.Include(i => i.Materia).Include(i => i.Profesor);
            return View(impartes.ToList());
        }

        // GET: Imparte/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imparte imparte = db.Impartes.Find(id);
            if (imparte == null)
            {
                return HttpNotFound();
            }
            return View(imparte);
        }

        // GET: Imparte/Create
        public ActionResult Create()
        {
            ViewBag.MateriaId = new SelectList(db.Materias, "MateriaId", "Codigo");
            ViewBag.ProfesorId = new SelectList(db.Profesors, "ProfesorId", "Nombre");
            return View();
        }

        // POST: Imparte/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImparteId,ProfesorId,MateriaId")] Imparte imparte)
        {
            if (ModelState.IsValid)
            {
                db.Impartes.Add(imparte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MateriaId = new SelectList(db.Materias, "MateriaId", "Codigo", imparte.MateriaId);
            ViewBag.ProfesorId = new SelectList(db.Profesors, "ProfesorId", "Nombre", imparte.ProfesorId);
            return View(imparte);
        }

        // GET: Imparte/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imparte imparte = db.Impartes.Find(id);
            if (imparte == null)
            {
                return HttpNotFound();
            }
            ViewBag.MateriaId = new SelectList(db.Materias, "MateriaId", "Codigo", imparte.MateriaId);
            ViewBag.ProfesorId = new SelectList(db.Profesors, "ProfesorId", "Nombre", imparte.ProfesorId);
            return View(imparte);
        }

        // POST: Imparte/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImparteId,ProfesorId,MateriaId")] Imparte imparte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imparte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MateriaId = new SelectList(db.Materias, "MateriaId", "Codigo", imparte.MateriaId);
            ViewBag.ProfesorId = new SelectList(db.Profesors, "ProfesorId", "Nombre", imparte.ProfesorId);
            return View(imparte);
        }

        // GET: Imparte/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imparte imparte = db.Impartes.Find(id);
            if (imparte == null)
            {
                return HttpNotFound();
            }
            return View(imparte);
        }

        // POST: Imparte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Imparte imparte = db.Impartes.Find(id);
            db.Impartes.Remove(imparte);
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
