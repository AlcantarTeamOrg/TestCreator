using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestCreatorWebSite.Data;

namespace TestCreatorWebSite.Controllers
{
    public class OdpowiedzsController : Controller
    {
        private TestCreatorEntities db = new TestCreatorEntities();

        // GET: Odpowiedzs
        public ActionResult Index()
        {
            var odpowiedz = db.Odpowiedz.Include(o => o.Pytania);
            return View(odpowiedz.ToList());
        }

        // GET: Odpowiedzs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Odpowiedz odpowiedz = db.Odpowiedz.Find(id);
            if (odpowiedz == null)
            {
                return HttpNotFound();
            }
            return View(odpowiedz);
        }

        // GET: Odpowiedzs/Create
        public ActionResult Create()
        {
            ViewBag.id_pytanie = new SelectList(db.Pytania, "id_pytanie", "tresc_pytania");
            return View();
        }

        // POST: Odpowiedzs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_odpowiedz,id_pytanie,tresc_odpowiedzi,is_visible,dobra")] Odpowiedz odpowiedz)
        {
            if (ModelState.IsValid)
            {
                int index = 1;
                Odpowiedz o = db.Odpowiedz.FirstOrDefault();
                if (o != null)
                {
                    index = db.Odpowiedz.Max(x => x.id_odpowiedz) + 1;
                }
                odpowiedz.id_odpowiedz = index;
                odpowiedz.is_visible = true;
                db.Odpowiedz.Add(odpowiedz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_pytanie = new SelectList(db.Pytania, "id_pytanie", "tresc_pytania", odpowiedz.id_pytanie);
            return View(odpowiedz);
        }

        // GET: Odpowiedzs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Odpowiedz odpowiedz = db.Odpowiedz.Find(id);
            if (odpowiedz == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_pytanie = new SelectList(db.Pytania, "id_pytanie", "tresc_pytania", odpowiedz.id_pytanie);
            return View(odpowiedz);
        }

        // POST: Odpowiedzs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_odpowiedz,id_pytanie,tresc_odpowiedzi,is_visible,dobra")] Odpowiedz odpowiedz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(odpowiedz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_pytanie = new SelectList(db.Pytania, "id_pytanie", "tresc_pytania", odpowiedz.id_pytanie);
            return View(odpowiedz);
        }

        // GET: Odpowiedzs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Odpowiedz odpowiedz = db.Odpowiedz.Find(id);
            if (odpowiedz == null)
            {
                return HttpNotFound();
            }
            return View(odpowiedz);
        }

        // POST: Odpowiedzs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Odpowiedz odpowiedz = db.Odpowiedz.Find(id);
            db.Odpowiedz.Remove(odpowiedz);
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