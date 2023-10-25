using System.Net.Http.Headers;
using GoogleOAuth.Services.Contacts.Model;

namespace GoogleOAuth.Services.Contacts;

public interface IContactsService
{
    Task<List<Contact>> GetContacts(string accessToken);
}

public class ContactsService : IContactsService
{
    private const string GooglePeopleApiUrl = "https://people.googleapis.com/";
    private const string ListContactsEndpoint = "v1/people/me/connections";
    private readonly List<string> _personFields = new() {"photos", "names", "emailAddresses", "birthdays", "biographies"}; 
    private const char Delimiter = ',';
    
    public async Task<List<Contact>> GetContacts(string accessToken)
    {
        if(string.IsNullOrEmpty(accessToken)) return new List<Contact>();
        
        List<Contact> result = new List<Contact>();
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(GooglePeopleApiUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var endpoint = $"{ListContactsEndpoint}?personFields={String.Join(Delimiter, _personFields)}";

            HttpResponseMessage response = await client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var responses = await response.Content.ReadAsStringAsync();
                var responseContent = await response.Content.ReadFromJsonAsync<GetContactsResponse>();
                Console.WriteLine(responseContent);
                result = responseContent.Connections;
            }
            else
            {
                // Handle errors, e.g., response.StatusCode and response.ReasonPhrase
            }
        }

        return result;
    }
}