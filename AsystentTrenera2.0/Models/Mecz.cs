using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AsystentTrenera2.Models
{
    
    public class Mecz
    {
        public int MeczId { get; set; }
        public string Przeciwnik { get; set; }
        public DateTime Data { get; set; }
        public bool Dom { get; set; }
        public int Strzelone { get; set; }
        public int Stracone { get; set; }

      /*  public int TaktykaId { get; set; }
        [ForeignKey("TaktykaId")]
        public virtual Taktyka Taktyka { get; set; }*/
        
        
        public override string ToString()
        {
            return Przeciwnik;
        }
    }
}