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
    public class CursandoController : Controller
    {
        private MatriculacionEntities db = new MatriculacionEntities();

        // GET: Cursando
        public ActionResult Index()
        {
            var EstudianteId = Convert.ToInt32(Session["EstudianteId"]);
            var cursandoes = db.Cursandoes.Include(c => c.Estudiante).Include(c => c.Materia).Where(a => a.EstudianteId == EstudianteId);
            return View(cursandoes.ToList());
        }

        // GET: Cursando/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cursando cursando = db.Cursandoes.Find(id);
            if (cursando == null)
            {
                return HttpNotFound();
            }
            return View(cursando);
        }
        public JsonResult MateriaDetalles(int? id)
        {
            if (id == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            var materia = db.Materias.Where(a => a.MateriaId == id).Select(a => new
            {
                MateriaId = a.MateriaId,
                CarreraId = a.CarreraId,
                Descripcion = a.Descripcion,
                Codigo = a.Codigo,
                Creditos = a.Creditos,
                Imparte = db.Impartes.Where(b => b.MateriaId == a.MateriaId).Select(b => new
                {
                    Profesor = db.Profesors.Where(c => c.ProfesorId == b.ProfesorId).Select(c => new
                    {
                        Nombre = c.Nombre,
                        Apellido = c.Apellido
                    })
                }).ToList()

            });
            if (materia == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            return Json(materia, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Eliminar(int? id)
        {
            Cursando cursando = db.Cursandoes.Find(id);
            db.Cursandoes.Remove(cursando);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMaterias()
        {
            var results = db.Materias.ToList().Select(a => new SelectListItem
            {
                Text = a.Descripcion.ToUpper(),
                Value = a.MateriaId.ToString()
            });
            return this.Json(results.OrderBy(a => a.Text), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Crear(int id)
        {
            if (ModelState.IsValid)
            {
                Cursando cursando = new Cursando();
                cursando.EstudianteId = Convert.ToInt32(Session["EstudianteId"]);
                cursando.MateriaId = id;

                db.Cursandoes.Add(cursando);
                db.SaveChanges();
                return Json("Ok", JsonRequestBehavior.AllowGet);

            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        // GET: Cursando/Create
        public ActionResult Create()
        {
            int carreraid = Convert.ToInt32(Session["CarreraId"]);
            ViewBag.MateriaId = new SelectList(db.Materias.Where(a => a.CarreraId == carreraid), "MateriaId", "Descripcion");
            return View();
        }

        // POST: Cursando/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CursandoId,EstudianteId,MateriaId")] Cursando cursando)
        {
            if (ModelState.IsValid)
            {
                cursando.EstudianteId = Convert.ToInt32(Session["EstudianteId"]);
                db.Cursandoes.Add(cursando);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstudianteId = new SelectList(db.Estudiantes, "EstudianteId", "Matricula", cursando.EstudianteId);
            ViewBag.MateriaId = new SelectList(db.Materias, "MateriaId", "Codigo", cursando.MateriaId);
            return View(cursando);
        }

        // GET: Cursando/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cursando cursando = db.Cursandoes.Find(id);
            if (cursando == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstudianteId = new SelectList(db.Estudiantes, "EstudianteId", "Matricula", cursando.EstudianteId);
            ViewBag.MateriaId = new SelectList(db.Materias, "MateriaId", "Codigo", cursando.MateriaId);
            return View(cursando);
        }

        // POST: Cursando/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CursandoId,EstudianteId,MateriaId")] Cursando cursando)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cursando).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstudianteId = new SelectList(db.Estudiantes, "EstudianteId", "Matricula", cursando.EstudianteId);
            ViewBag.MateriaId = new SelectList(db.Materias, "MateriaId", "Codigo", cursando.MateriaId);
            return View(cursando);
        }


        // GET: Cursando/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cursando cursando = db.Cursandoes.Find(id);
            if (cursando == null)
            {
                return HttpNotFound();
            }
            return View(cursando);
        }

        // POST: Cursando/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cursando cursando = db.Cursandoes.Find(id);
            db.Cursandoes.Remove(cursando);
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
