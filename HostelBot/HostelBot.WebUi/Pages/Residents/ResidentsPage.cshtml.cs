using System.Collections.ObjectModel;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.Residents
{
    [Authorize]
    public class ResidentsPage : PageModel
    {
        public IReadOnlyCollection<Resident> Residents { get; set; }

        private readonly Hostel? hostel;

        public ResidentsPage(HostelRepository hostelRepository)
        {
            var tmpId = 1; // TODO User.Claim
            hostel = hostelRepository.GetAsync(tmpId).Result;

            //var hostel = hostelRepository.GetAsync("№6").Result.Residents;
            // this.residentRepository = residentRepository;
            //
            // var hostel = new Hostel("№6");
            // var a = new Room(718, hostel);
            // var b = new Room(719, hostel);
            //
            //
            // var mockBd = new List<Resident>
            // {
            //     new (1, "John", "Johnson", hostel, new Room(718, hostel)),
            //     new (1, "John", "Johnson", hostel, new Room(718, hostel)),
            //     new (5, "Sam", "Samson", hostel, a),
            //     new (3, "Shon", "Shonson", hostel, b),
            //     new (4, "Wayn", "Waynson", hostel, b)
            //     
            // };
            //
            // foreach (var resident in mockBd)
            //     residentRepository.CreateAsync(resident);
        }
    
        public void OnGet()
        {
            Residents = hostel?.Residents.ToArray() ?? Array.Empty<Resident>();
        }
    }
}