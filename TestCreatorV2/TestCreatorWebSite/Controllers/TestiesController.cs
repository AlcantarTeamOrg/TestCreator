using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TestCreatorWebSite.Data;
using TestCreatorWebSite.Models;

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

        public ActionResult DetailsAdd(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id1 = (int)id;
            Testy testy = db.Testy
                                        .Where(u => u.id_test == id1
                                                && u.is_visible == true)
                                        .SingleOrDefault();
            if (testy == null)
            {
                return HttpNotFound();
            }
            return View(testy);
        }

        public ActionResult DetailsUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id1 = (int)id;
            Testy testy = db.Testy
                                        .Where(u => u.id_test == id1
                                                && u.is_visible == true)
                                        .SingleOrDefault();
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

        // GET: Testies/Delete/5
        [HttpGet]
        public string GetDetails(int? id)
        {
            if (id == null)
            {
                return null;
            }
            int id1 = (int)id;
            List<Pytania> pytania = db.Pytania
                                        .Where(u => u.id_test == id1
                                                && u.is_visible == true)
                                        .ToList();
            if (pytania == null)
            {
                return null;
            }
            string s = Json(pytania).ToString();
            List<PytaniaCustom> p = new List<PytaniaCustom>();
            foreach (var v in pytania)
            {
                PytaniaCustom pc = new PytaniaCustom();
                pc.id_pytanie = v.id_pytanie;
                pc.id_test = v.id_test;
                pc.is_visible = v.is_visible;
                pc.otwarte = v.otwarte;
                pc.tresc_pytania = v.tresc_pytania;
                foreach (var v1 in v.Odpowiedz)
                {
                    Models.Odpowiedz odp = new Models.Odpowiedz();
                    odp.dobra = v1.dobra;
                    odp.id_odpowiedz = v1.id_odpowiedz;
                    odp.id_pytanie = v1.id_pytanie;
                    odp.is_visible = v1.is_visible;
                    odp.tresc_odpowiedzi = v1.tresc_odpowiedzi;

                    if (v1.is_visible == true)
                    {
                        if (pc.Odpowiedz == null)
                        {
                            pc.Odpowiedz = new List<Models.Odpowiedz>();
                        }
                        pc.Odpowiedz.Add(odp);
                    }

                }
                if (pc.Odpowiedz.Count == 1)
                {
                    pc.type = 1; //1- otwarte
                }
                else if (pc.Odpowiedz.Where(u => u.dobra == true).Count() > 1)
                {
                    pc.type = 3; // 3- wielokrotnego wyboru
                }
                else
                {
                    pc.type = 2; //2-jednokrotnego wyboru
                }
                p.Add(pc);
            }

            return new JavaScriptSerializer().Serialize(p);  //Json(new { foo = "bar", baz = "Blech" });//pytania, JsonRequestBehavior.AllowGet);
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
