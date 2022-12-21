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
    [BindProperty]
    public DateTime StartData { get; set; }
    
    [BindProperty]
    public DateTime EndData { get; set; }
    
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

    public async Task<IActionResult> OnPostPrintUtilities(int utilityNameId)
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var id))
            RedirectToPage("/Account/AccessDenied");
        
        var hostel = await hostelRepository.GetAsync(id);
        var residents = hostel.Residents;
        var utilityName = await utilityNameRepository.GetAsync(utilityNameId);
        var residentsWithUtility = new List<Resident>();
        foreach (var resident in residents)
            residentsWithUtility.Add(await residentRepository.GetAsync(resident.Id));
        
        
        var matchUtilities = residentsWithUtility
            .SelectMany(x => x.Utilities)
            .Where(x => x.Name == utilityName.Name)
            .Where(x => x.CreationDateTime >= StartData)
            .Where(x => x.CreationDateTime <= EndData.AddDays(1))
            .ToList();
        var data = HtmlUtilitiesLogMaker.Make(matchUtilities);
        
        const string css = "table, td { border: 1px solid #333; } thead, tfoot { background-color: #333; color: #fff; } ";
        const string fileType = "application/pdf";

        var mas = PdfCreator.CreatePdfFile(data, css);
        var fileName = $"{utilityName} {StartData} -- {EndData}.pdf";
        return File(mas, fileType, fileName);
    }
}