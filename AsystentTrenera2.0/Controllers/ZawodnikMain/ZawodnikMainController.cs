using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AsystentTrenera2.Models;
using System.Data;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;
using OpenPop.Pop3;

namespace AsystentTrenera2.Controllers.ZawodnikMain
{
    [Authorize(Roles = "Zawodnik")]
    public class ZawodnikMainController : Controller
    {
        //
        // GET: /ZawodnikMain/

        private AsystentTrenera2Context context = new AsystentTrenera2Context();


        public ActionResult Index()
        {
            var zawodnik = context.Zawodnik.Include(x => x.Ocenas).Include(x => x.OcenaOgolna).FirstOrDefault(x => x.UserName == User.Identity.Name);
            SelectList lista = new SelectList(context.Zawodnik,"ZawodnikId","Nazwisko");
            ViewBag.Zawodnicy = lista;
            return View(zawodnik);
        }
    }
}
