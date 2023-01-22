using Microsoft.AspNetCore.Mvc.RazorPages;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Pages.Appeals;

public class AppealsMonitorPage : PageModel
{
    public List<Appeal> Appeals;
    private readonly AppealRepository appealRepository;
    private readonly HostelRepository hostelRepository;

    public AppealsMonitorPage(HostelRepository hostelRepository, AppealRepository appealRepository)
    {
        this.hostelRepository = hostelRepository;
        this.appealRepository = appealRepository;
    }
    
    public void OnGet()
    {
        if (!User.GetClaimValue("Hostel").TryParseLong(out var id))
            RedirectToPage("/Account/AccessDenied");

        UpdateAppeals(id);
    }
    
    public async Task<IActionResult> OnPostAppealResponse(long appealId, string response)
    {
        if (!User.GetClaimValue("Hostel").TryParseLong(out var id))
            RedirectToPage("/Account/AccessDenied");

        await appealRepository.AddAnswer(appealId, response);
        UpdateAppeals(id);
        return RedirectToPage();
    }
    
    public async Task<IActionResult> OnPostAppealDelete(long appealId)
    {
        if (!User.GetClaimValue("Hostel").TryParseLong(out var id))
            RedirectToPage("/Account/AccessDenied");

        await appealRepository.DeleteAsync(appealId);
        UpdateAppeals(id);
        return RedirectToPage();
    }

    private void UpdateAppeals(long hostelId)
    {
        Appeals = hostelRepository
            .GetAppealsByHostelId(hostelId)
            .Where(x => x.Answer is null)
            .ToList();
    }
}