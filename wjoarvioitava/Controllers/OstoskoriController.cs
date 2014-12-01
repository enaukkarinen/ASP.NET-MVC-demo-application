using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WJOArvioitava.Models;

namespace WJOArvioitava.Controllers
{
    public class OstoskoriController : Controller
    {
        private KauppaRepository kr = new KauppaRepository();

        //
        // GET: /Ostoskori/
        [Authorize]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                Asiakas asiakas = kr.HaeAsiakas(User.Identity.Name);
                Ostoskori ostoskori = kr.HaeOstoskori(asiakas.Id);
                return View(ostoskori);
            }
            else
            {
                return View();
            }
        }
        [Authorize]
        public ActionResult Muuta(int id, int tuoteid)
        {
            Ostos ostos = kr.HaeOstos(id, tuoteid);
            return View(ostos);
        }

        [HttpPost]
        public ActionResult Muuta(Ostos muutettu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    kr.MuutaOstos(muutettu);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(muutettu);
                }
            }
            catch
            {
                return View();
            }

        }

        [Authorize]
        public ActionResult Poista(int id, int tuoteid)
        {
            Ostos ostos = kr.HaeOstos(id, tuoteid);
            return View(ostos);
        }

        [HttpPost]
        public ActionResult Poista(Ostos poistettava)
        {
            try
            {
                kr.PoistaOstos(kr.HaeOstos(poistettava.Id, poistettava.TuoteId));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        public ActionResult Tilaa(int id)
        {
            List<Maksutapa> lista = kr.HaeMaksutavat().ToList();
            List<string> mnimet = new List<string>();
            foreach(var m in lista){
                mnimet.Add(m.Nimi);
            }

            ViewBag.Maksutavat = mnimet;
            Ostoskori ok = kr.HaeOstoskoriById(id);
            return View(ok);
        }

        [HttpPost]
        public ActionResult Tilaa(int id, Ostoskori tilattava)
        {
            try
            {
                Ostoskori o = kr.HaeOstoskoriById(id);
                kr.TilaaOstoskori(o);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }
        public PartialViewResult HaeOstosKoriPartial(bool otsikko = false)
        {
            if (User.Identity.IsAuthenticated)
            {
                Asiakas asiakas = kr.HaeAsiakas(User.Identity.Name);
                Ostoskori ostoskori = kr.HaeOstoskori(asiakas.Id);
                return PartialView("~/Views/_OstoskoriPartial.cshtml", new OstoskoriPartialModel { Ostoskori = ostoskori, NaytaOtsikko = otsikko });
            }
            else
            {
                return null;
            }
        }

        public PartialViewResult Lisaa(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Asiakas asiakas = kr.HaeAsiakas(User.Identity.Name);
                Ostoskori kori = kr.HaeOstoskori(asiakas.Id);
                kr.LisaaTuoteOstoskoriin(kori, id);

                return PartialView("~/Views/_OstoskoriPartial.cshtml", new OstoskoriPartialModel { Ostoskori = kori, NaytaOtsikko = false });
            }
            else
            {
                return PartialView();
            }
        }
	}
}