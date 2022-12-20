using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;
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
    
    public DeletePage(UtilityNameRepository utilityNameRepository)
    {
        this.utilityNameRepository = utilityNameRepository;
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