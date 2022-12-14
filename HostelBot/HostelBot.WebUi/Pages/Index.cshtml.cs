using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;
    public string WelcomeMessage => "Добро пожаловать, Галина Михайловна";
    //public string Message { get; set; }
    
    public IndexModel(ILogger<IndexModel> logger)
    {
        this.logger = logger;
    }

    public void OnGet()
    {
        //Message = $"Hello from OnGet {DateTime.Now}";
    }
}