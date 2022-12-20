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
    private readonly HostelRepository hostelRepository;
    
    public AddPage(HostelRepository hostelRepository)
    {
        this.hostelRepository = hostelRepository;
    }
    
    public void OnGet()
    {
    }
    
    public IActionResult OnPost()
    {
        if (!User.GetClaimValue("Hostel").TryParseInt(out var id))
            RedirectToPage("/Account/AccessDenied");

        var name = HttpContext.Request.Form["Workable"].ToString();
        var hostel = hostelRepository.GetAsync(id).Result;
        hostel.AddUtilityName(new UtilityName(name));
        hostelRepository.UpdateAsync(hostel);
        return RedirectToPage("/Utilities/UtilitiesPage");
    }
}