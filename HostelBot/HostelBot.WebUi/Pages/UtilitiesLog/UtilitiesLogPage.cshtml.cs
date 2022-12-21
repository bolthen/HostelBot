using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.UtilitiesLog;

[Authorize]
public class UtilitiesLogPage : PageModel
{
    [BindProperty]
    public DateTime StartData { get; set; }
    
    [BindProperty]
    public DateTime EndData { get; set; }
    
    public UtilityName[] Utilities { get; set; }
    
    public readonly string MaxChooseDate;
    private readonly HostelRepository hostelRepository;
    
    public UtilitiesLogPage(HostelRepository hostelRepository)
    {
        this.hostelRepository = hostelRepository;
        MaxChooseDate = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
    }
    
    public async Task<IActionResult> OnGet()
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var id))
            RedirectToPage("/Account/AccessDenied");
        
        var hostel = await hostelRepository.GetAsync(id);
        Utilities = hostel?.UtilityNames.ToArray() ?? Array.Empty<UtilityName>();
        return Page();
    }

    public IActionResult OnPostPrintUtilities(int utilityNameId)
    {
        var mas = PdfCreator.CreatePdfFile("<label>Конец периода:</label>");
        var file_type = "application/pdf";
        var file_name = "book2.pdf";
        return File(mas, file_type, file_name);
    }
}