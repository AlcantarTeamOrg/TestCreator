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
    public class PytaniesController : Controller
    {
        private TestCreatorEntities db = new TestCreatorEntities();

        // GET: Pytanies
        public ActionResult Index()
        {
            return View(db.Pytanie.ToList());
        }

        // GET: Pytanies/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pytanie pytanie = db.Pytanie.Find(id);
            if (pytanie == null)
            {
                return HttpNotFound();
            }
            return View(pytanie);
        }

        // GET: Pytanies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pytanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pytanie,pytanie1,odpowiedz,typ")] Pytanie pytanie)
        {
            if (ModelState.IsValid)
            {
                db.Pytanie.Add(pytanie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pytanie);
        }

        // GET: Pytanies/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pytanie pytanie = db.Pytanie.Find(id);
            if (pytanie == null)
            {
                return HttpNotFound();
            }
            return View(pytanie);
        }

        // POST: Pytanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pytanie,pytanie1,odpowiedz,typ")] Pytanie pytanie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pytanie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pytanie);
        }

        // GET: Pytanies/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pytanie pytanie = db.Pytanie.Find(id);
            if (pytanie == null)
            {
                return HttpNotFound();
            }
            return View(pytanie);
        }

        // POST: Pytanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Pytanie pytanie = db.Pytanie.Find(id);
            db.Pytanie.Remove(pytanie);
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
