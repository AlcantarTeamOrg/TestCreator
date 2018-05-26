using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestCreatorWebSite.Controllers
{
    public class ModeratorController : Controller
    {
        // GET: Moderator
        public ActionResult Index()
        {
            return View();
        }

        // GET: TestUsers/Details/5
        public ActionResult Details(long? id)
        {

            return View();
        }

        public ActionResult Delete(long? id)
        {

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}