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

    public async Task<IActionResult> OnGet()
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var id))
            RedirectToPage("/Account/AccessDenied");
            
        hostel = await hostelRepository.GetAsync(id);
        Residents = hostel?.Residents.Where(x => !x.IsAccepted).ToArray() ?? Array.Empty<Resident>();
        return Page();
    }
    
    public async Task<IActionResult> OnPostSubmitAsync(long id)
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var hostelId))
            RedirectToPage("/Account/AccessDenied");
            
        hostel = await hostelRepository.GetAsync(hostelId);
        hostel = await hostelRepository.AcceptResident(id, hostel.Id);
        Residents = hostel.Residents.Where(x => !x.IsAccepted).ToArray();
        return Page();
    }
    
    public async Task<IActionResult> OnPostDeclineAsync(long id)
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var hostelId))
            RedirectToPage("/Account/AccessDenied");
            
        hostel = await hostelRepository.GetAsync(hostelId);
        hostel = await hostelRepository.DeleteResident(id, hostel.Id);
        Residents = hostel.Residents.Where(x => !x.IsAccepted).ToArray();
        return Page();
    }
}