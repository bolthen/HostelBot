using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;
using HostelBot.Domain.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ninject;

namespace WebUi.Pages.Residents;

public class ResidentsPage : PageModel
{
    public IReadOnlyCollection<Resident> Residents { get; set; }
    
    public ResidentService residentService { get; set; }

    public ResidentsPage(ResidentService residentService)
    {
        this.residentService = residentService;
        
        var hostel = new Hostel("№6");
        var a = new Room(718, hostel);
        var b = new Room(719, hostel);
        
        
        var mockBd = new List<Resident>
        {
            new (1, "John", "Johnson", hostel, new Room(718, hostel)),
            new (1, "John", "Johnson", hostel, new Room(718, hostel)),
            new (5, "Sam", "Samson", hostel, a),
            new (3, "Shon", "Shonson", hostel, b),
            new (4, "Wayn", "Waynson", hostel, b)
        };

        foreach (var resident in mockBd)
        {
            this.residentService.CreateAsync(resident);
        }
    }
    
    public void OnGet()
    {
        Residents = GetResidents().ToArray();
        // Подгружаем жителей из БД и парсим в табличку,
        // к которой будем обращаться в отображении
    }

    private IQueryable<Resident> GetResidents()
    {
        return residentService.GetAll();
    }
}