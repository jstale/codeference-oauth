using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GoogleOIDC.Controllers;

public class AccountController : Controller
{
    private readonly OAuthConfiguration _oauthConfig;

    public AccountController(IOptions<OAuthConfiguration> configuration)
    {
        _oauthConfig = configuration.Value;
    }
    
    // GET
    public IActionResult Login()
    {
        
        ViewBag.ClientId = _oauthConfig.ClientId;
        ViewBag.Scope = HttpUtility.UrlEncode(_oauthConfig.Scope);
        ViewBag.RedirectUrl = _oauthConfig.RedirectUrl;

        
        return View();
    }
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("CookieAuthScheme");

        return RedirectToAction("Login");
    }
}