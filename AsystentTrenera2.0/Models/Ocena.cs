using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AsystentTrenera2.Models
{
    public class Ocena
    {

        public int OcenaId { get; set; }
        public int MeczId { get; set; }
        [ForeignKey("MeczId")]
        public virtual Mecz Mecz { get; set; }
        public int PozycjaId { get; set; }
        [ForeignKey("PozycjaId")]
        public virtual Pozycja Pozycja { get; set; }
        public int ZawodnikId { get; set; }
        [ForeignKey("ZawodnikId")]
        public virtual Zawodnik Zawodnik { get; set; }

        public ICollection<OcenaInnych> OcenaInnychs { get; set; }


        public decimal Ocen { get; set; }
        
        public decimal Gole { get; set; }
        public decimal Strzal { get; set; }
        public decimal Podania { get; set; }
        public decimal Faule { get; set; }
        public decimal Bledy { get; set; }
        public decimal Przechwyty { get; set; }
        public decimal Rajdy { get; set; }
        public decimal Kilometry { get; set; }

        public int GoleStrzelone { get; set; }
        public int StrzalyCelne { get; set; }
        public int StrzalyNiecelne { get; set; }
        public int PodaniaCelne { get; set; }
        public int PodaniaNiecelne { get; set; }
        public int StratywObronie { get; set; }
        public int Odbiory { get; set; }
        public int RajdyUdane { get; set; }
        public int RajdyNieudane { get; set; }
 
        public decimal ObliczOcene()
        {
            // funkcja sciaga z bazy danych dane dotyczace pozycji oraz meczu 
            //dla kazdego meczu jest liczona osobna ocena
            decimal wynikidealny = (1 * Pozycja.Gole + 1 * Pozycja.StrzalyCelne + 1 * Pozycja.Rajdy +
                1 * Pozycja.PodaniaCelne + 1 * Pozycja.Przechwyty - 0 * Pozycja.Faule - 0 * Pozycja.Bledy);
            decimal mianownik = (Gole * Pozycja.Gole + Strzal * Pozycja.StrzalyCelne + Rajdy * Pozycja.Rajdy +
                Podania * Pozycja.PodaniaCelne + Przechwyty * Pozycja.Przechwyty - Faule * Pozycja.Faule - Bledy * Pozycja.Bledy);
            //liczone sa 2 oceny  pierwszy do wynik idealny dla danej pozycji
            // 2 ocena to ocena jaka naprawde uzyskal zawodnik
            // wynikiem jest  ocena rzeczywista przez ocene idealna * 10 
            var _ocen = mianownik / wynikidealny * 10;
            return _ocen;
        }
        public decimal ObliczOceneInnych()
        {
            
            var suma=0;
            foreach (OcenaInnych ocena in this.OcenaInnychs)
            {
                suma += ocena.Wartosc;
            }
            return suma / this.OcenaInnychs.Count();
        }
        public override string ToString()
        {
            return Ocen.ToString();
        }
    }
}