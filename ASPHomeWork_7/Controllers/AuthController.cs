using ASPHomeWork_7.Models.ViewModels;
using ASPHomeWork_7.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPHomeWork_7.Controllers;

public class AuthController : Controller
{
    private readonly IUserManager userManager;

    public AuthController(IUserManager userManager)
    {
        this.userManager = userManager;
    }

    public IActionResult Register()
    {
        return View();
    }


    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (userManager.Register(model.Login, model.Password, false))
                    return RedirectToAction("Login");

                ModelState.AddModelError("All", "Login is allready taken");
            }
            return View(model);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (userManager.Login(model.Login, model.Password))
                return RedirectToAction("Index", "Home");
            ModelState.AddModelError("All", "Incorrect login or password");
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete("auth");
        return RedirectToAction("Index", "Home");
    }
}
