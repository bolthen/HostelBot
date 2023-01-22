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
    public Hostel Hostel { get; set; }
    
    private readonly HostelRepository hostelRepository;

    public CheckInPage(HostelRepository hostelRepository)
    {
        this.hostelRepository = hostelRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var id))
            RedirectToPage("/Account/AccessDenied");
            
        Hostel = await hostelRepository.GetAsync(id);
        Residents = Hostel?.Residents.Where(x => !x.IsAccepted).ToArray() ?? Array.Empty<Resident>();
        return Page();
    }
    
    public async Task<IActionResult> OnPostSubmitAsync(long id)
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var hostelId))
            RedirectToPage("/Account/AccessDenied");
            
        Hostel = await hostelRepository.GetAsync(hostelId);
        Hostel = await hostelRepository.AcceptResident(id, Hostel.Id);
        Residents = Hostel.Residents.Where(x => !x.IsAccepted).ToArray();
        return Page();
    }
    
    public async Task<IActionResult> OnPostDeclineAsync(long id)
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var hostelId))
            RedirectToPage("/Account/AccessDenied");
            
        Hostel = await hostelRepository.GetAsync(hostelId);
        Hostel = await hostelRepository.DeleteResident(id, Hostel.Id);
        Residents = Hostel.Residents.Where(x => !x.IsAccepted).ToArray();
        return Page();
    }
}