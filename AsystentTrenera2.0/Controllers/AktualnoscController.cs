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
    public class AktualnoscController : Controller
    {
        private AsystentTrenera2Context context = new AsystentTrenera2Context();

        //
        // GET: /Aktualnosc/

        public ViewResult Index()
        {
            return View(context.Aktualnosc.ToList());
        }

        //
        // GET: /Aktualnosc/Details/5

        public ViewResult Details(int id)
        {
            Aktualnosc aktualnosc = context.Aktualnosc.Single(x => x.AktualnoscId == id);
            return View(aktualnosc);
        }

        //
        // GET: /Aktualnosc/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Aktualnosc/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Aktualnosc aktualnosc)
        {
            if (ModelState.IsValid)
            {
                context.Aktualnosc.Add(aktualnosc);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(aktualnosc);
        }
        
        //
        // GET: /Aktualnosc/Edit/5
 
        public ActionResult Edit(int id)
        {
            Aktualnosc aktualnosc = context.Aktualnosc.Single(x => x.AktualnoscId == id);
            return View(aktualnosc);
        }

        //
        // POST: /Aktualnosc/Edit/5

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Aktualnosc aktualnosc)
        {
            if (ModelState.IsValid)
            {
                context.Entry(aktualnosc).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aktualnosc);
        }

        //
        // GET: /Aktualnosc/Delete/5
 
        public ActionResult Delete(int id)
        {
            Aktualnosc aktualnosc = context.Aktualnosc.Single(x => x.AktualnoscId == id);
            return View(aktualnosc);
        }

        //
        // POST: /Aktualnosc/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Aktualnosc aktualnosc = context.Aktualnosc.Single(x => x.AktualnoscId == id);
            context.Aktualnosc.Remove(aktualnosc);
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