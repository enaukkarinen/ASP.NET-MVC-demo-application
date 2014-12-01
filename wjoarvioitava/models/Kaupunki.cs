using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WJOArvioitava.Models
{
    public partial class Kaupunki
    {
        // Ominaisuudet
        public int Id { get; set; }
        public string Nimi { get; set; }

        // Navigaatio-ominaisuudet
        public virtual ICollection<Asiakas> Asiakkaat { get; set; }

        // Konstruktorit
        public Kaupunki()
        {
            this.Asiakkaat = new List<Asiakas>();
        }
    }   
}