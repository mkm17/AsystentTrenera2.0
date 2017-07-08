using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AsystentTrenera2.Models
{
    public class OcenaOgolna
    {
        public int OcenaOgolnaId { get; set; }
        public decimal Ocen { get; set; }
        public decimal Gole { get; set; }
        public decimal Strzal { get; set; }
        public decimal Podania { get; set; }
        public decimal Faule { get; set; }
        public decimal Bledy { get; set; }
        public decimal Przechwyty { get; set; }
        public decimal Rajdy { get; set; }
        public decimal Kilometry { get; set; }

    }
}