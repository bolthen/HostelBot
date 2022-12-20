using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages.Account
{
    public class Login : PageModel
    {
        [BindProperty]
        public HostelManager Manager { get; set; }

        private readonly AdministratorRepository administratorRepository;

        public Login(AdministratorRepository administratorRepository)
        {
            this.administratorRepository = administratorRepository;
        }
        
        public void OnGet()
        {
        
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var administrator = administratorRepository.GetByLogin(Manager.Login).Result;
            if (administrator is null)
                return Page();

            var storedPassword = administrator.HashPassword;
            if (!PasswordHasher.Verify(Manager.Password, storedPassword))
                return Page();
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, administrator.Login),
                new Claim("Hostel", administrator.HostelId.ToString()),
                new Claim("FirstName", administrator.Name),
                new Claim("MiddleName", administrator.MiddleName),
                new Claim("Surname", administrator.Surname)
            };
            
            /*var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Manager.Login),
                new Claim("Hostel", "1")
            };*/
            
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var claimPrincipal = new ClaimsPrincipal(identity);
            
            await HttpContext.SignInAsync("CookieAuth", claimPrincipal);

            return RedirectToPage("/Index");
        }
    }

    public class HostelManager : IdentityUser
    {
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }
        
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}