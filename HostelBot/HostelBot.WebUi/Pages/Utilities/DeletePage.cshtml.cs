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
    
    [BindProperty]
    public UtilityName Utility { get; set; }
    
    public DeletePage(UtilityNameRepository utilityNameRepository, HostelRepository hostelRepository)
    {
        this.utilityNameRepository = utilityNameRepository;
    }
    
    public async Task<IActionResult> OnGet(long id)
    {
        Utility = await utilityNameRepository.GetAsync(id);

        if (Utility is null)
            return RedirectToPage("/Utilities/UtilityNotFound", Utility);

        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        if (!await utilityNameRepository.DeleteAsync(Utility.Id))
            return RedirectToPage("/Utilities/UtilityNotFound", Utility);
        
        return RedirectToPage("/Utilities/UtilitiesPage");
    }
}