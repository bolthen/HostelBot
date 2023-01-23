using HostelBot.Domain.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.Residents;

public class ResidentNotFoundPage : PageModel
{
    public Resident? Resident { get; set; }
    
    public void OnGet(Resident resident)
    {
        Resident = resident;
    }
}