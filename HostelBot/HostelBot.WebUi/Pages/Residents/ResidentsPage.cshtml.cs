using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.Residents
{
    [Authorize]
    public class ResidentsPage : PageModel
    {
        public IReadOnlyCollection<Resident> Residents { get; set; }

        private readonly ResidentRepository residentRepository;

        public ResidentsPage(ResidentRepository residentRepository)
        {
            //var hostel = hostelRepository.GetAsync("№6").Result.Residents;
            this.residentRepository = residentRepository;
        
            var hostel = new Hostel("№6");
            var a = new Room(718, "№6");
            var b = new Room(719, "№6");
        
        
            var mockBd = new List<Resident>
            {
                new (1, "John", "Johnson", hostel, new Room(718, "№6")),
                new (1, "John", "Johnson", hostel, new Room(718, "№6")),
                new (5, "Sam", "Samson", hostel, a),
                new (3, "Shon", "Shonson", hostel, b),
                new (4, "Wayn", "Waynson", hostel, b)
                
            };

            foreach (var resident in mockBd)
            {
                this.residentRepository.CreateAsync(resident);
            }
        }
    
        public void OnGet()
        {
            Residents = residentRepository.GetAll().Result.ToArray();
            // Подгружаем жителей из БД и парсим в табличку,
            // к которой будем обращаться в отображении
        }
    }
}