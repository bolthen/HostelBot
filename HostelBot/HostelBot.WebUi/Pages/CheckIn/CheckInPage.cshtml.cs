using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telegram.Bot.Types;

namespace WebUi.Pages.CheckIn;

public class CheckInPage : PageModel
{
    [BindProperty]
    public IReadOnlyCollection<Resident> Residents { get; set; }
    [BindProperty]
    public Hostel hostel { get; set; }
    
    private readonly HostelRepository hostelRepository;

    public CheckInPage(HostelRepository hostelRepository)
    {
        this.hostelRepository = hostelRepository;
    }

    public IActionResult OnGet()
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var id))
            RedirectToPage("/Account/AccessDenied");
            
        hostel = hostelRepository.GetAsync(id).Result;
        Residents = hostel?.Residents.Where(x => !x.IsAccepted).ToArray() ?? Array.Empty<Resident>();
        return Page();
    }
    
    public IActionResult OnPostSubmit(int id)
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var hostelId))
            RedirectToPage("/Account/AccessDenied");
            
        hostel = hostelRepository.GetAsync(hostelId).Result;
        hostel = hostelRepository.AcceptResident(id, hostel.Id).Result;
        Residents = hostel.Residents.Where(x => !x.IsAccepted).ToArray();
        return Page();
    }
    
    public IActionResult OnPostDecline(int id)
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var hostelId))
            RedirectToPage("/Account/AccessDenied");
            
        hostel = hostelRepository.GetAsync(hostelId).Result;
        hostel = hostelRepository.DeleteResident(id, hostel.Id).Result;
        Residents = hostel.Residents.Where(x => !x.IsAccepted).ToArray();
        return Page();
    }
}