using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WJOArvioitava.Models
{
    public partial class Maksutapa
    {
        // Ominaisuudet
        public int Id { get; set; }
        public string Nimi { get; set; }

        // Navigaatio-ominaisuudet
        public virtual ICollection<Ostoskori> Ostoskorit { get; set; }

        public Maksutapa()
        {
            this.Ostoskorit = new List<Ostoskori>();
        }
    }
}