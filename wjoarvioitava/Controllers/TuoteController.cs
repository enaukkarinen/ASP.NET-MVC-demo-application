using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WJOArvioitava.Models;

namespace WJOArvioitava.Controllers
{
    public class TuoteController : Controller
    {
        private KauppaRepository _kr = new KauppaRepository();
        //
        // GET: /Tuote/
        public ActionResult Index()
        {
            var tuotteet = _kr.HaeTuotteet("").ToList();
            return View(tuotteet);
        }

        [HttpPost]
        public PartialViewResult Index(string q)
        {
            var tuotteet = _kr.HaeTuotteet(q).ToList();
            return PartialView("_TuoteLista", tuotteet);
        }

        // Ajax
        // GET: /Tuote/Etsi/nimi
        public PartialViewResult Etsi(string q)
        {
            var tuotteet = _kr.HaeTuotteet(q).ToList();
            return PartialView("_TuoteLista", tuotteet);
        }
	}
}