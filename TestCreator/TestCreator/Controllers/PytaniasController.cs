using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestCreator.Data;

namespace TestCreator.Controllers
{
    public class PytaniasController : Controller
    {
        private TestCreatorEntities db = new TestCreatorEntities();

        // GET: Pytanias
        public ActionResult Index()
        {
            var pytania = db.Pytania.Include(p => p.Testy);
            return View(pytania.ToList());
        }

        // GET: Pytanias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pytania pytania = db.Pytania.Find(id);
            if (pytania == null)
            {
                return HttpNotFound();
            }
            return View(pytania);
        }

        // GET: Pytanias/Create
        public ActionResult Create()
        {
            ViewBag.id_test = new SelectList(db.Testy, "id_test", "nazwa");
            return View();
        }

        // POST: Pytanias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pytanie,tresc_pytania,is_visible,otwarte,id_test")] Pytania pytania)
        {
            if (ModelState.IsValid)
            {
                db.Pytania.Add(pytania);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_test = new SelectList(db.Testy, "id_test", "nazwa", pytania.id_test);
            return View(pytania);
        }

        // GET: Pytanias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pytania pytania = db.Pytania.Find(id);
            if (pytania == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_test = new SelectList(db.Testy, "id_test", "nazwa", pytania.id_test);
            return View(pytania);
        }

        // POST: Pytanias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pytanie,tresc_pytania,is_visible,otwarte,id_test")] Pytania pytania)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pytania).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_test = new SelectList(db.Testy, "id_test", "nazwa", pytania.id_test);
            return View(pytania);
        }

        // GET: Pytanias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pytania pytania = db.Pytania.Find(id);
            if (pytania == null)
            {
                return HttpNotFound();
            }
            return View(pytania);
        }

        // POST: Pytanias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pytania pytania = db.Pytania.Find(id);
            db.Pytania.Remove(pytania);
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
