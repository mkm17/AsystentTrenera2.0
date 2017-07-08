using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AsystentTrenera2.Models
{
    public class Kontuzja
    {
        public int KontuzjaId { get; set; }
        public bool Aktualna { get; set; }
        public string KontuzjaNazwa { get; set; }
        public DateTime KontuzjaPoczatek { get; set; }
        public DateTime KontuzjaZakonczenie { get; set; }

        public int ZawodnikId { get; set; }
        [ForeignKey("ZawodnikId")]
        public virtual Zawodnik Zawodnik { get; set; }
       
        public override string ToString()
        {
            return KontuzjaNazwa;
        }
    }
}