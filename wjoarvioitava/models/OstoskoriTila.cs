using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WJOArvioitava.Models
{
    public partial class OstoskoriTila
    {
        // ominaisuudet
        public int Id { get; set; }
        public string Nimi { get; set; }
        
        // Navigaatio-ominaisuudet
        public virtual ICollection<Ostoskori> Ostoskorit { get; set; }

        public OstoskoriTila()
        {
            this.Ostoskorit = new List<Ostoskori>();
        }

    }
}