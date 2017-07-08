using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace AsystentTrenera2.Models
{
    public class PozycjaRys
    {
        public int PozycjaRysId { get; set; }
        public int X1 { get; set; }
        public int X2 { get; set; }
        public int Y1 { get; set; }
        public int Y2 { get; set; }

        public int PozycjaId { get; set; }
        [ForeignKey("PozycjaId")]
        public virtual Pozycja Pozycja { get; set; }

        public Rectangle UtworzProstokat()
        {
            var rectangle = new Rectangle(X1, Y1, X2, Y2);
            return rectangle;
        }

    }
}