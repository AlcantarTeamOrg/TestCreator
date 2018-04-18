using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestCreatorWebSite.Data;

namespace TestCreatorWebSite.Controllers
{
    public class TestiesController : Controller
    {
        private TestCreatorEntities db = new TestCreatorEntities();

        // GET: Testies
        public ActionResult Index()
        {
            var testy = db.Testy.Include(t => t.Stanowiska);
            return View(testy.ToList());
        }

        // GET: Testies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testy testy = db.Testy.Find(id);
            if (testy == null)
            {
                return HttpNotFound();
            }
            return View(testy);
        }

        // GET: Testies/Create
        public ActionResult Create()
        {
            ViewBag.id_stanowisko = new SelectList(db.Stanowiska, "id_stanowisko", "nazwa_stanowiska");
            return View();
        }

        // POST: Testies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_test,nazwa,data_stworzenia,is_visible,id_stanowisko")] Testy testy)
        {
            if (ModelState.IsValid)
            {
                testy.data_stworzenia = DateTime.Now;
                testy.is_visible = false;
                db.Testy.Add(testy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_stanowisko = new SelectList(db.Stanowiska, "id_stanowisko", "nazwa_stanowiska", testy.id_stanowisko);
            return View(testy);
        }

        // GET: Testies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testy testy = db.Testy.Find(id);
            if (testy == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_stanowisko = new SelectList(db.Stanowiska, "id_stanowisko", "nazwa_stanowiska", testy.id_stanowisko);
            return View(testy);
        }

        // POST: Testies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_test,nazwa,data_stworzenia,is_visible,id_stanowisko")] Testy testy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_stanowisko = new SelectList(db.Stanowiska, "id_stanowisko", "nazwa_stanowiska", testy.id_stanowisko);
            return View(testy);
        }

        // GET: Testies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testy testy = db.Testy.Find(id);
            if (testy == null)
            {
                return HttpNotFound();
            }
            return View(testy);
        }

        // POST: Testies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Testy testy = db.Testy.Find(id);
            db.Testy.Remove(testy);
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
