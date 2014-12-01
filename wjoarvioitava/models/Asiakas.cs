using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WJOArvioitava.Models
{
    public partial class Asiakas
    {
        // Ominaisuudet
        public int Id { get; set; }
        public string Nimi { get; set; } // (256)

        [DisplayName("Sähköposti")]
        public string Sahkoposti { get; set; } // 200
        public string Katuosoite { get; set; }

        [StringLength(5, ErrorMessage = "Postinumeron tulee olla viisi merkkiä pitkä.", MinimumLength = 5)]
        [DisplayName("Postinumero")]
        public string Postinro { get; set; }

        [DisplayName("Postitoimipaikka")]
        public Nullable<int> KaupunkiId { get; set; }        
        public Nullable<decimal> AlennusProsentti { get; set; }
        public string Maksukortti { get; set; }

        // Navigaatio-ominaisuudet
        public virtual ICollection<Ostoskori> Ostoskorit { get; set; }
        public virtual Kaupunki Kaupunki { get; set; }

        // Apu-ominaisuus
        [NotMapped]
        [DisplayName("Kaupunki")]
        public string KaupunkiString { get; set; }

        // Konstruktorit
        public Asiakas()
        {
            this.Ostoskorit = new List<Ostoskori>();
        }

    }
}