using GoogleOAuth.Services;
using GoogleOAuth.Services.OAuth;
using Microsoft.AspNetCore.Mvc;

namespace GoogleOAuth.Controllers;

public class OAuthController : Controller
{
    private readonly IOAuthService _oAuthService;

    public OAuthController(IOAuthService oAuthService)
    {
        _oAuthService = oAuthService;
    }

    [Route("cb")]
    public async Task<IActionResult> Callback(string state, string code, string scope)
    {
        string accessToken = await _oAuthService.GetAccessToken(code);
        HttpContext.Session.SetString("token", accessToken);
        return RedirectToAction("Index", "Contacts");
    }
}