using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace GraduationGuideline.web.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly AppSettings _appSettings;


        public AuthController(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
        }

        /// <summary>
        /// Login View, this is the main entry point for establishing an identity,
        /// in the case that Shib is disabled, you'll set the user object here.
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            IShibClaim shib = new ShibClaim();
            ClaimsPrincipal principal;

            if (this._appSettings.EnableShib)
            {
                principal = shib.BuildClaimsPrincipal(HttpContext.Request.Headers["myheadername"]);
            }
            else
            {
                principal = shib.BuildClaimsPrincipal("derekst"); /// THis can be used in debug to set a username
            }

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult UnAuthorized()
        {
            return View();
        }
    }
}