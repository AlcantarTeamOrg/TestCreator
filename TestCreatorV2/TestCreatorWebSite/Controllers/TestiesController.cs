﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TestCreatorWebSite.Data;
using TestCreatorWebSite.Helpers;
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

        // GET: Testies/Details/5
        public ActionResult DetailsFull(int? id)
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

        public ActionResult DetailsUser(int? id)
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

        public ActionResult DetailsResolveTest(int? id)
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

        public ActionResult CreateFull()
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
                HttpResponseMessage httpResponseMessage2 = GlobalVariables.WebApiClient.GetAsync("UserLogin").Result;
                var user = httpResponseMessage2.Content.ReadAsAsync<Uzytkownik>().Result;
                testy.id_autor = 1;
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
                pc.tresc_pytania = v.tresc_pytania;
                pc.typ = v.typ;
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
                p.Add(pc);
            }

            return new JavaScriptSerializer().Serialize(p);  //Json(new { foo = "bar", baz = "Blech" });//pytania, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string GetDetailsResolve(int? idTest, int? idUser)
        {
            var userID = Session["userID"];
            if (idTest == null)
            {
                return null;
            }
            if (idUser == null)
            {
                idUser = 1;
            }
            int id1 = (int)idTest;
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
                pc.tresc_pytania = v.tresc_pytania;
                pc.typ = v.typ;
                foreach (var v1 in v.Odpowiedz)
                {
                    Models.Odpowiedz odp = new Models.Odpowiedz();
                    odp.dobra = false;// v1.dobra;
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
                var odpowiedzUser = db.UzytkownikOdpowiedz
                                       .Where(u => u.id_testy == v.id_pytanie
                                               && u.id_uzytkownik == idUser).OrderByDescending(u => u.id)
                                       .FirstOrDefault();
                if (v.typ == 4 || v.typ == 3)
                {
                    pc.Odpowiedz[0].tresc_odpowiedzi = odpowiedzUser.odpowiedz;
                }

                else if (v.typ == 2 || v.typ == 1)
                {
                    var trueList = odpowiedzUser.odpowiedz.Split(';');
                    foreach (var trueO in trueList)
                    {
                        pc.Odpowiedz[int.Parse(odpowiedzUser.odpowiedz)-1].dobra = true;
                    }
                    
                }
                p.Add(pc);
            }

            return new JavaScriptSerializer().Serialize(p);  //Json(new { foo = "bar", baz = "Blech" });//pytania, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]

        public bool SaveAnsver(List<UzytkownikOdpowiedz> list)
        {
            try
            {
                Session["userID"] = 5;
                foreach (var i in list)
                {
                    UzytkownikOdpowiedz odp = new UzytkownikOdpowiedz();
                    int id = db.UzytkownikOdpowiedz.Max(u => u.id);
                    odp.id = id + 1;
                    odp.id_testy = i.id_testy;// idTest;
                    odp.id_uzytkownik = 1;
                    odp.odpowiedz = i.odpowiedz;// html;
                    db.UzytkownikOdpowiedz.Add(odp);
                    db.SaveChanges();
                }
                
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPost]
        public bool SaveTest(TestSave test)
        {
            try
            {
                Testy testy = new Testy();
                testy.data_stworzenia = DateTime.Now;
                testy.is_visible = true;
                testy.id_autor = 1; //zamienic na zalogowanego
                testy.id_stanowisko = test.id_stanowisko;
                testy.nazwa = test.Name;

                db.Testy.Add(testy);
                db.SaveChanges();

                foreach (var pyt in test.listPytania)
                {
                    Pytania pytania = new Pytania();
                    pytania.is_visible = true;
                    pytania.id_test = testy.id_test;
                    pytania.tresc_pytania = pyt.tresc_pytania;
                    pytania.typ = pyt.typ;
                    db.Pytania.Add(pytania);
                    db.SaveChanges();

                    foreach (var odp in pyt.Odpowiedz)
                    {
                        Data.Odpowiedz odpowiedz = new Data.Odpowiedz();
                        odpowiedz.is_visible = true;
                        odpowiedz.id_pytanie = pytania.id_pytanie;
                        odpowiedz.tresc_odpowiedzi = odp.tresc_odpowiedzi;
                        odpowiedz.dobra = odp.dobra;
                        db.Odpowiedz.Add(odpowiedz);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
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
