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
    public partial class Ostoskori
    {
        // Ominaisuudet
        public int Id { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ViimeksiMuokattu { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> TilausPvm { get; set; }
        public int Tilaaja { get; set; }
        public int Tila { get; set; }
        public int MaksutapaId { get; set; }

        // Navigaatio-ominaisuudet
        public virtual ICollection<Ostos> Ostokset { get; set; }
        public virtual Maksutapa Maksutapa { get; set; }
        public virtual OstoskoriTila OstoskoriTila { get; set; }
        public virtual Asiakas Asiakas { get; set; }

        // Apu-ominaisuudet
        [NotMapped]
        [DisplayName("Maksutapa")]
        public string MaksutapaString { get; set; }

        // Konstruktorit
        public Ostoskori()
        {
            this.Ostokset = new List<Ostos>();
        }
    }
}