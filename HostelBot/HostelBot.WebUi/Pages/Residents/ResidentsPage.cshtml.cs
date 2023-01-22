using System.Collections.ObjectModel;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.Residents
{
    [Authorize]
    public class ResidentsPage : PageModel
    {
        public IReadOnlyCollection<Resident> Residents { get; set; }

        private readonly HostelRepository hostelRepository;

        public ResidentsPage(HostelRepository hostelRepository, ResidentRepository residentRepository)
        {
            this.hostelRepository = hostelRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            if (!User.GetClaimValue("Hostel").TryParseInt(out var id))
                RedirectToPage("/Account/AccessDenied");
            
            var hostel = await hostelRepository.GetAsync(id);
            Residents = hostel?.Residents.Where(x => x.IsAccepted).ToArray() ?? Array.Empty<Resident>();
            return Page();
        }
    }
}