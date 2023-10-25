using GoogleOAuth.Services.Contacts;
using GoogleOAuth.Services.Contacts.Model;
using Microsoft.AspNetCore.Mvc;

namespace GoogleOAuth.Controllers;

public class ContactsController : Controller
{
    private readonly IContactsService _contactsService;

    public ContactsController(IContactsService contactsService)
    {
        _contactsService = contactsService;
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

        ViewBag.IsAuthenticated = isAuthenticated;
        return View();
    }

    public async Task LoadContacts(string accessToken)
    {
        List<Contact> contacts = await _contactsService.GetContacts(accessToken);
        ViewBag.Contacts = contacts ?? new List<Contact>();
    }
}