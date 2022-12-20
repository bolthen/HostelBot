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
    
    public IActionResult OnGet(int id)
    {
        Resident = residentRepository.GetAsync(id).Result;

        if (Resident is null)
            return RedirectToPage("/Residents/ResidentNotFoundPage", Resident);

        return Page();
    }
    
    public IActionResult OnPost()
    {
        if (!residentRepository.DeleteAsync(Resident.Id).Result)
            return RedirectToPage("/Residents/ResidentNotFoundPage", Resident);

        return RedirectToPage("/Residents/ResidentsPage");
    }
}