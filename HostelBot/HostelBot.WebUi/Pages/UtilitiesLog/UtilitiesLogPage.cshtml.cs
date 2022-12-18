using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.UtilitiesLog;

public class UtilitiesLogPage : PageModel
{
    public UtilityName[] Utilities { get; set; }
    
    private readonly HostelRepository hostelRepository;
    
    public UtilitiesLogPage(HostelRepository hostelRepository)
    {
        this.hostelRepository = hostelRepository;
    }
    
    public void OnGet()
    {
        Utilities = new[]
        {
            new UtilityName("Сантехник"),
            new UtilityName("Клининг"),
            new UtilityName("Электрик")
        };
    }
}