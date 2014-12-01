using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WJOArvioitava.Models;

namespace WJOArvioitava.Controllers
{
    [Authorize]
    public class KayttajaController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public KayttajaController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public KayttajaController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        //
        // GET: /Kayttaja/Rekisteroi
        [AllowAnonymous]
        public ActionResult Rekisteroi()
        {
            return View();
        }

        //
        // POST: /Kayttaja/Rekisteroi
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Rekisteroi(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Uusi", "Asiakas", new {nimi = model.UserName});
                }
                else
                {
                    AddErrors(result);
                }
            }

            // Jos tultiin tähän, jotain meni vikaan, jolloin näytetään form
            return View(model);
        }

        //
        // GET: /Kayttaja/Sisaan
        [AllowAnonymous]
        public ActionResult Sisaan(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Kayttaja/Sisaan
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Sisaan(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Virheellinen käyttäjänimi tai salasana.");
                }
            }

            //Jos tultiin tähän, jotain meni pieleen, jolloin näytetään form uudelleen
            return View(model);
        }

        //
        // POST: /Kayttaja/Ulos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ulos()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Etusivu");
        }

        //
        // GET: /Kayttaja/Muuta
        public ActionResult Muuta(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Salasana on muutettu."
                : message == ManageMessageId.SetPasswordSuccess ? "Salasana on asetettu."
                : message == ManageMessageId.RemoveLoginSuccess ? "Ulkoinen sisään kirjaus on poistettu."
                : message == ManageMessageId.Error ? "Tapahtui virhe."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Muuta");
            return View();
        }

        //
        // POST: /Kayttaja/Muuta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Muuta(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Muuta");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Muuta", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // Käyttäjällä ei ole salasanaa, joten poistetaan mahdollinen validointivirhe kentästä OldPassword
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Muuta", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // Jos tultiin tähän, jotain meni pieleen, jolloin näytetään form uudelleen
            return View(model);
        }

        #region Helpers
        // Used XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Etusivu");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}