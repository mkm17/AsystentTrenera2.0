using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsystentTrenera2.Models;

namespace AsystentTrenera2.Controllers
{
    public class MeczController : Controller
    {
        private AsystentTrenera2Context context = new AsystentTrenera2Context();

        //
        // GET: /Mecz/

        public ViewResult Index()
        {
            
            return View(context.Mecz.ToList());
        }

        //
        // GET: /Mecz/Details/5

        public ViewResult Details(int id)
        {
            Mecz mecz = context.Mecz.Single(x => x.MeczId == id);
            return View(mecz);
        }

        //
        // GET: /Mecz/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Mecz/Create

        [HttpPost]
        public ActionResult Create(Mecz mecz)
        {
            if (ModelState.IsValid)
            {
                context.Mecz.Add(mecz);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mecz);
        }

        //
        // GET: /Mecz/Edit/5

        public ActionResult Edit(int id)
        {
            Mecz mecz = context.Mecz.Single(x => x.MeczId == id);
            return View(mecz);
        }

        //
        // POST: /Mecz/Edit/5

        [HttpPost]
        public ActionResult Edit(Mecz mecz)
        {
            if (ModelState.IsValid)
            {
                context.Entry(mecz).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mecz);
        }

        //
        // GET: /Mecz/Delete/5

        public ActionResult Delete(int id)
        {
            Mecz mecz = context.Mecz.Single(x => x.MeczId == id);
            return View(mecz);
        }

        //
        // POST: /Mecz/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Mecz mecz = context.Mecz.Single(x => x.MeczId == id);
            context.Mecz.Remove(mecz);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult StatystykiMeczu(int id)
        {
            Mecz mecz = context.Mecz.Single(x => x.MeczId == id);
            var ListaOcen = context.Ocena.Include(x => x.Zawodnik).Include(x => x.Pozycja).Include(x=>x.OcenaInnychs).Where(x => x.MeczId == id).ToList();
                //;
            ViewBag.Mecz = mecz;
            return View(ListaOcen);
        }
        public ActionResult OcenaZawodnika(int idZawodnika, int idMeczu)
        {
            var Ocena = context.Ocena.Include(x => x.Zawodnik).Include(x => x.Pozycja).Include(x=>x.OcenaInnychs).Single(x => x.MeczId == idMeczu && x.ZawodnikId == idZawodnika);
            var Zawodnik = context.Zawodnik.SingleOrDefault(x => x.UserName == User.Identity.Name);
            foreach (var item in Ocena.OcenaInnychs)
            {
                if (item.User == User.Identity.Name)
                {
                    return RedirectToAction("Index", "Temu zawodnikowi ju¿ doda³eœ ocene");
                }
            }
            return View(Ocena);
        }
        [HttpPost]
        public ActionResult OcenZawodnika(string Ocena,Ocena ocenaZaMecz)
        {
            var zawodnik= context.Zawodnik.SingleOrDefault(x=>x.UserName==User.Identity.Name);
            int ocen = 0;
            if (Int32.TryParse(Ocena, out ocen))
            {
                OcenaInnych ocenaInnych= new OcenaInnych();
                ocenaInnych.Wartosc=ocen;
                ocenaInnych.User = User.Identity.Name;
                context.OcenaInnych.Add(ocenaInnych);
                ocenaZaMecz.OcenaInnychs.Add(ocenaInnych);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ocenaZaMecz);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}