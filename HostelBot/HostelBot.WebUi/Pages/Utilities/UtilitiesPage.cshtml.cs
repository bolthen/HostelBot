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
        private static bool flag;
        public UtilityName[] Utilities { get; set; }
    
        private readonly HostelRepository hostelRepository;
        private readonly ResidentRepository residentRepository;

        public UtilitiesPage(HostelRepository hostelRepository, ResidentRepository residentRepository)
        {
            this.hostelRepository = hostelRepository;
            this.residentRepository = residentRepository;
            return;
            // if (flag)
            //     return;
            // flag = true;
            // var first = new UtilityName("Сантехник", "№6");
            // first.Id = 5;
            // var second = new UtilityName("Электрик", "№6");
            // second.Id = 6;
            // var third = new UtilityName("Клининг", "№6");
            // third.Id = 2;
            //
            // this.utilityNameRepository.CreateAsync(first);
            // this.utilityNameRepository.CreateAsync(second);
            // this.utilityNameRepository.CreateAsync(third);
        }
    
        public void OnGet()
        {
            var k = residentRepository.GetAsync(1).Result;
            Utilities = hostelRepository.GetByName("№6").Result.UtilityNames.ToArray();
        }
    }
}