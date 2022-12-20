using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.Utilities
{
    [Authorize]
    public class UtilitiesPage : PageModel
    {
        public UtilityName[] Utilities { get; set; }

        private readonly HostelRepository hostelRepository;

        public UtilitiesPage(HostelRepository hostelRepository)
        {
            this.hostelRepository = hostelRepository;
        }
    
        public async Task<IActionResult> OnGet()
        {
            if (!User.GetClaimValue("Hostel").TryParseInt(out var id))
                RedirectToPage("/Account/AccessDenied");

            var hostel = await hostelRepository.GetAsync(id);
            Utilities = hostel?.UtilityNames.ToArray() ?? Array.Empty<UtilityName>();
            return Page();
        }
    }
}