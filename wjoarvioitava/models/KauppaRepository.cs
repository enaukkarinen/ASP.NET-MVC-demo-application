using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WJOArvioitava.Models
{
    public class KauppaRepository
    {
        private KauppaContext _kc = new KauppaContext();

        // Asiakas
        public IQueryable<Asiakas> HaeKaikkiAsiakkaat()
        {
            return _kc.Asiakkaat;
        }

        public void LisaaAsiakas(Asiakas a)
        {
            _kc.Asiakkaat.Add(a);
            _kc.SaveChanges();
        }

        public Asiakas HaeAsiakas(int id)
        {
            Asiakas asiakas = _kc.Asiakkaat.Where(a => a.Id == id).FirstOrDefault();
            return asiakas;
        }
        
        public Asiakas HaeAsiakas(string nimi)
        {
            Asiakas asiakas = _kc.Asiakkaat.Where(a => a.Nimi == nimi).FirstOrDefault();
            return asiakas;
        }

        public void MuutaAsiakas(Asiakas muutettava)
        {
            Asiakas vanha = HaeAsiakas(muutettava.Id);
            vanha.Sahkoposti = muutettava.Sahkoposti;
            vanha.Katuosoite = muutettava.Katuosoite;
            vanha.Postinro = muutettava.Postinro;

            if (!String.IsNullOrEmpty(muutettava.KaupunkiString))
            {
                vanha.KaupunkiId = HaeKaupunkiId(muutettava.KaupunkiString);
            }
            else
            {
                vanha.KaupunkiId = null;
            }
            
            vanha.AlennusProsentti = muutettava.AlennusProsentti;
            vanha.Maksukortti = muutettava.Maksukortti;
            _kc.SaveChanges();
        }

        public IQueryable<Kaupunki> HaeKaupungit(string nimi)
        {
            return _kc.Kaupungit.Where(k => k.Nimi.Contains(nimi));
        }

        public int HaeKaupunkiId(string nimi)
        {
            return _kc.Kaupungit.Where(k => k.Nimi == nimi).FirstOrDefault().Id;
        }

        public IQueryable<Kaupunki> HaeKaikkiKaupungit(string nimi)
        {
            return _kc.Kaupungit.Where(r => r.Nimi.StartsWith(nimi) || String.IsNullOrEmpty(nimi));
        }

        // Ostos
        public Ostos HaeOstos(int koriId, int tuoteId)
        {
            Ostos paluu = _kc.Ostokset.Where(o => o.Id == koriId && o.TuoteId == tuoteId).FirstOrDefault();
            return paluu;
        }
        public void MuutaOstos(Ostos muutettava)
        {
            Ostos ostos = HaeOstos(muutettava.Id, muutettava.TuoteId);
            ostos.Maara = muutettava.Maara;
            ostos.TuoteId = muutettava.TuoteId;
            ostos.YksikkoHinta = muutettava.YksikkoHinta;
            ostos.AlennusProsentti = muutettava.AlennusProsentti;
            _kc.SaveChanges();
        }
        public void PoistaOstos(Ostos poistettava)
        {
            _kc.Ostokset.Remove(poistettava);
            _kc.SaveChanges();
        }

        // Tuote
        public IQueryable<Tuote> HaeTuotteet(string nimi, int lkm = 10)
        {
            return _kc.Tuotteet.Where(t => t.Nimi.Contains(nimi) || String.IsNullOrEmpty(nimi)).Take(lkm);
        }
        public Tuote HaeTuote(int id)
        {
            return _kc.Tuotteet.Where(t => t.Id == id).FirstOrDefault();
        }

        // Ostoskori
        public void LisaaTuoteOstoskoriin(Ostoskori o, int tuoteid)
        {
            Ostoskori ostoskori = _kc.Ostoskorit.Where(kori => kori.Id == o.Id).SingleOrDefault();
            Tuote lisattava = HaeTuote(tuoteid);
            
            Ostos ostos = new Ostos { Id = ostoskori.Id, TuoteId = lisattava.Id, Maara = 1, YksikkoHinta = lisattava.Hinta };
            
            // Katsotaan löytyykö tuote jo korista
            if (ostoskori.Ostokset.Any(x => x.TuoteId == ostos.TuoteId))
            {
                Ostos vanha = ostoskori.Ostokset.Single(vo => vo.Id == ostos.Id && vo.TuoteId == ostos.TuoteId);
                vanha.Maara++;
            }
            else
            {
                ostoskori.Ostokset.Add(ostos);
            }
            
            _kc.SaveChanges();
        }

        public Ostoskori HaeOstoskori(int asiakasid, bool teeUusi = true)
        {
            Ostoskori os;

            os = _kc.Ostoskorit.Where(o => o.Tilaaja == asiakasid && o.Tila != 2).SingleOrDefault();
 
            if(os == null && teeUusi)
            {
                return UusiOstoskori(asiakasid);
            }
            else
            {
                return os;
            }
        }

        public Ostoskori HaeOstoskoriById(int id)
        {
            return _kc.Ostoskorit.Where(ok => ok.Id == id).SingleOrDefault();
        }

        public Ostoskori UusiOstoskori(int asiakasid)
        {

            _kc.Ostoskorit.Add(new Ostoskori { ViimeksiMuokattu = DateTime.Now, Tilaaja = asiakasid, Tila = 1, MaksutapaId = 1 });
            _kc.SaveChanges();
            return HaeOstoskori(asiakasid);
        }
        public Ostoskori TilaaOstoskori(Ostoskori ok)
        {
            ok.Tila = 2;
            ok.TilausPvm = DateTime.Now;
            _kc.SaveChanges();
            return ok;
        }

        // Maksutapa
        public IQueryable<Maksutapa> HaeMaksutavat()
        {
            return _kc.Maksutavat;
        }
    }
}