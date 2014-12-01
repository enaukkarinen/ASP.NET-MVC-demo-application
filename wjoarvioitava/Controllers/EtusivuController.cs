using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WJOArvioitava.Models;

namespace WJOArvioitava.Controllers
{
    public class EtusivuController : Controller
    {
        private KauppaRepository kr = new KauppaRepository();
        //
        // GET: /Etusivu/
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
	}
}