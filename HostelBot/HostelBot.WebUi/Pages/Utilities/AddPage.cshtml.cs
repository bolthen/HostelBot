using System.ComponentModel.DataAnnotations;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebUi.Pages.Utilities;

[Authorize]
public class AddPage : PageModel
{
    private readonly UtilityNameRepository utilityNameRepository;
    
    public AddPage(UtilityNameRepository utilityNameRepository)
    {
        this.utilityNameRepository = utilityNameRepository;
    }
    
    public void OnGet()
    {
    }
    
    public IActionResult OnPost()
    {
        var name = HttpContext.Request.Form["Workable"].ToString();
        utilityNameRepository.CreateAsync(new UtilityName(name, "№6"));
        return RedirectToPage("/Utilities/UtilitiesPage");
    }
}