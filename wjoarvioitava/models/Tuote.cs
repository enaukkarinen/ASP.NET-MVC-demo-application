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
    public class Tuote
    {
        // Ominaisuudet
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Selitys { get; set; }

        [DisplayFormat(DataFormatString = "{0:n} €")]
        public decimal Hinta { get; set; }
        public Nullable<int> VeroId { get; set; }

        // Navigaatio-ominaisuudet
        public virtual ICollection<Ostos> Ostokset { get; set; }
        public virtual Verokanta Verokanta { get; set; }

        // Konstruktorit
        public Tuote()
        {
            this.Ostokset = new List<Ostos>();
        }

    }
}