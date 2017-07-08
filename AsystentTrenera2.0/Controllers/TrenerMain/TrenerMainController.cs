using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsystentTrenera2.Models;
using System.Data.Entity;
using System.Data;
using System.Drawing;

namespace AsystentTrenera2.Controllers.TrenerMain
{
  //  [Authorize(Roles="Trener")]
    public class TrenerMainController : Controller
    {
        //
        // GET: /TrenerMain/
        AsystentTrenera2Context context = new AsystentTrenera2Context();

        public ActionResult Index()
        {   
            return View();
        }
        public ActionResult Sklad()
        {
            var Zawodnicy = context.Zawodnik.Include(zawodnik => zawodnik.Kontuzjas).Include(zawodnik=>zawodnik.Ocenas).Include(zawodnik=>zawodnik.OcenaOgolna);
            SelectList lista = new SelectList(context.Zawodnik, "ZawodnikId", "Nazwisko");
            ViewBag.Zawodnicy = lista;
            return View(Zawodnicy);
        }
        public ActionResult Taktyka()
        {
            var Zawodnicy = context.Zawodnik.Include(zawodnik => zawodnik.Kontuzjas).Include(zawodnik=>zawodnik.Ocenas).Include(zawodnik=>zawodnik.OcenaOgolna);
            SelectList lista = new SelectList(context.Zawodnik, "ZawodnikId", "Nazwisko");
            ViewBag.Zawodnicy = lista;
            return View(Zawodnicy);
        }
        [HttpPost]
        public JsonResult TaktykaPozycje(IEnumerable<int> left,IEnumerable<int> top)
        {

            List<int> lewa=left.ToList();
            List<int> gora=top.ToList();
            List<Rectangle> Rectangles=new List<Rectangle>();
            string[] ListaLabelow =new string[6];
            Rectangles.Add(new Rectangle(lewa[0], gora[0], 40, 40));
            Rectangles.Add( new Rectangle(lewa[1], gora[1], 40, 40));
            Rectangles.Add( new Rectangle(lewa[2], gora[2], 40, 40));
            Rectangles.Add( new Rectangle(lewa[3], gora[3], 40, 40));
            Rectangles.Add( new Rectangle(lewa[4], gora[4], 40, 40));
            Rectangles.Add( new Rectangle(lewa[5], gora[5], 40, 40));

            List<PozycjaRys> ListaPozycji=context.PozycjaRys.Include(x=>x.Pozycja).ToList();

            for (int i = 0; i < Rectangles.Count; i++)
            {
                for (int j = 0; j <8 ;j++ )
                {//todo
                    if (ListaPozycji[j].UtworzProstokat().IntersectsWith(Rectangles[i]))
                        
                        
                    {
                        ListaLabelow[i] = ListaPozycji[j].Pozycja.Nazwa; 
                    }
                }
            }
            ViewBag.Labele = ListaLabelow;
            ViewBag.Pozycje = ListaPozycji;
            return Json(ListaLabelow,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult OptymalnySklad(IEnumerable<string> pozycje)
        {
            Random r=new Random();
            List<string> lista = new List<string>();
            List<string> listaPozycji = pozycje.ToList();
            List<Pozycja> pozycjas = new List<Pozycja>();
            List<decimal> listaOcen = new List<decimal>();
            List<Zawodnik> listaZawodnikow=context.Zawodnik.Include(x=>x.Ocenas).ToList();
            List<Zawodnik> listaLiczonychZaw = new List<Zawodnik>(6);
            List<Zawodnik> listaNajlepszych = new List<Zawodnik>(6);
            bool[] uzyty=new bool[listaZawodnikow.Count];
            decimal wynikMax = 0;
            decimal wynikObl = 0;
            for (int i = 0; i < 6; i++)
            {
                listaNajlepszych.Add(new Zawodnik { });
                listaLiczonychZaw.Add(new Zawodnik { });
                listaOcen.Add(0);
                var pozi = listaPozycji[i].ToString();
                var poz = context.Pozycja.SingleOrDefault(x => x.Nazwa == pozi);
                pozycjas.Add(poz);
                uzyty[i] = false;
            }
            for(int i=0;i<10000;i++)
            {

                for(int j=0;j<6;j++)
                {
                    int nrRandom = r.Next(listaZawodnikow.Count);
                    while (uzyty[nrRandom])
                    {
                        nrRandom = r.Next(listaZawodnikow.Count);
                    }
                        if (!uzyty[nrRandom] && listaZawodnikow[nrRandom].Kontuzjas == null)
                        {
                            {
                                listaOcen[j] = ObliczOceneDoPozycji(listaZawodnikow[nrRandom], pozycjas[j]);
                                listaLiczonychZaw[j] = listaZawodnikow[nrRandom];
                                uzyty[nrRandom] = true;
                            }
                        }
                }
                    wynikObl = listaOcen.Sum();
                    if (wynikObl > wynikMax)
                    {
                        wynikMax = wynikObl;
                        for (int x = 0; x < 6; x++)
                        {
                            listaNajlepszych[x] = listaLiczonychZaw[x];
                        }
                    }
                    
                    for (int y = 0; y < uzyty.Length; y++)
                    {
                        uzyty[y] = false;
                    }
                
            }
            foreach (var zaw in listaNajlepszych)
            {
                lista.Add(zaw.Nazwisko);
            }
            
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ZawodnicyDoPozycji(IEnumerable<string> pozycje, IEnumerable<string> zawodnicy)
        {
            decimal[] listaWynikow = new decimal[6];
            List<string> listaPozycji=pozycje.ToList();
            List<string> listaZawodników=zawodnicy.ToList();
            for (int i=0;i<6;i++)
            {
                var poz=listaPozycji[i];
                var pozycja = (Pozycja)context.Pozycja.SingleOrDefault(x => x.Nazwa == poz);
                var zaw=listaZawodników[i];
                var zawodnik = (Zawodnik)context.Zawodnik.Include(x=>x.Ocenas).SingleOrDefault(x => x.Nazwisko ==zaw );
                listaWynikow[i] = ObliczOceneDoPozycji(zawodnik, pozycja);
            }
            
            return Json(listaWynikow,JsonRequestBehavior.AllowGet);
        }


        public decimal ObliczOceneDoPozycji(Zawodnik Zawodnik, Pozycja Pozycja)
        {
            //pobieramy do obliczen 3 ostatne spotkania
            //tutaj moga byc 2 podejscia 
            //1. średnia z 3 ostatnich spotkań
            //2. Obliczanie jakie oceny miałby zawodnik gdyby gral na wskazanej pozycji
            if (Zawodnik.Ocenas.Count < 3)
            {
                return 0;
            }
            int liczbaOcen = Zawodnik.Ocenas.Count;
            List<Ocena> listaOcen = Zawodnik.Ocenas.ToList();
            Ocena o1 = listaOcen[liczbaOcen - 1];
            Ocena o2 = listaOcen[liczbaOcen - 2];
            Ocena o3 = listaOcen[liczbaOcen - 3];
            decimal wynikidealny = (1 * Pozycja.Gole + 1 * Pozycja.StrzalyCelne + 1 * Pozycja.Rajdy +
                1 * Pozycja.PodaniaCelne + 1 * Pozycja.Przechwyty - 0 * Pozycja.Faule - 0 * Pozycja.Bledy);
            decimal licznik1 = (o1.Gole * Pozycja.Gole + o1.Strzal * Pozycja.StrzalyCelne + o1.Rajdy * Pozycja.Rajdy +
    o1.Podania * Pozycja.PodaniaCelne + o1.Przechwyty * Pozycja.Przechwyty - o1.Faule * Pozycja.Faule - o1.Bledy * Pozycja.Bledy);
            decimal licznik2 = (o2.Gole * Pozycja.Gole + o2.Strzal * Pozycja.StrzalyCelne + o2.Rajdy * Pozycja.Rajdy +
    o2.Podania * Pozycja.PodaniaCelne + o2.Przechwyty * Pozycja.Przechwyty - o2.Faule * Pozycja.Faule - o2.Bledy * Pozycja.Bledy);
            decimal licznik3 = (o3.Gole * Pozycja.Gole + o3.Strzal * Pozycja.StrzalyCelne + o3.Rajdy * Pozycja.Rajdy +
    o3.Podania * Pozycja.PodaniaCelne + o3.Przechwyty * Pozycja.Przechwyty - o3.Faule * Pozycja.Faule - o3.Bledy * Pozycja.Bledy);
            decimal wynik = Math.Round((licznik1 / wynikidealny + licznik2 / wynikidealny + licznik3 / wynikidealny) / 3, 2) * 10;
            return wynik;

        }
 
   }
}
