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
    public class StanowiskasController : Controller
    {
        private TestCreatorEntities db = new TestCreatorEntities();

        // GET: Stanowiskas
        public ActionResult Index()
        {
            return View(db.Stanowiska.ToList());
        }

        // GET: Stanowiskas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stanowiska stanowiska = db.Stanowiska.Find(id);
            if (stanowiska == null)
            {
                return HttpNotFound();
            }
            return View(stanowiska);
        }

        // GET: Stanowiskas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stanowiskas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_stanowisko,nazwa_stanowiska,is_visible")] Stanowiska stanowiska)
        {
            if (ModelState.IsValid)
            {
                db.Stanowiska.Add(stanowiska);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stanowiska);
        }

        // GET: Stanowiskas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stanowiska stanowiska = db.Stanowiska.Find(id);
            if (stanowiska == null)
            {
                return HttpNotFound();
            }
            return View(stanowiska);
        }

        // POST: Stanowiskas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_stanowisko,nazwa_stanowiska,is_visible")] Stanowiska stanowiska)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stanowiska).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stanowiska);
        }

        // GET: Stanowiskas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stanowiska stanowiska = db.Stanowiska.Find(id);
            if (stanowiska == null)
            {
                return HttpNotFound();
            }
            return View(stanowiska);
        }

        // POST: Stanowiskas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stanowiska stanowiska = db.Stanowiska.Find(id);
            db.Stanowiska.Remove(stanowiska);
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
