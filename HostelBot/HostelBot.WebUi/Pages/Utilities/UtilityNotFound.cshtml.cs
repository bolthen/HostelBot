using HostelBot.Domain.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.Utilities;

public class UtilityNotFound : PageModel
{
    public UtilityName? Utility { get; set; }
    
    public void OnGet(UtilityName utility)
    {
        Utility = utility;
    }
}