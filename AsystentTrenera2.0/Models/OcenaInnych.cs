using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AsystentTrenera2.Models
{
    public class OcenaInnych
    {
        public int OcenaInnychId { get; set; }
        public string User { get; set; }
        public int OcenaId { get; set; }
        [ForeignKey("OcenaId")]
        public virtual Ocena Ocena { get; set; }
        
        [Range(1,10)]
        public int Wartosc { get; set; }

    }
    
}