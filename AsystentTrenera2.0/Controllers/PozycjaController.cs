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
    public class PozycjaController : Controller
    {
        private AsystentTrenera2Context context = new AsystentTrenera2Context();

        //
        // GET: /Pozycja/

        public ViewResult Index()
        {
            return View(context.Pozycja.ToList());
        }

        //
        // GET: /Pozycja/Details/5

        public ViewResult Details(int id)
        {
            Pozycja pozycja = context.Pozycja.Single(x => x.PozycjaId == id);
            return View(pozycja);
        }

        //
        // GET: /Pozycja/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Pozycja/Create

        [HttpPost]
        public ActionResult Create(Pozycja pozycja)
        {
            if (ModelState.IsValid)
            {
                context.Pozycja.Add(pozycja);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(pozycja);
        }
        
        //
        // GET: /Pozycja/Edit/5
 
        public ActionResult Edit(int id)
        {
            Pozycja pozycja = context.Pozycja.Single(x => x.PozycjaId == id);
            return View(pozycja);
        }

        //
        // POST: /Pozycja/Edit/5

        [HttpPost]
        public ActionResult Edit(Pozycja pozycja)
        {
            if (ModelState.IsValid)
            {
                context.Entry(pozycja).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pozycja);
        }

        //
        // GET: /Pozycja/Delete/5
 
        public ActionResult Delete(int id)
        {
            Pozycja pozycja = context.Pozycja.Single(x => x.PozycjaId == id);
            return View(pozycja);
        }

        //
        // POST: /Pozycja/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Pozycja pozycja = context.Pozycja.Single(x => x.PozycjaId == id);
            context.Pozycja.Remove(pozycja);
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