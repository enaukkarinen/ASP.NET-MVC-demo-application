using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WJOArvioitava.Models;

namespace WJOArvioitava.Controllers
{
    public class AsiakasController : Controller
    {
        private KauppaRepository _kr = new KauppaRepository();
        //
        // GET: /Asiakas/
        public ActionResult Index()
        {
            return View();

        }

        public PartialViewResult Tiedot()
        {
            Asiakas a = _kr.HaeAsiakas(User.Identity.Name);
            return PartialView("~/Views/Asiakas/_AsiakasPartial.cshtml", a);
        }

        public ActionResult Muuta()
        {
            Asiakas a = _kr.HaeAsiakas(User.Identity.Name);
            if (a.KaupunkiId != null)
            {
                a.KaupunkiString = a.Kaupunki.Nimi;
            }
            ViewBag.Otsikko = "Muuta asiakastiedot";
            return View(a);
        }
        //
        // POST: /Asiakas/Muuta/
        [HttpPost]
        public ActionResult Muuta(Asiakas asiakas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Lisätään asiakkaan tietoja
                    _kr.MuutaAsiakas(asiakas);
                    return RedirectToAction("Index", "Asiakas");
                }
                else
                {
                    return RedirectToAction("Muuta", "Asiakas", new { nimi = asiakas.Nimi });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Etusivu");
            }
        }


        //
        // GET: /Asiakas/Uusi/nimi
        public ActionResult Uusi(string nimi)
        {
            _kr.LisaaAsiakas(new Asiakas{Nimi = nimi});
            Asiakas a = _kr.HaeAsiakas(nimi);
            ViewBag.Otsikko = "Täydennä asiakastiedot";
            return View(a);
        }

        //
        // POST: /Asiakas/Uusi/
        [HttpPost]
        public ActionResult Uusi(Asiakas asiakas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Lisätään asiakkaan tietoja
                    _kr.MuutaAsiakas(asiakas);
                    return RedirectToAction("Index", "Asiakas");
                }
                else
                {
                    return RedirectToAction("Uusi", "Asiakas", new { nimi = asiakas.Nimi });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Etusivu");
            }
        }

        public ActionResult Etsi(string term) 
        {
            return Json(_kr.HaeKaikkiKaupungit(term).Select(k => new { label = k.Nimi }), JsonRequestBehavior.AllowGet);
        }
	}
}