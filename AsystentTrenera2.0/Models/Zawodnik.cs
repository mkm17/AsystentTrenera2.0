using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AsystentTrenera2.Models
{
    public class Zawodnik
    {
       
        public int ZawodnikId { get; set; }

        [Required]
        public string Imie { get; set; }

        [Required]
        public string PozycjaZawodnika { get; set; }

        [Required]
        public string Nazwisko { get; set; }

        [Required]
        public int Wzrost { get; set; }

        [Required]
        public int Waga { get; set; }

        [Required]
        public int Wiek { get; set; }

        [Required]
        public string Noga { get; set; }

        //[Required]
        //public int PozycjaId { get; set; }
        //[ForeignKey("PozycjaId")]
        //public virtual Pozycja Pozycja { get; set; }

        public string UserName { get; set; }

        public ICollection<Ocena> Ocenas { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        public int? OcenaOgolnaId { get; set; }
        [ForeignKey("OcenaOgolnaId")]
        public virtual OcenaOgolna OcenaOgolna { get; set; }

        public ICollection<Kontuzja> Kontuzjas { get; set; }

        public override string ToString()
        {
            return Imie+" "+Nazwisko;
        }
    }
}