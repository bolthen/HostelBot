using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.UtilitiesLog;

public class UtilitiesLogPage : PageModel
{
    public UtilityName[] Utilities { get; set; }
    
    private readonly Hostel? hostel;
    
    public UtilitiesLogPage(HostelRepository hostelRepository)
    {
        hostel = hostelRepository.GetAsync(1L).Result;
    }
    
    public void OnGet()
    {
        Utilities = hostel?.UtilityNames.ToArray() ?? Array.Empty<UtilityName>();
    }
}