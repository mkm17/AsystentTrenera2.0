using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AsystentTrenera2.Models
{
    public class Pozycja
    {
        public int PozycjaId { get; set; }

        [Required]
        public string Nazwa { get; set; }

        [Required]
        public int Noga { get; set; }

        [Required]
        public string Strona { get; set; }

        [Required]
        public int Gole { get; set; }

        [Required]
        public int StrzalyCelne { get; set; }

        [Required]
        public int PodaniaCelne { get; set; }

        [Required]
        public int Faule { get; set; }

        [Required]
        public int Bledy { get; set; }

        [Required]
        public int Przechwyty { get; set; }

        [Required]
        public int Rajdy { get; set; }

        public override string ToString()
        {
            return Nazwa;
        }
    }
}