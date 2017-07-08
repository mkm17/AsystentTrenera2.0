using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using AsystentTrenera2.Models;

namespace AsystentTrenera2.Controllers.LekarzMain
{
    public class LekarzMainController : Controller
    {
        //
        // GET: /LekarzMain/
        AsystentTrenera2Context context = new AsystentTrenera2Context();

        public ActionResult Index()
        {
            var Zawodnicy = context.Zawodnik.Include(zawodnik => zawodnik.Kontuzjas).Where(zawodnik => zawodnik.Kontuzjas.Any(kontuzja => kontuzja.Aktualna == true));
            return View(Zawodnicy);
        }

    }
}
