using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ButterMtn_296.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ButterMountain2.Controllers
{
    public class AccountController : Controller
    {

        UserManager<AppUser> userManager;
        SignInManager<AppUser> signInManager;

    public AccountController(UserManager<AppUser> userMngr, SignInManager<AppUser> signInMngr)
    {
        userManager = userMngr;
        signInManager = signInMngr;
    } // The Register(), LogIn(), and LogOut()methods go here } 

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        //if (ModelState.IsValid)
        {
            var user = new AppUser { UserName = model.Username };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Member");
                await signInManager.SignInAsync(user, isPersistent: false); return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult LogIn(string returnURL = "")
    {
        var model = new LoginVM { ReturnUrl = returnURL };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LoginVM model)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                { return Redirect(model.ReturnUrl); }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        ModelState.AddModelError("", "Invalid username/password."); return View(model);
    }
        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }

    }
}

