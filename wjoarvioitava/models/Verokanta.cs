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
    public class Verokanta
    {
        // Ominaisuudet
        public int Id { get; set; }
        public string Nimi { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Veroprosentti { get; set; }

        // Navigaatio-ominaisuudet
        public virtual ICollection<Tuote> Tuotteet { get; set; }

        public Verokanta()
        {
            this.Tuotteet = new List<Tuote>();
        }
    }
}