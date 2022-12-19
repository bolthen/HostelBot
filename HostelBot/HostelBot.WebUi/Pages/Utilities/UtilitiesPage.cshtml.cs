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
    
        public IActionResult OnGet()
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type == "Hostel");
            if (claim is null)
                return RedirectToPage("/Account/AccessDenied");

            if (int.TryParse(claim.Value, out var id))
            {
                var hostel = hostelRepository.GetAsync(id).Result;
                Utilities = hostel?.UtilityNames.ToArray() ?? Array.Empty<UtilityName>();
                return Page();
            }
            
            return RedirectToPage("/Account/AccessDenied");
        }
    }
}