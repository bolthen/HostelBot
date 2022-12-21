using System.Text;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Misc;
using HostelBot.Domain.Infrastructure.Misc.HtmlTableBuilder;
using HostelBot.Domain.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;

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
    
    public UtilitiesLogPage(HostelRepository hostelRepository, UtilityNameRepository utilityNameRepository)
    {
        this.hostelRepository = hostelRepository;
        this.utilityNameRepository = utilityNameRepository;
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

        var utilityName = await utilityNameRepository.GetAsync(utilityNameId);
        var matchUtilities = await hostelRepository.GetUtilityByDate(id, StartData, EndData, utilityName.Name);
        var data = HtmlUtilitiesLogMaker.Make(matchUtilities);
        
        const string css = "table, td { border: 1px solid #333; } thead, tfoot { background-color: #333; color: #fff; } ";
        const string fileType = "application/pdf";

        var mas = PdfCreator.CreatePdfFile(data, css);
        var fileName = $"{utilityName} {StartData} -- {EndData}.pdf";
        return File(mas, fileType, fileName);
    }
}