using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.Utilities;

[Authorize]
public class DeletePage : PageModel
{
    private readonly UtilityNameRepository utilityNameRepository;
    private readonly HostelRepository hostelRepository;
    
    [BindProperty]
    public UtilityName Utility { get; set; }
    
    [BindProperty]
    public Hostel Hostel { get; set; }
    
    public DeletePage(HostelRepository hostelRepository, UtilityNameRepository utilityNameRepository)
    {
        this.utilityNameRepository = utilityNameRepository;
        this.hostelRepository = hostelRepository;
    }
    
    public IActionResult OnGet(int id)
    {
        Utility = utilityNameRepository.GetAsync(id).Result;

        if (Utility is null)
            return RedirectToPage("/NotFound");

        return Page();
    }
    
    public IActionResult OnPost()
    {
        if (!utilityNameRepository.DeleteAsync(Utility.Id).Result)
            return RedirectToPage("/NotFound");

        return RedirectToPage("/Utilities/UtilitiesPage");
    }
}