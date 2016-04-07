using Microsoft.AspNet.Mvc;
using MVC6.Seed.V1.Services.Identity;
using MVC6.Seed.V1.Services.Identity.Exceptions;
using MVC6.Seed.V1.Web.Utility;
using MVC6.Seed.V1.Web.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Web.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(
            IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                try
                {
                    await _accountService.Login(model.Map());
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                catch (IdentityErrorException ex)
                {
                    ModelState.AddIdentityErrors(ex);
                }
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("log-out")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
