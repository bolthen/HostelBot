using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;
    public string WelcomeMessage = "Добро пожаловать";
    
    public IndexModel(ILogger<IndexModel> logger)
    {
        this.logger = logger;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!User.GetClaimValue("FirstName").TryGetString(out var firstName))
            return Page();
        if (!User.GetClaimValue("MiddleName").TryGetString(out var secondName))
            return Page();
        if (!User.GetClaimValue("Surname").TryGetString(out var lastName))
            return Page();

        WelcomeMessage += $", {firstName} {secondName}";
        return Page();
    }
}