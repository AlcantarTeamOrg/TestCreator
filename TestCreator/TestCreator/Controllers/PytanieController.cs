using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestCreator.Data;

namespace TestCreator.Controllers
{
    public class PytanieController : Controller
    {
        // GET: Pytanie/Index
        public ActionResult Index()
        {
            using (TestCreatorEntities dbModel = new TestCreatorEntities())
            {
                return View(dbModel.Pytanie.ToList());
            }

        }

        // GET: Pytanie/Details/5
        public ActionResult Details(Pytanie pytanie)
        {
            using (TestCreatorEntities dbModel = new TestCreatorEntities())
            {
                return View(dbModel.Pytanie.ToList());
            }
        }

        // GET: Pytanie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pytanie/Create
        [HttpPost]
        public ActionResult Create(Pytanie pytanie)
        {
            try
            {
                // TODO: Add insert logic here
                using (TestCreatorEntities dbModel = new TestCreatorEntities())
                {
                    dbModel.Pytanie.Add(pytanie);
                    dbModel.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pytanie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pytanie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pytanie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pytanie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
