using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsystentTrenera2.Models;

namespace AsystentTrenera2.Controllers
{
    public class HomeController : Controller
    {

        AsystentTrenera2Context context = new AsystentTrenera2Context();
        public ActionResult Index()
        {

            //var lista = context.Aktualnosc.ToList();
            ViewBag.Message = "Witamy na stronie amatroskiej drużyny piłkaskiej";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
