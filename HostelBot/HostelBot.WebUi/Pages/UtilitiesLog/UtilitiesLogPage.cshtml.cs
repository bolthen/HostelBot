using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.UtilitiesLog;

[Authorize]
public class UtilitiesLogPage : PageModel
{
    public UtilityName[] Utilities { get; set; }
    
    private readonly HostelRepository hostelRepository;
    
    public UtilitiesLogPage(HostelRepository hostelRepository)
    {
        this.hostelRepository = hostelRepository;
    }
    
    public async Task<IActionResult> OnGet()
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var id))
            RedirectToPage("/Account/AccessDenied");
        
        var hostel = await hostelRepository.GetAsync(id);
        Utilities = hostel?.UtilityNames.ToArray() ?? Array.Empty<UtilityName>();
        return Page();
    }
}