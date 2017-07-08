using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsystentTrenera2.Models;
using ZedGraph;
using System.Drawing;
using System.IO;

namespace AsystentTrenera2.Controllers
{   
    public class ZawodnikController : Controller
    {
        private AsystentTrenera2Context context = new AsystentTrenera2Context();

        //
        // GET: /Zawodnik/

        public ViewResult Index()
        {
            return View(context.Zawodnik.Include(zawodnik => zawodnik.Ocenas).Include(zawodnik => zawodnik.OcenaOgolna).Include(zawodnik => zawodnik.Kontuzjas).ToList());
        }

        //
        // GET: /Zawodnik/Details/5

        public ViewResult Details(int id)
        {
            Zawodnik zawodnik = context.Zawodnik.Single(x => x.ZawodnikId == id);
            return View(zawodnik);
        }

        //
        // GET: /Zawodnik/Create

        public ActionResult Create()
        {
            ViewBag.PossibleOcenaOgolna = context.OcenaOgolna;
            return View();
        } 

        //
        // POST: /Zawodnik/Create

        [HttpPost]
        public ActionResult Create(Zawodnik zawodnik)
        {
            OcenaOgolna ocena = new OcenaOgolna();
            context.OcenaOgolna.Add(ocena);
            zawodnik.OcenaOgolna = ocena;
            if (ModelState.IsValid)
            {
                context.Zawodnik.Add(zawodnik);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleOcenaOgolna = context.OcenaOgolna;
            return View(zawodnik);
        }
        
        //
        // GET: /Zawodnik/Edit/5
 
        public ActionResult Edit(int id)
        {
            Zawodnik zawodnik = context.Zawodnik.Single(x => x.ZawodnikId == id);
            ViewBag.PossibleOcenaOgolna = context.OcenaOgolna;
            return View(zawodnik);
        }

        //
        // POST: /Zawodnik/Edit/5

        [HttpPost]
        public ActionResult Edit(Zawodnik zawodnik)
        {
            if (ModelState.IsValid)
            {
                context.Entry(zawodnik).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleOcenaOgolna = context.OcenaOgolna;
            return View(zawodnik);
        }

        //
        // GET: /Zawodnik/Delete/5
 
        public ActionResult Delete(int id)
        {
            Zawodnik zawodnik = context.Zawodnik.Single(x => x.ZawodnikId == id);
            return View(zawodnik);
        }

        //
        // POST: /Zawodnik/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Zawodnik zawodnik = context.Zawodnik.Single(x => x.ZawodnikId == id);
            context.Zawodnik.Remove(zawodnik);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult WykresFormyZawodnika(int? IdZawodnika1, int? IdZawodnika2 = null)
        {
            if (IdZawodnika1 == null)
            {
                return RedirectToAction("Index", "Zawodnik");
            }
            var zawodnik1 = context.Zawodnik.Include(x => x.Ocenas).Include(x => x.OcenaOgolna).Single(x => x.ZawodnikId == IdZawodnika1);
            ObliczanieOceny(zawodnik1);
            Zawodnik zawodnik2 = null;
            PointPairList pointserie2 = null;
            if (IdZawodnika2 != null)
            {
                
                zawodnik2 = context.Zawodnik.Include(x => x.Ocenas).Include(x => x.OcenaOgolna).Single(x => x.ZawodnikId == IdZawodnika2);
                pointserie2 = new PointPairList();
                ObliczanieOceny(zawodnik2);
                foreach (Ocena o in zawodnik2.Ocenas)
                {
                    double ocena = Decimal.ToDouble(o.ObliczOcene());
                    pointserie2.Add((double)new XDate(o.Mecz.Data), ocena);
                }
            }

            var zedGraphPane = new GraphPane();
            zedGraphPane.Title.Text = zawodnik1.Nazwisko;
            zedGraphPane.XAxis.Title.Text = "Data";
            zedGraphPane.XAxis.Type = AxisType.Date;
            zedGraphPane.YAxis.Title.Text = "Ocena";

            PointPairList pointserie1 = new PointPairList();

            foreach (Ocena o in zawodnik1.Ocenas)
            {
                double ocena = Decimal.ToDouble(o.ObliczOcene());
                pointserie1.Add((double)new XDate(o.Mecz.Data), ocena);
            }

            LineItem curve1 = zedGraphPane.AddCurve(zawodnik1.Nazwisko,
            pointserie1, Color.Red, SymbolType.None);
            if (zawodnik2 != null)
            {
                LineItem curve2 = zedGraphPane.AddCurve(zawodnik2.Nazwisko,
                pointserie2, Color.Blue, SymbolType.None);
                zedGraphPane.Title.Text += " vs " + zawodnik2.Nazwisko;
            }

            Bitmap bm = new Bitmap(1, 1);
            using (Graphics g = Graphics.FromImage(bm))
                zedGraphPane.AxisChange(g);
            var outStream = new MemoryStream();
            zedGraphPane.GetImage(640, 480, 96, true).Save(outStream, System.Drawing.Imaging.ImageFormat.Png);
            outStream.Position = 0;
            return new FileStreamResult(outStream, "image/png");
        }

        public ActionResult Statystyki(int IdZawodnika1)
        {
            var zawodnik = context.Zawodnik.Include(x => x.Ocenas).Include(x => x.OcenaOgolna).FirstOrDefault(x => x.ZawodnikId == IdZawodnika1);

            ObliczanieOceny(zawodnik);
            return View(zawodnik);
        }
        public void ObliczanieOceny(Zawodnik zawodnik)
        {
            foreach (Ocena ocena in zawodnik.Ocenas)
            {
                zawodnik.OcenaOgolna.Bledy += ocena.Bledy;
                zawodnik.OcenaOgolna.Faule += ocena.Faule;
                zawodnik.OcenaOgolna.Gole += ocena.Gole;
                zawodnik.OcenaOgolna.Kilometry += ocena.Kilometry;
                zawodnik.OcenaOgolna.Ocen += ocena.ObliczOcene();
                zawodnik.OcenaOgolna.Podania += ocena.Podania;
                zawodnik.OcenaOgolna.Przechwyty += ocena.Przechwyty;
                zawodnik.OcenaOgolna.Rajdy += ocena.Rajdy;
                zawodnik.OcenaOgolna.Strzal += ocena.Strzal;

            }
            zawodnik.OcenaOgolna.Bledy = zawodnik.OcenaOgolna.Bledy / zawodnik.Ocenas.Count;
            zawodnik.OcenaOgolna.Faule = zawodnik.OcenaOgolna.Faule / zawodnik.Ocenas.Count;
            zawodnik.OcenaOgolna.Gole = zawodnik.OcenaOgolna.Gole / zawodnik.Ocenas.Count;
            zawodnik.OcenaOgolna.Kilometry = zawodnik.OcenaOgolna.Kilometry / zawodnik.Ocenas.Count;
            zawodnik.OcenaOgolna.Ocen = zawodnik.OcenaOgolna.Ocen / zawodnik.Ocenas.Count;
            zawodnik.OcenaOgolna.Podania = zawodnik.OcenaOgolna.Podania / zawodnik.Ocenas.Count;
            zawodnik.OcenaOgolna.Przechwyty = zawodnik.OcenaOgolna.Przechwyty / zawodnik.Ocenas.Count;
            zawodnik.OcenaOgolna.Rajdy = zawodnik.OcenaOgolna.Rajdy / zawodnik.Ocenas.Count;
            zawodnik.OcenaOgolna.Strzal = zawodnik.OcenaOgolna.Strzal / zawodnik.Ocenas.Count;
            context.SaveChanges();
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