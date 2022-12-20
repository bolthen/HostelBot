using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.Residents;

public class ResidentDeletePage : PageModel
{
    private readonly ResidentRepository residentRepository;
    
    [BindProperty]
    public Resident Resident { get; set; }
    
    public ResidentDeletePage(ResidentRepository residentRepository)
    {
        this.residentRepository = residentRepository;
    }
    
    public async Task<IActionResult> OnGet(long id)
    {
        Resident = await residentRepository.GetAsync(id);

        if (Resident is null)
            return RedirectToPage("/Residents/ResidentNotFoundPage", Resident);

        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        if (!await residentRepository.DeleteAsync(Resident.Id))
            return RedirectToPage("/Residents/ResidentNotFoundPage", Resident);

        return RedirectToPage("/Residents/ResidentsPage");
    }
}