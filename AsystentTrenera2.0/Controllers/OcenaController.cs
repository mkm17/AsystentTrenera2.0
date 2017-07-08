using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsystentTrenera2.Models;

namespace AsystentTrenera2._0.Controllers
{   
    public class OcenaController : Controller
    {
        private AsystentTrenera2Context context = new AsystentTrenera2Context();

        //
        // GET: /Ocena/

        public ViewResult Index()
        {
            return View(context.Ocena.Include(ocena => ocena.Mecz).Include(ocena => ocena.Pozycja).Include(ocena => ocena.Zawodnik).Include(ocena => ocena.OcenaInnychs).ToList());
        }

        //
        // GET: /Ocena/Details/5

        public ViewResult Details(int id)
        {
            Ocena ocena = context.Ocena.Single(x => x.OcenaId == id);
            return View(ocena);
        }

        //
        // GET: /Ocena/Create

        public ActionResult Create()
        {
            ViewBag.PossibleMecz = context.Mecz;
            ViewBag.PossiblePozycja = context.Pozycja;
            ViewBag.PossibleZawodnik = context.Zawodnik;
            return View();
        } 

        //
        // POST: /Ocena/Create

        [HttpPost]
        public ActionResult Create(Ocena ocena)
        {
            if (ModelState.IsValid)
            {
                context.Ocena.Add(ocena);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleMecz = context.Mecz;
            ViewBag.PossiblePozycja = context.Pozycja;
            ViewBag.PossibleZawodnik = context.Zawodnik;
            return View(ocena);
        }
        
        //
        // GET: /Ocena/Edit/5
 
        public ActionResult Edit(int id)
        {
            Ocena ocena = context.Ocena.Single(x => x.OcenaId == id);
            ViewBag.PossibleMecz = context.Mecz;
            ViewBag.PossiblePozycja = context.Pozycja;
            ViewBag.PossibleZawodnik = context.Zawodnik;
            return View(ocena);
        }

        //
        // POST: /Ocena/Edit/5

        [HttpPost]
        public ActionResult Edit(Ocena ocena)
        {
            if (ModelState.IsValid)
            {
                context.Entry(ocena).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleMecz = context.Mecz;
            ViewBag.PossiblePozycja = context.Pozycja;
            ViewBag.PossibleZawodnik = context.Zawodnik;
            return View(ocena);
        }

        //
        // GET: /Ocena/Delete/5
 
        public ActionResult Delete(int id)
        {
            Ocena ocena = context.Ocena.Single(x => x.OcenaId == id);
            return View(ocena);
        }

        //
        // POST: /Ocena/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ocena ocena = context.Ocena.Single(x => x.OcenaId == id);
            context.Ocena.Remove(ocena);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}