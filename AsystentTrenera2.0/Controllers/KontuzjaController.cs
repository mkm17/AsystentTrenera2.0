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
    public class KontuzjaController : Controller
    {
        private AsystentTrenera2Context context = new AsystentTrenera2Context();

        //
        // GET: /Kontuzja/

        public ViewResult Index()
        {
            return View(context.Kontuzja.Include(kontuzja => kontuzja.Zawodnik).ToList());
        }

        //
        // GET: /Kontuzja/Details/5

        public ViewResult Details(int id)
        {
            Kontuzja kontuzja = context.Kontuzja.Single(x => x.KontuzjaId == id);
            return View(kontuzja);
        }

        //
        // GET: /Kontuzja/Create

        public ActionResult Create()
        {
            ViewBag.PossibleZawodnik = context.Zawodnik;
            return View();
        } 

        //
        // POST: /Kontuzja/Create

        [HttpPost]
        public ActionResult Create(Kontuzja kontuzja)
        {
            if (ModelState.IsValid)
            {
                context.Kontuzja.Add(kontuzja);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleZawodnik = context.Zawodnik;
            return View(kontuzja);
        }
        
        //
        // GET: /Kontuzja/Edit/5
 
        public ActionResult Edit(int id)
        {
            Kontuzja kontuzja = context.Kontuzja.Single(x => x.KontuzjaId == id);
            ViewBag.PossibleZawodnik = context.Zawodnik;
            return View(kontuzja);
        }

        //
        // POST: /Kontuzja/Edit/5

        [HttpPost]
        public ActionResult Edit(Kontuzja kontuzja)
        {
            if (ModelState.IsValid)
            {
                context.Entry(kontuzja).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleZawodnik = context.Zawodnik;
            return View(kontuzja);
        }

        //
        // GET: /Kontuzja/Delete/5
 
        public ActionResult Delete(int id)
        {
            Kontuzja kontuzja = context.Kontuzja.Single(x => x.KontuzjaId == id);
            return View(kontuzja);
        }

        //
        // POST: /Kontuzja/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Kontuzja kontuzja = context.Kontuzja.Single(x => x.KontuzjaId == id);
            context.Kontuzja.Remove(kontuzja);
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