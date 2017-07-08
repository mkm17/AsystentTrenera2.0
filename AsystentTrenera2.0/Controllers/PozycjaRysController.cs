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
    public class PozycjaRysController : Controller
    {
        private AsystentTrenera2Context context = new AsystentTrenera2Context();

        //
        // GET: /PozycjaRys/

        public ViewResult Index()
        {
            return View(context.PozycjaRys.Include(pozycjarys => pozycjarys.Pozycja).ToList());
        }

        //
        // GET: /PozycjaRys/Details/5

        public ViewResult Details(int id)
        {
            PozycjaRys pozycjarys = context.PozycjaRys.Single(x => x.PozycjaRysId == id);
            return View(pozycjarys);
        }

        //
        // GET: /PozycjaRys/Create

        public ActionResult Create()
        {
            ViewBag.PossiblePozycja = context.Pozycja;
            return View();
        } 

        //
        // POST: /PozycjaRys/Create

        [HttpPost]
        public ActionResult Create(PozycjaRys pozycjarys)
        {
            if (ModelState.IsValid)
            {
                context.PozycjaRys.Add(pozycjarys);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossiblePozycja = context.Pozycja;
            return View(pozycjarys);
        }
        
        //
        // GET: /PozycjaRys/Edit/5
 
        public ActionResult Edit(int id)
        {
            PozycjaRys pozycjarys = context.PozycjaRys.Single(x => x.PozycjaRysId == id);
            ViewBag.PossiblePozycja = context.Pozycja;
            return View(pozycjarys);
        }

        //
        // POST: /PozycjaRys/Edit/5

        [HttpPost]
        public ActionResult Edit(PozycjaRys pozycjarys)
        {
            if (ModelState.IsValid)
            {
                context.Entry(pozycjarys).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossiblePozycja = context.Pozycja;
            return View(pozycjarys);
        }

        //
        // GET: /PozycjaRys/Delete/5
 
        public ActionResult Delete(int id)
        {
            PozycjaRys pozycjarys = context.PozycjaRys.Single(x => x.PozycjaRysId == id);
            return View(pozycjarys);
        }

        //
        // POST: /PozycjaRys/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PozycjaRys pozycjarys = context.PozycjaRys.Single(x => x.PozycjaRysId == id);
            context.PozycjaRys.Remove(pozycjarys);
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