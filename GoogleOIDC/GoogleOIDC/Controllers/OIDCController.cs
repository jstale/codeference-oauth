using GoogleOAuth.Services;
using GoogleOIDC.Services.OAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GoogleOIDC.Controllers;

public class OidcController : Controller
{
    private readonly IOidcService _oidcService;

    public OidcController(IOidcService oidcService)
    {
        _oidcService = oidcService;
    }

    [Route("cb")]
    public async Task<IActionResult> Callback(string state, string code, string scope)
    {
        var user = await _oidcService.GetUser(code);

        var authenticationProperties = new AuthenticationProperties
        {
            IsPersistent = true, // Set to true to create a persistent cookie
        };

        await HttpContext.SignInAsync("CookieAuthScheme", user, authenticationProperties);
        
        return RedirectToAction("Index", "Home");
    }
}