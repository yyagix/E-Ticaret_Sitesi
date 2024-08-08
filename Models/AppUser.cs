using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? City { get; set; }
    public int ConfirmedCode { get; set; } = 0; // Varsayılan değer
}
