using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Misc;
using HostelBot.Domain.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.UtilitiesLog;

[Authorize]
public class UtilitiesLogPage : PageModel
{
    private const string FileType = "application/pdf";
    
    [BindProperty]
    public DateTime StartDate { get; set; }
    
    [BindProperty]
    public DateTime EndDate { get; set; }
    
    public UtilityName[] Utilities { get; set; }
    
    public readonly string MaxChooseDate;
    private readonly HostelRepository hostelRepository;
    private readonly UtilityNameRepository utilityNameRepository;
    private readonly ResidentRepository residentRepository;
    
    public UtilitiesLogPage(HostelRepository hostelRepository, UtilityNameRepository utilityNameRepository, ResidentRepository residentRepository)
    {
        this.hostelRepository = hostelRepository;
        this.utilityNameRepository = utilityNameRepository;
        this.residentRepository = residentRepository;
        MaxChooseDate = DateTime.Today.AddDays(0).ToString("yyyy-MM-dd");
    }
    
    public async Task<IActionResult> OnGet()
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var id))
            RedirectToPage("/Account/AccessDenied");
        
        var hostel = await hostelRepository.GetAsync(id);
        Utilities = hostel?.UtilityNames.ToArray() ?? Array.Empty<UtilityName>();
        return Page();
    }

    public async Task<IActionResult> OnPostPrintUtilities(int utilityNameId)
    {
        if (!User.GetClaimValue("Hostel").TryParseLong(out var id))
            RedirectToPage("/Account/AccessDenied");
        
        var utilityName = await utilityNameRepository.GetAsync(utilityNameId);
        var matchData = hostelRepository.GetUtilitiesByDate(id, StartDate, EndDate, utilityName.Name);
        
        var data = HtmlUtilitiesLogMaker.Make(matchData.ToList());
        
        var mas = PdfCreator.CreatePdfFile(data);
        var fileName = $"{utilityName} {StartDate} -- {EndDate}.pdf";
        return File(mas, FileType, fileName);
    }
}