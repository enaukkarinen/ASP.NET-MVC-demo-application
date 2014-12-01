using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc; 

namespace WJOArvioitava.Models
{
    public partial class Ostos
    {
        // Ominaisuudet
        public int Id { get; set; }
        public int TuoteId { get; set; }

        [Range(typeof(Decimal), "1", "999", ErrorMessage = "Määrän tulee olla positiivinen luku.")]
        [Required(ErrorMessage = "Määrä on pakollinen.")]
        public decimal Maara { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} €")]
        public decimal YksikkoHinta { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} €")]
        public Nullable<decimal> AlennusProsentti { get; set; }

        // Navigaatio-ominaisuudet
        public virtual Ostoskori Ostoskori { get; set; }
        public virtual Tuote Tuote { get; set; }
    }
}