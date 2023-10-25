using GoogleOAuth.Services.Contacts;
using GoogleOAuth.Services.Contacts.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GoogleOAuth.Controllers;

public class ContactsController : Controller
{
    private readonly IContactsService _contactsService;
    private readonly OAuthConfiguration _oauthConfig;

    public ContactsController(IContactsService contactsService, IOptions<OAuthConfiguration> configuration)
    {
        _contactsService = contactsService;
        _oauthConfig = configuration.Value;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        Console.WriteLine(HttpContext.Session.Id);
        var accessToken = HttpContext.Session.GetString("token");
        var isAuthenticated = !string.IsNullOrEmpty(accessToken);
        if (isAuthenticated)
        {
            await LoadContacts(accessToken);
        }
        else
        {
            ViewBag.ClientId = _oauthConfig.ClientId;
            ViewBag.Scope = _oauthConfig.Scope;
            ViewBag.RedirectUrl = _oauthConfig.RedirectUrl;
        }

        ViewBag.IsAuthenticated = isAuthenticated;
        return View();
    }

    public async Task LoadContacts(string accessToken)
    {
        List<Contact> contacts = await _contactsService.GetContacts(accessToken);
        ViewBag.Contacts = contacts ?? new List<Contact>();
    }
}