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

             // var hostel = new Hostel("№6");
             // var a = new Room(718, hostel);
             // var b = new Room(719, hostel);
             //
             // var mockBd = new List<Resident>
             // {
             //     new (1, "John", "Johnson", hostel, a),
             //     new (5, "Sam", "Samson", hostel, a),
             //     new (3, "Shon", "Shonson", hostel, b),
             //     new (4, "Wayn", "Waynson", hostel, b)
             //     
             // };
             //
             // foreach (var resident in mockBd)
             //     residentRepository.CreateAsync(resident);
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