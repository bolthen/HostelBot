using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.Utilities
{
    [Authorize]
    public class UtilitiesPage : PageModel
    {
        public UtilityName[] Utilities { get; set; }
    
        private readonly Hostel? hostel;

        public UtilitiesPage(HostelRepository hostelRepository)
        {
            hostel = hostelRepository.GetAsync(1L).Result;
        }
    
        public void OnGet()
        {
            Utilities = hostel?.UtilityNames.ToArray() ?? Array.Empty<UtilityName>();
        }
    }
}