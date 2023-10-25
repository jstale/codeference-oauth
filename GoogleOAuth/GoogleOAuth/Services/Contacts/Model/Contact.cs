namespace GoogleOAuth.Services.Contacts.Model;

public class Contact
{
    public List<Name> Names { get; set; }
    public List<Email> EmailAddresses { get; set; }
    
    public List<Photo> Photos { get; set; }
    
    public List<Birthday> Birthdays { get; set; }
}